using System;
using System.Collections.Specialized;
using System.Configuration;
using Cloudcre.Infrastructure.Configuration;

namespace Cloudcre.Web.Configuration
{
    public class PropertyControllerConfigurationProvider
    {
        private readonly IConfigurationManager _configurationManager;

        public PropertyControllerConfigurationProvider(IConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public NameValueCollection AppSettings
        {
            get
            {
                return _configurationManager.AppSettings;
            }
        }

        public string NumberOfResultsPerPage
        {
            get
            {
                return _configurationManager
                    .GetSection<PropertyControllerSectionProvider>("propertyControllerSection")
                    .PropertyControllerSettings["NumberOfResultsPerPage"].Value;
            }
        }
    }

    public class PropertyControllerSectionProvider : ConfigurationSection
    {
        [ConfigurationProperty("global")]
        public bool Global
        {
            get { return (bool)this["global"]; }
        }

        [ConfigurationProperty("propertyControllerSettings", IsRequired = true)]
        public PropertyControllerElementCollection PropertyControllerSettings
        {
            get
            { return (PropertyControllerElementCollection)this["propertyControllerSettings"]; }
            set
            { this["propertyControllerSettings"] = value; }
        }

        [ConfigurationCollection(typeof(PropertyControllerElement), AddItemName = "propertyControllerElement")]
        public class PropertyControllerElementCollection : ConfigurationElementCollection
        {
            protected override ConfigurationElement CreateNewElement()
            {
                return new PropertyControllerElement();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((PropertyControllerElement)element).Key;
            }

            public void Add(PropertyControllerElement element)
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

            public new PropertyControllerElement this[String key]
            {
                get
                {
                    var element = (PropertyControllerElement)BaseGet(key);
                    if (element == null)
                        return new PropertyControllerElement(key);

                    return (PropertyControllerElement)BaseGet(key);
                }
            }
        }

        public class PropertyControllerElement : ConfigurationElement
        {
            public PropertyControllerElement()
            {
            }

            public PropertyControllerElement(string key)
            {
                Key = key;
            }

            public PropertyControllerElement(string key, string value)
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
