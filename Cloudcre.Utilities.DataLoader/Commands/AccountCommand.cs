using Cloudcre.Utilities.Console;
using Cloudcre.Test.DataLoader.Marshallers;

namespace Cloudcre.Test.DataLoader.Commands
{
    public class AccountCommand : EnvironmentCommand
    {
        private const string DataPath = @"C:\Whitney\whitneycomp\TestData\useraccountdata.csv";

        /// <summary>
        /// Command to load account data
        /// </summary>
        /// <example>
        /// Example usage:
        /// 
        /// Cloudcre.Test.DataLoader.exe load-accounts -e development
        ///
        /// </example>
        public AccountCommand()
        {
            IsCommand("load-accounts", "Upload user account data");
        }

        public override void Execute(string[] remainingArguments)
        {
            var marshaller = new AccountMarshaller(EnvironmentType);

            marshaller.Load(DataPath);
        }
    }
}