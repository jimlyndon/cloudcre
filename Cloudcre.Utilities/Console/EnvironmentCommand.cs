using ManyConsole;
using NDesk.Options;

namespace Cloudcre.Utilities.Console
{
    public abstract class EnvironmentCommand : ConsoleCommand
    {
        protected EnvironmentContext.Type EnvironmentType { get; private set; }

        public string EnvironmentParam;

        protected EnvironmentCommand()
        {
            Options = new OptionSet
                          {
                              { "e|environment=", "Environment to run in. Eg., development, staging, etc.", s => EnvironmentParam = s }
                          };
        }

        public override int Run(string[] remainingArguments)
        {
            SetEnvironment();

            Execute(remainingArguments);

            return 0;
        }

        public abstract void Execute(string[] remainingArguments);

        private void SetEnvironment()
        {
            switch (EnvironmentParam)
            {
                case "development":
                    EnvironmentType = EnvironmentContext.Type.Development;
                    break;
                case "qualityassurance":
                    EnvironmentType = EnvironmentContext.Type.QualityAssurance;
                    break;
                case "staging":
                    EnvironmentType = EnvironmentContext.Type.Staging;
                    break;
                case "production":
                    EnvironmentType = EnvironmentContext.Type.Production;
                    break;
                case "demo":
                    EnvironmentType = EnvironmentContext.Type.Demo;
                    break;
                default:
                    throw new ConsoleHelpAsException("Usage: [EXECUTABLE] [COMMAND] -e [ENVIRONMENT]...");
            }
        }
    }
}