﻿//-----------------------------------------------------------------------
// <copyright file="SwaggerPathItem.cs" company="NSwag">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/NSwag/NSwag/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using NJsonSchema;
using NJsonSchema.References;
using NSwag.Collections;

namespace NSwag
{
    /// <summary>A Swagger path, the key is usually a value of <see cref="SwaggerOperationMethod"/>.</summary>
    [JsonConverter(typeof(SwaggerPathItemConverter))]
    public class SwaggerPathItem : ObservableDictionary<string, SwaggerOperation>, IJsonReferenceBase, IJsonReference
    {
        /// <summary>Initializes a new instance of the <see cref="SwaggerPathItem"/> class.</summary>
        public SwaggerPathItem()
        {
            CollectionChanged += (sender, args) =>
            {
                foreach (var operation in Values)
                    operation.Parent = this;
            };
        }

        /// <summary>Gets the parent <see cref="SwaggerDocument"/>.</summary>
        [JsonIgnore]
        public SwaggerDocument Parent { get; internal set; }

        /// <summary>Gets the actual response, either this or the referenced response.</summary>
        [JsonIgnore]
        public SwaggerPathItem ActualPathItem => Reference ?? this;

        /// <summary>Gets or sets the summary (OpenApi only).</summary>
        [JsonProperty(PropertyName = "summary", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Summary { get; set; }

        /// <summary>Gets or sets the description (OpenApi only).</summary>
        [JsonProperty(PropertyName = "description", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Description { get; set; }

        /// <summary>Gets or sets the servers (OpenAPI only).</summary>
        [JsonProperty(PropertyName = "servers", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public ICollection<OpenApiServer> Servers { get; set; } = new Collection<OpenApiServer>();

        /// <summary>Gets or sets the parameters.</summary>
        [JsonProperty(PropertyName = "parameters", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ICollection<SwaggerParameter> Parameters { get; set; } = new Collection<SwaggerParameter>();

        #region Implementation of IJsonReferenceBase and IJsonReference

        private SwaggerPathItem _reference;

        /// <summary>Gets the document path (URI or file path) for resolving relative references.</summary>
        [JsonIgnore]
        public string DocumentPath { get; set; }

        /// <summary>Gets or sets the type reference path ($ref). </summary>
        [JsonProperty(JsonPathUtilities.ReferenceReplaceString, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        string IJsonReferenceBase.ReferencePath { get; set; }

        /// <summary>Gets or sets the referenced object.</summary>
        [JsonIgnore]
        internal virtual SwaggerPathItem Reference
        {
            get => _reference;
            set
            {
                if (_reference != value)
                {
                    _reference = value;
                    ((IJsonReferenceBase)this).ReferencePath = null;
                }
            }
        }

        /// <summary>Gets or sets the referenced object.</summary>
        [JsonIgnore]
        IJsonReference IJsonReferenceBase.Reference
        {
            get => Reference;
            set => Reference = (SwaggerPathItem)value;
        }

        [JsonIgnore]
        IJsonReference IJsonReference.ActualObject => ActualPathItem;

        [JsonIgnore]
        object IJsonReference.PossibleRoot => Parent;

        #endregion

        // Needed to convert dictionary keys to lower case
        internal class SwaggerPathItemConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var operations = (SwaggerPathItem)value;
                writer.WriteStartObject();

                if (operations.Parameters != null && operations.Parameters.Any())
                {
                    writer.WritePropertyName("parameters");
                    serializer.Serialize(writer, operations.Parameters);
                }

                foreach (var pair in operations)
                {
                    writer.WritePropertyName(pair.Key.ToString().ToLowerInvariant());
                    serializer.Serialize(writer, pair.Value);
                }
                writer.WriteEndObject();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                    return null;

                var operations = new SwaggerPathItem();
                while (reader.Read() && reader.TokenType == JsonToken.PropertyName)
                {
                    var propertyName = reader.Value.ToString();
                    reader.Read();

                    if (propertyName == "parameters")
                        operations.Parameters = (List<SwaggerParameter>)serializer.Deserialize(reader, typeof(List<SwaggerParameter>));
                    else
                    {
                        var value = (SwaggerOperation)serializer.Deserialize(reader, typeof(SwaggerOperation));
                        operations.Add(propertyName, value);
                    }

                }
                return operations;
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(SwaggerPathItem);
            }
        }
    }
}