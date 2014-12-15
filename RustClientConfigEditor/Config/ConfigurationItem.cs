using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustClientConfigEditor.Config
{

    public interface IConfigurationItem {
        string Serialize();
    };

    /// <summary>
    /// Represents a configuration item for rust client config.
    /// </summary>
    /// <typeparam name="TValue">Type of property that this configuration item will hold</typeparam>
    public class ConfigurationItem<TValue> : IConfigurationItem
    {
        private string _id;

        /// <summary>
        /// The string ID of the property
        /// </summary>
        public string PropertyID {
            get{
                return _id;
            }
        }

        /// <summary>
        /// The value of the property
        /// </summary>
        public TValue PropertyValue { get; set; }

        /// <summary>
        /// Creates a new ConfigurationItem witht the specified properties
        /// </summary>
        /// <param name="property">The name of the property</param>
        /// <param name="value">The value of the property</param>
        public ConfigurationItem(string property, TValue value)
        {
            _id = property;
            PropertyValue = value;
        }

        /// <summary>
        /// Returns a value that will be written back into the configuration file.
        /// </summary>
        /// <returns>Returns a string in the form of "<propertyID> <propertyValue>"</returns>
        public virtual string Serialize()
        {
            return PropertyID + " " + StringValue;
        }

        /// <summary>
        /// Returns a string representation of the property value.
        /// </summary>
        /// <returns></returns>
        protected string StringValue
        {
            get{
                if (typeof(TValue).Equals(typeof(bool)))
                {
                    //Check for boolean values and return string representation
                    //with first letter capital.
                    bool? val = PropertyValue as bool?;
                    return val == true ? "True" : "False";
                }
                return PropertyValue.ToString();
            }
        }
    }
}
