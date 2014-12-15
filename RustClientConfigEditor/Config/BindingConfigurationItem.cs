using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustClientConfigEditor.Config
{
    /// <summary>
    /// Represents a configuration item of an input binding.
    /// </summary>
    class BindingConfigurationItem : ConfigurationItem<String>
    {
        public String BackupPropertyValue { get; set; }

        public BindingConfigurationItem(string property, string value1, string value2) : base(property, value1) {
            BackupPropertyValue = value2;
        }

        public override string Serialize()
        {
            return "input.bind " + PropertyID + " " + PropertyValue + " " + BackupPropertyValue;
        }
    }
}
