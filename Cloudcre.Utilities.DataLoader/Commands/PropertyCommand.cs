using Cloudcre.Test.DataLoader.Marshallers;
using Cloudcre.Utilities.Console;

namespace Cloudcre.Test.DataLoader.Commands
{
    public class PropertyCommand : EnvironmentCommand
    {
        private const string DataPath = @"C:\Whitney\whitneycomp\TestData\propertyrecorddata.csv";

        /// <summary>
        /// Command to load property data
        /// </summary>
        /// <example>
        /// Example usage:
        /// 
        /// Cloudcre.Test.DataLoader.exe load-properties -e development
        ///
        /// </example>
        public PropertyCommand()
        {
            IsCommand("load-properties", "Upload user account data");
        }

        public override void Execute(string[] remainingArguments)
        {
            var marshaller = new PropertyMarshaller(EnvironmentType);

            marshaller.Load(DataPath);
        }
    }
}