using System.Configuration;
using Cloudcre.Utilities.Console;
using Cloudcre.Utilities.DataLoader.Marshallers;

namespace Cloudcre.Utilities.DataLoader.Commands
{
    public class AccountCommand : EnvironmentCommand
    {
        /// <summary>
        /// Command to load account data
        /// </summary>
        /// <example>
        /// Example usage:
        /// 
        /// Cloudcre.Utilities.DataLoader.exe load-accounts -e development
        ///
        /// </example>
        public AccountCommand()
        {
            IsCommand("load-accounts", "Upload user account data");
        }

        public override void Execute(string[] remainingArguments)
        {
            var marshaller = new AccountMarshaller(EnvironmentType);

            marshaller.Load(ConfigurationManager.AppSettings["AccountDataLoaderPath"]);
        }
    }
}