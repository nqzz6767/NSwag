﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 14.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace NSwag.CodeGeneration.CodeGenerators.CSharp.Templates
{
    using NJsonSchema;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    internal partial class ControllerTemplate : ControllerTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\r\n[System.CodeDom.Compiler.GeneratedCode(\"NSwag\", \"");
            
            #line 4 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(SwaggerDocument.ToolchainVersion));
            
            #line default
            #line hidden
            this.Write("\")]\r\npublic interface I");
            
            #line 5 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.Class));
            
            #line default
            #line hidden
            this.Write("Controller\r\n{\r\n");
            
            #line 7 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
foreach(var operation in Model.Operations){
            
            #line default
            #line hidden
            
            #line 8 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
  if(operation.HasSummary){
            
            #line default
            #line hidden
            this.Write("    /// <summary>");
            
            #line 8 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ConversionUtilities.ConvertCSharpDocBreaks(operation.Summary, 1)));
            
            #line default
            #line hidden
            this.Write("</summary>\r\n");
            
            #line 9 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
  }foreach (var parameter in operation.Parameters){
            
            #line default
            #line hidden
            
            #line 10 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
      if(parameter.HasDescription){
            
            #line default
            #line hidden
            this.Write("    /// <param name=\"");
            
            #line 10 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(parameter.VariableName));
            
            #line default
            #line hidden
            this.Write("\">");
            
            #line 10 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ConversionUtilities.ConvertCSharpDocBreaks(parameter.Description, 1)));
            
            #line default
            #line hidden
            this.Write("</param>\r\n");
            
            #line 11 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
  }}if(operation.HasResultDescription){
            
            #line default
            #line hidden
            this.Write("    /// <returns>");
            
            #line 11 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ConversionUtilities.ConvertCSharpDocBreaks(operation.ResultDescription, 1)));
            
            #line default
            #line hidden
            this.Write("</returns>\r\n");
            
            #line 12 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
  }if(operation.IsDeprecated){
            
            #line default
            #line hidden
            this.Write("    [System.Obsolete]\r\n");
            
            #line 13 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
  }
            
            #line default
            #line hidden
            this.Write("    ");
            
            #line 13 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.ResultType));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 13 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.OperationNameUpper));
            
            #line default
            #line hidden
            this.Write("Async(");
            
            #line 13 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
foreach(var parameter in operation.Parameters){
            
            #line default
            #line hidden
            
            #line 13 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(parameter.Type));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 13 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(parameter.VariableName));
            
            #line default
            #line hidden
            
            #line 13 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
if(!parameter.IsLast){
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 13 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
}}
            
            #line default
            #line hidden
            this.Write(");\r\n\r\n");
            
            #line 15 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write("}\r\n\r\n[System.CodeDom.Compiler.GeneratedCode(\"NSwag\", \"");
            
            #line 18 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(SwaggerDocument.ToolchainVersion));
            
            #line default
            #line hidden
            this.Write("\")]\r\n");
            
            #line 19 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
if(Model.HasBasePath){
            
            #line default
            #line hidden
            this.Write("[System.Web.Http.RoutePrefix(\"");
            
            #line 20 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.BasePath));
            
            #line default
            #line hidden
            this.Write("\")]\r\n");
            
            #line 21 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write("public partial class ");
            
            #line 22 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.Class));
            
            #line default
            #line hidden
            this.Write("Controller : ");
            
            #line 22 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
if(Model.HasBaseClass){
            
            #line default
            #line hidden
            
            #line 22 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.BaseClass));
            
            #line default
            #line hidden
            
            #line 22 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
}else{
            
            #line default
            #line hidden
            this.Write("System.Web.Http.ApiController");
            
            #line 22 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write(" \r\n{\r\n    private I");
            
            #line 24 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.Class));
            
            #line default
            #line hidden
            this.Write("Controller _implementation; \r\n\r\n    public ");
            
            #line 26 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.Class));
            
            #line default
            #line hidden
            this.Write("Controller(I");
            
            #line 26 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.Class));
            
            #line default
            #line hidden
            this.Write("Controller implementation)\r\n    {\r\n        _implementation = implementation; \r\n  " +
                    "  }\r\n");
            
            #line 30 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
foreach(var operation in Model.Operations){
            
            #line default
            #line hidden
            
            #line 31 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
  if(operation.HasSummary){
            
            #line default
            #line hidden
            this.Write("    /// <summary>");
            
            #line 31 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ConversionUtilities.ConvertCSharpDocBreaks(operation.Summary, 1)));
            
            #line default
            #line hidden
            this.Write("</summary>\r\n");
            
            #line 32 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
  }foreach (var parameter in operation.Parameters){
            
            #line default
            #line hidden
            
            #line 33 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
      if(parameter.HasDescription){
            
            #line default
            #line hidden
            this.Write("    /// <param name=\"");
            
            #line 33 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(parameter.VariableName));
            
            #line default
            #line hidden
            this.Write("\">");
            
            #line 33 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ConversionUtilities.ConvertCSharpDocBreaks(parameter.Description, 1)));
            
            #line default
            #line hidden
            this.Write("</param>\r\n");
            
            #line 34 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
  }}if(operation.HasResultDescription){
            
            #line default
            #line hidden
            this.Write("    /// <returns>");
            
            #line 34 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ConversionUtilities.ConvertCSharpDocBreaks(operation.ResultDescription, 1)));
            
            #line default
            #line hidden
            this.Write("</returns>\r\n");
            
            #line 35 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
  }if(operation.IsDeprecated){
            
            #line default
            #line hidden
            this.Write("    [System.Obsolete]\r\n");
            
            #line 36 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
  }
            
            #line default
            #line hidden
            this.Write("    [System.Web.Http.Http");
            
            #line 36 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.HttpMethodUpper));
            
            #line default
            #line hidden
            this.Write(", System.Web.Http.Route(\"");
            
            #line 36 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.Path));
            
            #line default
            #line hidden
            this.Write("\")]\r\n    public ");
            
            #line 37 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.ResultType));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 37 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.OperationNameUpper));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 37 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
foreach(var parameter in operation.Parameters){
            
            #line default
            #line hidden
            
            #line 37 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(parameter.Type));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 37 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(parameter.VariableName));
            
            #line default
            #line hidden
            
            #line 37 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
if(!parameter.IsLast){
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 37 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
}}
            
            #line default
            #line hidden
            this.Write(")\r\n    {\r\n        ");
            
            #line 39 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
if(operation.UnwrappedResultType != "void"){
            
            #line default
            #line hidden
            this.Write("return ");
            
            #line 39 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write("_implementation.");
            
            #line 39 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(operation.OperationNameUpper));
            
            #line default
            #line hidden
            this.Write("Async(");
            
            #line 39 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
foreach(var parameter in operation.Parameters){
            
            #line default
            #line hidden
            
            #line 39 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(parameter.VariableName));
            
            #line default
            #line hidden
            
            #line 39 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
if(!parameter.IsLast){
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 39 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
}}
            
            #line default
            #line hidden
            this.Write(");\r\n    }\r\n\r\n");
            
            #line 42 "C:\Data\Projects\NSwag\src\NSwag.CodeGeneration\CodeGenerators\CSharp\Templates\ControllerTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write("}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    internal class ControllerTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
