using System.Collections.Generic;
using Cloudcre.Infrastructure.Configuration;

namespace Cloudcre.Utilities.Console
{
    public static class EnvironmentContext
    {
        private static readonly Dictionary<Type, EnvironmentConfigurationProvider> Envs;
        private static readonly IConfigurationManager ConfigurationManager;

        static EnvironmentContext()
        {
            Envs = new Dictionary<Type, EnvironmentConfigurationProvider>();
            ConfigurationManager = new WebConfigurationManagerAdapter();

            Envs.Add(Type.Development, new DevelopmentEnvironmentConfigurationProvider(ConfigurationManager));
            Envs.Add(Type.QualityAssurance, new QualityAssuranceEnvironmentConfigurationProvider(ConfigurationManager));
            Envs.Add(Type.Staging, new StagingEnvironmentConfigurationProvider(ConfigurationManager));
            Envs.Add(Type.Production, new ProductionEnvironmentConfigurationProvider(ConfigurationManager));
            Envs.Add(Type.Demo, new DemoEnvironmentConfigurationProvider(ConfigurationManager));
        }

        public enum Type
        {
            Development = 0,
            QualityAssurance = 1,
            Staging = 2,
            Production = 3,
            Demo = 4
        }

        public static string ConnectionString(Type environment)
        {
            return ConfigurationManager.ConnectionStrings[Envs[environment].ConnectionString].ConnectionString;
        }

        public static string LuceneIndexRootDirectory(Type environment)
        {
            return Envs[environment].LuceneIndexRootDirectory;
        }
    }
}