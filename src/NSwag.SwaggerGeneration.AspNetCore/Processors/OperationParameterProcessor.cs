﻿//-----------------------------------------------------------------------
// <copyright file="WebApiToSwaggerGeneratorSettings.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Namotion.Reflection;
using NJsonSchema;
using NJsonSchema.Annotations;
using NJsonSchema.Generation;
using NJsonSchema.Infrastructure;
using NSwag.SwaggerGeneration.Processors;
using NSwag.SwaggerGeneration.Processors.Contexts;

namespace NSwag.SwaggerGeneration.AspNetCore.Processors
{
    internal class OperationParameterProcessor : IOperationProcessor
    {
        private readonly AspNetCoreOpenApiDocumentGeneratorSettings _settings;

        public OperationParameterProcessor(AspNetCoreOpenApiDocumentGeneratorSettings settings)
        {
            _settings = settings;
        }

        /// <summary>Processes the specified method information.</summary>
        /// <param name="operationProcessorContext"></param>
        /// <returns>true if the operation should be added to the Swagger specification.</returns>
        public async Task<bool> ProcessAsync(OperationProcessorContext operationProcessorContext)
        {
            if (!(operationProcessorContext is AspNetCoreOperationProcessorContext context))
            {
                return false;
            }

            var httpPath = context.OperationDescription.Path;
            var parameters = context.ApiDescription.ParameterDescriptions;

            var methodParameters = context.MethodInfo.GetParameters();

            var position = 1;
            foreach (var apiParameter in parameters.Where(p => p.Source != null))
            {
                // TODO: Provide extension point so that this can be implemented in the ApiVersionProcessor class
                var versionProcessor = _settings.OperationProcessors.TryGet<ApiVersionProcessor>();
                if (versionProcessor != null &&
                    versionProcessor.IgnoreParameter &&
                    apiParameter.ModelMetadata?.DataTypeName == "ApiVersion")
                {
                    continue;
                }

                // In Mvc < 2.0, there isn't a good way to infer the attributes of a parameter with a IModelNameProvider.Name
                // value that's different than the parameter name. Additionally, ApiExplorer will recurse in to complex model bound types
                // and expose properties as top level parameters. Consequently, determining the property or parameter of an Api is best
                // effort attempt.
                var extendedApiParameter = new ExtendedApiParameterDescription
                {
                    ApiParameter = apiParameter,
                    Attributes = Enumerable.Empty<Attribute>(),
                    ParameterType = apiParameter.Type
                };

                ParameterInfo parameter = null;

                var propertyName = apiParameter.ModelMetadata?.PropertyName;
                var property = !string.IsNullOrEmpty(propertyName) ?
                    apiParameter.ModelMetadata.ContainerType?.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance) :
                    null;

                if (property != null)
                {
                    extendedApiParameter.PropertyInfo = property;
                    extendedApiParameter.Attributes = property.GetCustomAttributes();
                }
                else
                {
                    var parameterDescriptor = apiParameter.TryGetPropertyValue<ParameterDescriptor>("ParameterDescriptor");
                    var parameterName = parameterDescriptor?.Name ?? apiParameter.Name;
                    parameter = methodParameters.FirstOrDefault(m => m.Name.ToLowerInvariant() == parameterName.ToLowerInvariant());
                    if (parameter != null)
                    {
                        extendedApiParameter.ParameterInfo = parameter;
                        extendedApiParameter.Attributes = parameter.GetCustomAttributes();
                    }
                    else
                    {
                        parameterName = apiParameter.Name;
                        property = operationProcessorContext.ControllerType.GetProperty(parameterName, BindingFlags.Public | BindingFlags.Instance);
                        if (property != null)
                        {
                            extendedApiParameter.PropertyInfo = property;
                            extendedApiParameter.Attributes = property.GetCustomAttributes();
                        }
                    }
                }

                if (apiParameter.Type == null)
                {
                    extendedApiParameter.ParameterType = typeof(string);

                    if (apiParameter.Source == BindingSource.Path)
                    {
                        // ignore unused implicit path parameters
                        if (!httpPath.ToLowerInvariant().Contains("{" + apiParameter.Name.ToLowerInvariant() + ":") &&
                            !httpPath.ToLowerInvariant().Contains("{" + apiParameter.Name.ToLowerInvariant() + "}"))
                        {
                            continue;
                        }

                        extendedApiParameter.Attributes = extendedApiParameter.Attributes.Concat(new[] { new NotNullAttribute() });
                    }
                }

                if (extendedApiParameter.Attributes.GetAssignableToTypeName("SwaggerIgnoreAttribute", TypeNameStyle.Name).Any())
                {
                    continue;
                }

                OpenApiParameter operationParameter = null;
                if (apiParameter.Source == BindingSource.Path ||
                    (apiParameter.Source == BindingSource.Custom &&
                     httpPath.Contains($"{{{apiParameter.Name}}}")))
                {
                    operationParameter = await CreatePrimitiveParameterAsync(context, extendedApiParameter).ConfigureAwait(false);
                    operationParameter.Kind = OpenApiParameterKind.Path;
                    operationParameter.IsRequired = true; // apiParameter.RouteInfo?.IsOptional == false;

                    context.OperationDescription.Operation.Parameters.Add(operationParameter);
                }
                else if (apiParameter.Source == BindingSource.Header)
                {
                    operationParameter = await CreatePrimitiveParameterAsync(context, extendedApiParameter).ConfigureAwait(false);
                    operationParameter.Kind = OpenApiParameterKind.Header;

                    context.OperationDescription.Operation.Parameters.Add(operationParameter);
                }
                else if (apiParameter.Source == BindingSource.Query)
                {
                    operationParameter = await CreatePrimitiveParameterAsync(context, extendedApiParameter).ConfigureAwait(false);
                    operationParameter.Kind = OpenApiParameterKind.Query;

                    context.OperationDescription.Operation.Parameters.Add(operationParameter);
                }
                else if (apiParameter.Source == BindingSource.Body)
                {
                    operationParameter = await AddBodyParameterAsync(context, extendedApiParameter).ConfigureAwait(false);
                }
                else if (apiParameter.Source == BindingSource.Form)
                {
                    operationParameter = await CreatePrimitiveParameterAsync(context, extendedApiParameter).ConfigureAwait(false);
                    operationParameter.Kind = OpenApiParameterKind.FormData;

                    context.OperationDescription.Operation.Parameters.Add(operationParameter);
                }
                else
                {
                    if (await TryAddFileParameterAsync(context, extendedApiParameter).ConfigureAwait(false) == false)
                    {
                        operationParameter = await CreatePrimitiveParameterAsync(context, extendedApiParameter).ConfigureAwait(false);
                        operationParameter.Kind = OpenApiParameterKind.Query;

                        context.OperationDescription.Operation.Parameters.Add(operationParameter);
                    }
                }

                if (operationParameter != null)
                {
                    operationParameter.Position = position;
                    position++;

                    if (_settings.SchemaType == SchemaType.OpenApi3)
                    {
                        operationParameter.IsNullableRaw = null;
                    }

                    if (parameter != null)
                    {
                        ((Dictionary<ParameterInfo, OpenApiParameter>)operationProcessorContext.Parameters)[parameter] = operationParameter;
                    }
                }
            }

            RemoveUnusedPathParameters(context.OperationDescription, httpPath);
            UpdateConsumedTypes(context.OperationDescription);

            EnsureSingleBodyParameter(context.OperationDescription);

            return true;
        }

        private void EnsureSingleBodyParameter(OpenApiOperationDescription operationDescription)
        {
            if (operationDescription.Operation.ActualParameters.Count(p => p.Kind == OpenApiParameterKind.Body) > 1)
            {
                throw new InvalidOperationException($"The operation '{operationDescription.Operation.OperationId}' has more than one body parameter.");
            }
        }

        private void UpdateConsumedTypes(OpenApiOperationDescription operationDescription)
        {
            if (operationDescription.Operation.ActualParameters.Any(p => p.IsBinary || p.ActualSchema.IsBinary))
            {
                operationDescription.Operation.TryAddConsumes("multipart/form-data");
            }
        }

        private void RemoveUnusedPathParameters(OpenApiOperationDescription operationDescription, string httpPath)
        {
            operationDescription.Path = "/" + Regex.Replace(httpPath, "{(.*?)(:(([^/]*)?))?}", match =>
            {
                var parameterName = match.Groups[1].Value.TrimEnd('?');
                if (operationDescription.Operation.ActualParameters.Any(p => p.Kind == OpenApiParameterKind.Path && string.Equals(p.Name, parameterName, StringComparison.OrdinalIgnoreCase)))
                {
                    return "{" + parameterName + "}";
                }

                return string.Empty;
            }).Trim('/');
        }

        private bool IsNullable(ParameterInfo parameter)
        {
            var isNullable = Nullable.GetUnderlyingType(parameter.ParameterType) != null;
            if (isNullable)
            {
                return false;
            }

            return parameter.ParameterType.GetTypeInfo().IsValueType;
        }

        private async Task<bool> TryAddFileParameterAsync(
            OperationProcessorContext context, ExtendedApiParameterDescription extendedApiParameter)
        {
            var typeInfo = _settings.ReflectionService.GetDescription(extendedApiParameter.ParameterType.ToContextualType(extendedApiParameter.Attributes), _settings);

            var isFileArray = IsFileArray(extendedApiParameter.ApiParameter.Type, typeInfo);

            var attributes = extendedApiParameter.Attributes
                .Union(extendedApiParameter.ParameterType.GetTypeInfo().GetCustomAttributes());

            var hasSwaggerFileAttribute = attributes.FirstAssignableToTypeNameOrDefault("SwaggerFileAttribute", TypeNameStyle.Name) != null;

            if (typeInfo.Type == JsonObjectType.File ||
                typeInfo.Format == JsonFormatStrings.Binary ||
                hasSwaggerFileAttribute ||
                isFileArray)
            {
                await AddFileParameterAsync(context, extendedApiParameter, isFileArray).ConfigureAwait(false);
                return true;
            }

            return false;
        }

        private async Task AddFileParameterAsync(OperationProcessorContext context, ExtendedApiParameterDescription extendedApiParameter, bool isFileArray)
        {
            var operationParameter = await CreatePrimitiveParameterAsync(context, extendedApiParameter).ConfigureAwait(false);
            InitializeFileParameter(operationParameter, isFileArray);

            context.OperationDescription.Operation.Parameters.Add(operationParameter);
        }

        private bool IsFileArray(Type type, JsonTypeDescription typeInfo)
        {
            var isFormFileCollection = type.Name == "IFormFileCollection";
            if (isFormFileCollection)
            {
                return true;
            }

            if (typeInfo.Type == JsonObjectType.Array && type.GenericTypeArguments.Any())
            {
                var description = _settings.ReflectionService.GetDescription(type.GenericTypeArguments[0].ToContextualType(), _settings);
                if (description.Type == JsonObjectType.File || description.Format == JsonFormatStrings.Binary)
                {
                    return true;
                }
            }

            return false;
        }

        private async Task<OpenApiParameter> AddBodyParameterAsync(OperationProcessorContext context, ExtendedApiParameterDescription extendedApiParameter)
        {
            OpenApiParameter operationParameter;

            var contextualParameterType = extendedApiParameter.ParameterType
                .ToContextualType(extendedApiParameter.Attributes);

            var typeDescription = _settings.ReflectionService.GetDescription(contextualParameterType, _settings);
            var isNullable = _settings.AllowNullableBodyParameters && typeDescription.IsNullable;

            var operation = context.OperationDescription.Operation;
            var parameterType = extendedApiParameter.ParameterType;
            if (parameterType.Name == "XmlDocument" || parameterType.InheritsFromTypeName("XmlDocument", TypeNameStyle.Name))
            {
                operation.TryAddConsumes("application/xml");
                operationParameter = new OpenApiParameter
                {
                    Name = extendedApiParameter.ApiParameter.Name,
                    Kind = OpenApiParameterKind.Body,
                    Schema = new JsonSchema
                    {
                        Type = JsonObjectType.String,
                        IsNullableRaw = isNullable
                    },
                    IsNullableRaw = isNullable,
                    IsRequired = extendedApiParameter.IsRequired(_settings.RequireParametersWithoutDefault),
                    Description = await extendedApiParameter.GetDocumentationAsync().ConfigureAwait(false)
                };
            }
            else if (parameterType.IsAssignableToTypeName("System.IO.Stream", TypeNameStyle.FullName))
            {
                operation.TryAddConsumes("application/octet-stream");
                operationParameter = new OpenApiParameter
                {
                    Name = extendedApiParameter.ApiParameter.Name,
                    Kind = OpenApiParameterKind.Body,
                    Schema = new JsonSchema
                    {
                        Type = JsonObjectType.String,
                        Format = JsonFormatStrings.Byte,
                        IsNullableRaw = isNullable
                    },
                    IsNullableRaw = isNullable,
                    IsRequired = extendedApiParameter.IsRequired(_settings.RequireParametersWithoutDefault),
                    Description = await extendedApiParameter.GetDocumentationAsync().ConfigureAwait(false)
                };
            }
            else // body from type
            {
                operationParameter = new OpenApiParameter
                {
                    Name = extendedApiParameter.ApiParameter.Name,
                    Kind = OpenApiParameterKind.Body,
                    IsRequired = extendedApiParameter.IsRequired(_settings.RequireParametersWithoutDefault),
                    IsNullableRaw = isNullable,
                    Description = await extendedApiParameter.GetDocumentationAsync().ConfigureAwait(false),
                    Schema = await context.SchemaGenerator.GenerateWithReferenceAndNullabilityAsync<JsonSchema>(
                        contextualParameterType, isNullable, schemaResolver: context.SchemaResolver).ConfigureAwait(false)
                };
            }

            operation.Parameters.Add(operationParameter);
            return operationParameter;
        }

        private async Task<OpenApiParameter> CreatePrimitiveParameterAsync(
            OperationProcessorContext context,
            ExtendedApiParameterDescription extendedApiParameter)
        {
            var contextualParameter = extendedApiParameter.ParameterType.ToContextualType(extendedApiParameter.Attributes);

            var description = await extendedApiParameter.GetDocumentationAsync().ConfigureAwait(false);
            var operationParameter = await context.DocumentGenerator.CreatePrimitiveParameterAsync(
                extendedApiParameter.ApiParameter.Name, description, contextualParameter).ConfigureAwait(false);

            if (extendedApiParameter.ParameterInfo?.HasDefaultValue == true)
            {
                var defaultValue = context.SchemaGenerator
                    .ConvertDefaultValue(contextualParameter, extendedApiParameter.ParameterInfo.DefaultValue);

                if (_settings.SchemaType == SchemaType.Swagger2)
                {
                    operationParameter.Default = defaultValue;
                }
                else if (operationParameter.Schema.HasReference)
                {
                    operationParameter.Schema = new JsonSchema
                    {
                        Default = defaultValue,
                        OneOf = { operationParameter.Schema }
                    };
                }
                else
                {
                    operationParameter.Schema.Default = defaultValue;
                }
            }

            operationParameter.IsRequired = extendedApiParameter.IsRequired(_settings.RequireParametersWithoutDefault);
            return operationParameter;
        }

        private void InitializeFileParameter(OpenApiParameter operationParameter, bool isFileArray)
        {
            operationParameter.Type = JsonObjectType.File;
            operationParameter.Kind = OpenApiParameterKind.FormData;

            if (isFileArray)
            {
                operationParameter.CollectionFormat = OpenApiParameterCollectionFormat.Multi;
            }
        }

        private class ExtendedApiParameterDescription
        {
            public ApiParameterDescription ApiParameter { get; set; }

            public ParameterInfo ParameterInfo { get; set; }

            public PropertyInfo PropertyInfo { get; set; }

            public Type ParameterType { get; set; }

            public IEnumerable<Attribute> Attributes { get; set; } = Enumerable.Empty<Attribute>();

            public bool IsRequired(bool requireParametersWithoutDefault)
            {
                var isRequired = false;

                // available in asp.net core >= 2.2
                if (ApiParameter.HasProperty("IsRequired"))
                {
                    isRequired = ApiParameter.TryGetPropertyValue("IsRequired", false);
                }
                else
                {
                    // fallback for asp.net core <= 2.1
                    if (ApiParameter.Source == BindingSource.Body)
                    {
                        isRequired = true;
                    }
                    else if (ApiParameter.ModelMetadata != null &&
                             ApiParameter.ModelMetadata.IsBindingRequired)

                    {
                        isRequired = true;
                    }
                    else if (ApiParameter.Source == BindingSource.Path &&
                             ApiParameter.RouteInfo != null &&
                             ApiParameter.RouteInfo.IsOptional == false)
                    {
                        isRequired = true;
                    }
                }

                return isRequired || (requireParametersWithoutDefault && ParameterInfo?.HasDefaultValue != true);
            }

            public async Task<string> GetDocumentationAsync()
            {
                var parameterDocumentation = string.Empty;
                if (ParameterInfo != null)
                {
                    parameterDocumentation = await ParameterInfo.ToContextualParameter().GetDescriptionAsync().ConfigureAwait(false);
                }
                else if (PropertyInfo != null)
                {
                    parameterDocumentation = await PropertyInfo.ToContextualProperty().GetDescriptionAsync().ConfigureAwait(false);
                }

                return parameterDocumentation;
            }
        }
    }
}
