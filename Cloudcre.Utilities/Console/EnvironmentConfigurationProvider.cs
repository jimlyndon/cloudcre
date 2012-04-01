using System;
using System.Collections.Specialized;
using System.Configuration;
using Cloudcre.Infrastructure.Configuration;

namespace Cloudcre.Utilities.Console
{
    public class DevelopmentEnvironmentConfigurationProvider : EnvironmentConfigurationProvider
    {
        public DevelopmentEnvironmentConfigurationProvider(IConfigurationManager configurationManager)
            : base("developmentEnvironmentSection", configurationManager)
        {

        }
    }

    public class QualityAssuranceEnvironmentConfigurationProvider : EnvironmentConfigurationProvider
    {
        public QualityAssuranceEnvironmentConfigurationProvider(IConfigurationManager configurationManager)
            : base("qualityassuranceEnvironmentSection", configurationManager)
        {

        }
    }

    public class StagingEnvironmentConfigurationProvider : EnvironmentConfigurationProvider
    {
        public StagingEnvironmentConfigurationProvider(IConfigurationManager configurationManager)
            : base("stagingEnvironmentSection", configurationManager)
        {

        }
    }

    public class ProductionEnvironmentConfigurationProvider : EnvironmentConfigurationProvider
    {
        public ProductionEnvironmentConfigurationProvider(IConfigurationManager configurationManager)
            : base("productionEnvironmentSection", configurationManager)
        {

        }
    }

    public class DemoEnvironmentConfigurationProvider : EnvironmentConfigurationProvider
    {
        public DemoEnvironmentConfigurationProvider(IConfigurationManager configurationManager)
            : base("demoEnvironmentSection", configurationManager)
        {

        }
    }

    public class EnvironmentConfigurationProvider
    {
        private readonly IConfigurationManager _configurationManager;

        private readonly string _sectionIdentifier;

        public EnvironmentConfigurationProvider(string sectionIdentifier, IConfigurationManager configurationManager)
        {
            _sectionIdentifier = sectionIdentifier;
            _configurationManager = configurationManager;
        }

        public NameValueCollection AppSettings
        {
            get
            {
                return _configurationManager.AppSettings;
            }
        }

        public string ConnectionString
        {
            get
            {
                return _configurationManager
                    .GetSection<EnvironmentSectionProvider>(_sectionIdentifier)
                    .EnvironmentSettings["ConnectionString"].Value;
            }
        }

        public string LuceneIndexRootDirectory
        {
            get
            {
                return _configurationManager
                    .GetSection<EnvironmentSectionProvider>(_sectionIdentifier)
                    .EnvironmentSettings["LuceneIndexRootDirectory"].Value;
            }
        }
    }

    public class EnvironmentSectionProvider : ConfigurationSection
    {
        [ConfigurationProperty("environmentSettings", IsRequired = true)]
        public EnvironmentElementCollection EnvironmentSettings
        {
            get
            { return (EnvironmentElementCollection)this["environmentSettings"]; }
            set
            { this["environmentSettings"] = value; }
        }

        [ConfigurationCollection(typeof(EnvironmentElement), AddItemName = "environmentElement")]
        public class EnvironmentElementCollection : ConfigurationElementCollection
        {
            protected override ConfigurationElement CreateNewElement()
            {
                return new EnvironmentElement();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((EnvironmentElement)element).Key;
            }

            public void Add(EnvironmentElement element)
            {
                BaseAdd(element);
            }

            public void Remove(string key)
            {
                BaseRemove(key);
            }

            public void Clear()
            {
                BaseClear();
            }

            public new EnvironmentElement this[String key]
            {
                get
                {
                    var element = (EnvironmentElement)BaseGet(key);
                    if (element == null)
                        return new EnvironmentElement(key);

                    return (EnvironmentElement)BaseGet(key);
                }
            }
        }

        public class EnvironmentElement : ConfigurationElement
        {
            public EnvironmentElement()
            {
            }

            public EnvironmentElement(string key)
            {
                Key = key;
            }

            public EnvironmentElement(string key, string value)
            {
                Key = key;
                Value = value;
            }

            [ConfigurationProperty("key", DefaultValue = "", IsRequired = true)]
            public String Key
            {
                get
                { return (String)this["key"]; }
                set
                { this["key"] = value; }
            }

            [ConfigurationProperty("value", DefaultValue = "", IsRequired = true)]
            public String Value
            {
                get
                { return (String)this["value"]; }
                set
                { this["value"] = value; }
            }
        }
    }
}
