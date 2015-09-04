using System.ComponentModel;
using System.Threading.Tasks;
using NConsole;
using NSwag.CodeGeneration.ClientGenerators.TypeScript;

namespace NSwag.Console.Commands
{
    [Description("Generates the TypeScript client code.")]
    public class TypeScriptCommand : InputOutputCommandBase
    {
        [Argument(Name = "Class", DefaultValue = "Client")]
        public string Class { get; set; }

        public override async Task RunAsync(CommandLineProcessor processor, IConsoleHost host)
        {
            var clientGenerator = new SwaggerToTypeScriptGenerator(InputSwaggerService);
            clientGenerator.Class = Class;

            var output = clientGenerator.GenerateFile();
            WriteOutput(host, output);
        }
    }
}