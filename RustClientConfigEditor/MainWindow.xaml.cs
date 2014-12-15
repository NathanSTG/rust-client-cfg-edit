using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;

using RustClientConfigEditor.Properties;
using RustClientConfigEditor.Config;

using MahApps.Metro;
using MahApps.Metro.Controls;

namespace RustClientConfigEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public ClientConfiguration Config = new ClientConfiguration();

        private OpenFileDialog openDialog = new OpenFileDialog();
        private SaveFileDialog saveDialog = new SaveFileDialog();

        private string currentFilePath = Settings.Default.configFilePath;

        public MainWindow()
        {
            openDialog.FileName = currentFilePath;
            saveDialog.FileName = currentFilePath;
            openDialog.Filter = "Rust client configuration (.cfg)|*.cfg";
            saveDialog.Filter = "Rust client configuration (.cfg)|*.cfg";
            InitializeComponent(); 
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Default.configFilePath = currentFilePath;
            Settings.Default.Save();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var result = openDialog.ShowDialog(this);
            if(result == null || result == false){
                return;
            }
            else if(result == true)
            {
                try
                {
                    Config.ParseFrom(openDialog.FileName);
                    itemsList.ItemsSource = Config.Items;
                    currentFilePath = openDialog.FileName;
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentFilePath != null)
            {
                try
                {
                    Config.WriteTo(currentFilePath);
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            var result = saveDialog.ShowDialog(this);
            if (result == null || result == false)
            {
                return;
            }
            else if (result == true)
            {
                try
                {
                    Config.WriteTo(saveDialog.FileName);
                    currentFilePath = saveDialog.FileName;
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        private void ShowError(string message)
        {
            MessageBox.Show(this, message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
