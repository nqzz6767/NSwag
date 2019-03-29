//-----------------------------------------------------------------------
// <copyright file="OperationModel.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NJsonSchema;
using NJsonSchema.CodeGeneration;

namespace NSwag.CodeGeneration.Models
{
    /// <summary>The Swagger operation template model.</summary>
    public abstract class OperationModelBase<TParameterModel, TResponseModel> : IOperationModel
        where TParameterModel : ParameterModelBase
        where TResponseModel : ResponseModelBase
    {
        private readonly SwaggerOperation _operation;
        private readonly TypeResolverBase _resolver;
        private readonly IClientGenerator _generator;
        private readonly ClientGeneratorBaseSettings _settings;

        /// <summary>Initializes a new instance of the <see cref="OperationModelBase{TParameterModel, TResponseModel}"/> class.</summary>
        /// <param name="exceptionSchema">The exception schema.</param>
        /// <param name="operation">The operation.</param>
        /// <param name="resolver">The resolver.</param>
        /// <param name="generator">The generator.</param>
        /// <param name="settings">The settings.</param>
        protected OperationModelBase(JsonSchema4 exceptionSchema, SwaggerOperation operation, TypeResolverBase resolver, IClientGenerator generator, ClientGeneratorBaseSettings settings)
        {
            _operation = operation;
            _resolver = resolver;
            _generator = generator;
            _settings = settings;

            var responses = _operation.ActualResponses
                .Select(response => CreateResponseModel(operation, response.Key, response.Value, exceptionSchema, generator, resolver, settings))
                .ToList();

            var defaultResponse = responses.SingleOrDefault(r => r.StatusCode == "default");
            if (defaultResponse != null)
                responses.Remove(defaultResponse);

            Responses = responses;
            DefaultResponse = defaultResponse;
        }

        /// <summary>Creates the response model.</summary>
        /// <param name="operation">The operation.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="response">The response.</param>
        /// <param name="exceptionSchema">The exception schema.</param>
        /// <param name="generator">The generator.</param>
        /// <param name="resolver">The resolver.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The response model.</returns>
        protected abstract TResponseModel CreateResponseModel(SwaggerOperation operation, string statusCode, SwaggerResponse response, JsonSchema4 exceptionSchema, IClientGenerator generator,
            TypeResolverBase resolver, ClientGeneratorBaseSettings settings);

        /// <summary>Gets the operation ID.</summary>
        public string Id => _operation.OperationId;

        /// <summary>Gets or sets the HTTP path (i.e. the absolute route).</summary>
        public string Path { get; set; }

        /// <summary>Gets or sets the HTTP method.</summary>
        public string HttpMethod { get; set; }

        /// <summary>Gets or sets the name of the operation.</summary>
        public string OperationName { get; set; }

        /// <summary>Gets the actual name of the operation (language specific).</summary>
        public abstract string ActualOperationName { get; }

        /// <summary>Gets the HTTP method in uppercase.</summary>
        public string HttpMethodUpper => ConversionUtilities.ConvertToUpperCamelCase(HttpMethod.ToString(), false);

        /// <summary>Gets the HTTP method in lowercase.</summary>
        public string HttpMethodLower => ConversionUtilities.ConvertToLowerCamelCase(HttpMethod.ToString(), false);

        /// <summary>Gets a value indicating whether the HTTP method is GET or DELETE or HEAD.</summary>
        public bool IsGetOrDeleteOrHead =>
            HttpMethod == SwaggerOperationMethod.Get ||
            HttpMethod == SwaggerOperationMethod.Delete ||
            HttpMethod == SwaggerOperationMethod.Head;

        /// <summary>Gets a value indicating whether the HTTP method is GET or HEAD.</summary>
        public bool IsGetOrHead => HttpMethod == SwaggerOperationMethod.Get || HttpMethod == SwaggerOperationMethod.Head;

        // TODO: Remove this (may not work correctly)
        /// <summary>Gets or sets a value indicating whether the operation has a result type (i.e. not void).</summary>
        public bool HasResultType => GetSuccessResponse().Value?.IsEmpty(_operation) == false;

        /// <summary>Gets or sets the type of the result.</summary>
        public abstract string ResultType { get; }

        /// <summary>Gets the type of the unwrapped result type (without Task).</summary>
        public string UnwrappedResultType
        {
            get
            {
                var response = GetSuccessResponse();
                if (response.Value == null || response.Value.IsEmpty(_operation))
                {
                    return "void";
                }

                if (response.Value.IsBinary(_operation) == true)
                {
                    return _generator.GetBinaryResponseTypeName();
                }

                var isNullable = response.Value.IsNullable(_settings.CodeGeneratorSettings.SchemaType);
                return _generator.GetTypeName(response.Value.Schema, isNullable, !response.Value.Schema.HasTypeNameTitle ? "Response" : null);
            }
        }

        /// <summary>Gets a value indicating whether the result has description.</summary>
        public bool HasResultDescription => !string.IsNullOrEmpty(ResultDescription);

        /// <summary>Gets or sets the result description.</summary>
        public string ResultDescription
        {
            get
            {
                var response = GetSuccessResponse();
                if (response.Value != null)
                {
                    return ConversionUtilities.TrimWhiteSpaces(response.Value.Description);
                }

                return null;
            }
        }

        /// <summary>Gets the name of the controller.</summary>
        public string ControllerName { get; set; }

        /// <summary>Gets or sets the type of the exception.</summary>
        public abstract string ExceptionType { get; }

        /// <summary>Gets or sets the responses.</summary>
        public List<TResponseModel> Responses { get; }

        /// <summary>Gets a value indicating whether the operation has default response.</summary>
        public bool HasDefaultResponse => DefaultResponse != null;

        /// <summary>Gets or sets the default response.</summary>
        public TResponseModel DefaultResponse { get; }

        /// <summary>Gets a value indicating whether the operation has an explicit success response defined.</summary>
        public bool HasSuccessResponse => Responses.Any(r => r.IsSuccess);

        /// <summary>Gets the success response.</summary>
        public TResponseModel SuccessResponse => Responses.FirstOrDefault(r => r.IsSuccess);

        /// <summary>Gets the responses.</summary>
        IEnumerable<ResponseModelBase> IOperationModel.Responses => Responses;

        /// <summary>Gets or sets the parameters.</summary>
        public IList<TParameterModel> Parameters { get; protected set; }

        /// <summary>Gets a value indicating whether the operation has only a default response.</summary>
        public bool HasOnlyDefaultResponse => Responses.Count == 0 && HasDefaultResponse;

        /// <summary>Gets a value indicating whether the operation has content parameter.</summary>
        public bool HasContent => ContentParameter != null;

        /// <summary>Gets a value indicating whether the the request has a body.</summary>
        public bool HasBody => HasContent || HasFormParameters;

        /// <summary>Gets the content parameter.</summary>
        /// <exception cref="InvalidOperationException" accessor="get">Multiple body parameters found in operation.</exception>
        public TParameterModel ContentParameter
        {
            get
            {
                if (Parameters.Count(p => p.Kind == SwaggerParameterKind.Body) > 1)
                    throw new InvalidOperationException("Multiple body parameters found in operation '" + _operation.OperationId + "'.");

                return Parameters.SingleOrDefault(p => p.Kind == SwaggerParameterKind.Body);
            }
        }

        /// <summary>Gets the path parameters.</summary>
        public IEnumerable<TParameterModel> PathParameters => Parameters.Where(p => p.Kind == SwaggerParameterKind.Path);

        /// <summary>Gets the query parameters.</summary>
        public IEnumerable<TParameterModel> QueryParameters => Parameters.Where(p => p.Kind == SwaggerParameterKind.Query || p.Kind == SwaggerParameterKind.ModelBinding);

        /// <summary>Gets a value indicating whether the operation has query parameters.</summary>
        public bool HasQueryParameters => QueryParameters.Any();

        /// <summary>Gets the header parameters.</summary>
        public IEnumerable<TParameterModel> HeaderParameters => Parameters.Where(p => p.Kind == SwaggerParameterKind.Header);

        /// <summary>Gets or sets a value indicating whether the accept header is defined in a parameter.</summary>
        public bool HasAcceptHeaderParameterParameter => HeaderParameters.Any(p => p.Name.ToLowerInvariant() == "accept");

        /// <summary>Gets a value indicating whether the operation has form parameters.</summary>
        public bool HasFormParameters => _operation.ActualParameters.Any(p => p.Kind == SwaggerParameterKind.FormData);

        /// <summary>Gets a value indicating whether the operation consumes 'application/x-www-form-urlencoded'.</summary>
        public bool ConsumesFormUrlEncoded =>
            _operation.ActualConsumes?.Any(c => c == "application/x-www-form-urlencoded") == true ||
            _operation.RequestBody?.Content.Any(mt => mt.Key == "application/x-www-form-urlencoded") == true;

        /// <summary>Gets the form parameters.</summary>
        public IEnumerable<TParameterModel> FormParameters => Parameters.Where(p => p.Kind == SwaggerParameterKind.FormData);

        /// <summary>Gets a value indicating whether the operation has summary.</summary>
        public bool HasSummary => !string.IsNullOrEmpty(Summary);

        /// <summary>Gets the summary text.</summary>
        public string Summary => ConversionUtilities.TrimWhiteSpaces(_operation.Summary);

        /// <summary>Gets a value indicating whether the operation has any documentation.</summary>
        public bool HasDocumentation => HasSummary || HasResultDescription || Parameters.Any(p => p.HasDescription) || _operation.IsDeprecated;

        /// <summary>Gets a value indicating whether the operation is deprecated.</summary>
        public bool IsDeprecated => _operation.IsDeprecated;

        /// <summary>Gets or sets a value indicating whether this operation has an XML body parameter.</summary>
        public bool HasXmlBodyParameter => _operation.ActualParameters.Any(p => p.IsXmlBodyParameter);

        /// <summary>Gets or sets a value indicating whether this operation has an binary body parameter.</summary>
        public bool HasBinaryBodyParameter => _operation.ActualParameters.Any(p => p.IsBinaryBodyParameter);

        /// <summary>Gets the mime type of the request body.</summary>
        public string Consumes
        {
            get
            {
                if (_operation.ActualConsumes?.Contains("application/json") == true)
                    return "application/json";

                return _operation.ActualConsumes?.FirstOrDefault() ?? "application/json";
            }
        }

        /// <summary>Gets the mime type of the response body.</summary>
        public string Produces
        {
            get
            {
                if (_operation.ActualProduces?.Contains("application/json") == true)
                    return "application/json";

                return _operation.ActualProduces?.FirstOrDefault() ?? "application/json";
            }
        }

        /// <summary>Gets a value indicating whether a file response is expected from one of the responses.</summary>
        public bool IsFile => _operation.ActualResponses.Any(r => r.Value.Schema?.ActualSchema.IsBinary == true); // TODO: Use response.IsBinary directly

        /// <summary>Gets a value indicating whether to wrap the response of this operation.</summary>
        public bool WrapResponse => _settings.WrapResponses && (
                                    _settings.WrapResponseMethods == null ||
                                    _settings.WrapResponseMethods.Length == 0 ||
                                    _settings.WrapResponseMethods.Contains(_settings.GenerateControllerName(ControllerName) + "." + ActualOperationName));

        /// <summary>Gets the operation extension data.</summary>
        public IDictionary<string, object> ExtensionData => _operation.ExtensionData;

        /// <summary>Gets the success response.</summary>
        /// <returns>The response.</returns>
        protected KeyValuePair<string, SwaggerResponse> GetSuccessResponse()
        {
            if (_operation.ActualResponses.Any(r => r.Key == "200"))
                return new KeyValuePair<string, SwaggerResponse>("200", _operation.ActualResponses.Single(r => r.Key == "200").Value);

            var response = _operation.ActualResponses.FirstOrDefault(r => HttpUtilities.IsSuccessStatusCode(r.Key));
            if (response.Value != null)
                return new KeyValuePair<string, SwaggerResponse>(response.Key, response.Value);

            return new KeyValuePair<string, SwaggerResponse>("default", _operation.ActualResponses.FirstOrDefault(r => r.Key == "default").Value);
        }

        /// <summary>Gets the name of the parameter variable.</summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="allParameters">All parameters.</param>
        /// <returns>The parameter variable name.</returns>
        protected virtual string GetParameterVariableName(SwaggerParameter parameter, IEnumerable<SwaggerParameter> allParameters)
        {
            return _settings.ParameterNameGenerator.Generate(parameter, allParameters);
        }

        /// <summary>Resolves the type of the parameter.</summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The parameter type name.</returns>
        protected virtual string ResolveParameterType(SwaggerParameter parameter)
        {
            var schema = parameter.ActualSchema;

            if (parameter.IsXmlBodyParameter)
            {
                return "string";
            }

            if (parameter.CollectionFormat == SwaggerParameterCollectionFormat.Multi && !schema.Type.HasFlag(JsonObjectType.Array))
            {
                schema = new JsonSchema4 { Type = JsonObjectType.Array, Item = schema };
            }

            var typeNameHint = !schema.HasTypeNameTitle ? ConversionUtilities.ConvertToUpperCamelCase(parameter.Name, true) : null;
            var isNullable = parameter.IsRequired == false || parameter.IsNullable(_settings.CodeGeneratorSettings.SchemaType);
            return _resolver.Resolve(schema, isNullable, typeNameHint);
        }
    }
};