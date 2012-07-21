using System.Configuration;
using Cloudcre.Utilities.Console;
using Cloudcre.Utilities.DataLoader.Marshallers;

namespace Cloudcre.Utilities.DataLoader.Commands
{
    public class PropertyCommand : EnvironmentCommand
    {
        /// <summary>
        /// Command to load property data
        /// </summary>
        /// <example>
        /// Example usage:
        /// 
        /// Cloudcre.Utilities.DataLoader.exe load-properties -e development
        ///
        /// </example>
        public PropertyCommand()
        {
            IsCommand("load-properties", "Upload user account data");
        }

        public override void Execute(string[] remainingArguments)
        {
            var marshaller = new PropertyMarshaller(EnvironmentType);

            marshaller.Load(ConfigurationManager.AppSettings["PropertyDataLoaderPath"]);
        }
    }
}