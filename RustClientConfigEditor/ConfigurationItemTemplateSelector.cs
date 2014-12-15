using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RustClientConfigEditor
{
    public class ConfigurationItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BooleanDataTemplate { get; set; }
        public DataTemplate IntegerDataTemplate { get; set; }
        public DataTemplate DoubleDataTemplate { get; set; }
        public DataTemplate KeyBindingDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item as Config.ConfigurationItem<bool> != null)
            {
                return BooleanDataTemplate;
            }
            else if (item as Config.ConfigurationItem<int> != null)
            {
                return IntegerDataTemplate;
            }
            else if (item as Config.ConfigurationItem<double> != null)
            {
                return DoubleDataTemplate;
            }
            else
            {
                return KeyBindingDataTemplate;
            }
        }
    }
}
