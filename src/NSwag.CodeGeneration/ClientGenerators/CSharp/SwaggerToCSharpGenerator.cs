﻿//-----------------------------------------------------------------------
// <copyright file="SwaggerToCSharpGenerator.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using NJsonSchema;
using NSwag.CodeGeneration.ClientGenerators.Models;

namespace NSwag.CodeGeneration.ClientGenerators.CSharp
{
    /// <summary>Generates the CSharp service client code. </summary>
    public class SwaggerToCSharpGenerator : ClientGeneratorBase
    {
        private readonly SwaggerService _service;
        private readonly SwaggerToCSharpTypeResolver _resolver;

        /// <summary>Initializes a new instance of the <see cref="SwaggerToCSharpGenerator"/> class.</summary>
        /// <param name="service">The service.</param>
        /// <exception cref="ArgumentNullException"><paramref name="service"/> is <see langword="null" />.</exception>
        public SwaggerToCSharpGenerator(SwaggerService service)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            _service = service;

            foreach (var definition in _service.Definitions)
                definition.Value.TypeName = definition.Key;

            _resolver = new SwaggerToCSharpTypeResolver(_service.Definitions);
        }

        /// <summary>Gets or sets the class name of the service client.</summary>
        public string Class { get; set; }

        /// <summary>Gets or sets the namespace.</summary>
        public string Namespace { get; set; }

        /// <summary>Gets or sets a value indicating whether to only generate the client classes (no DTO classes).</summary>
        public bool GenerateClientClassesOnly { get; set; }

        /// <summary>Gets the language.</summary>
        protected override string Language
        {
            get { return "CSharp"; }
        }

        /// <summary>Generates the file.</summary>
        /// <returns>The file contents.</returns>
        public override string GenerateFile()
        {
            return GenerateFile(_service, _resolver);
        }
        
        internal override string RenderFile(string clientCode)
        {
            var template = LoadTemplate("File");
            template.Add("namespace", Namespace);
            template.Add("toolchain", SwaggerService.ToolchainVersion);
            template.Add("clients", clientCode);
            template.Add("classes", !GenerateClientClassesOnly ? _resolver.GenerateTypes() : string.Empty);
            return template.Render();
        }

        internal override string RenderClientCode(string controllerName, IEnumerable<OperationModel> operations)
        {
            var template = LoadTemplate("Client");
            template.Add("class", Class.Replace("{controller}", ConvertToUpperStartIdentifier(controllerName)));
            template.Add("baseUrl", _service.BaseUrl);
            template.Add("operations", operations);
            template.Add("hasOperations", operations.Any());
            return template.Render();
        }

        internal override string GetExceptionType(SwaggerOperation operation)
        {
            if (operation.Responses.Count(r => r.Key != "200") != 1)
                return "Exception";

            return GetType(operation.Responses.Single(r => r.Key != "200").Value.Schema, "Exception");
        }

        internal override string GetResultType(SwaggerOperation operation)
        {
            if (operation.Responses.Count(r => r.Key == "200") == 0)
                return "Task";

            var response = GetOkResponse(operation);
            return "Task<" + GetType(response.Schema, "Response") + ">";
        }

        internal override string GetType(JsonSchema4 schema, string typeNameHint)
        {
            if (schema == null)
                return "string";

            if (schema.ActualSchema.IsAnyType)
                return "object";

            return _resolver.Resolve(schema.ActualSchema, true, typeNameHint);
        }
    }
}
