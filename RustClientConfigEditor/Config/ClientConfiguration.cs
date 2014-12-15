using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace RustClientConfigEditor.Config
{
    public class ClientConfiguration
    {
        public CompositeCollection Items = new CompositeCollection();

        public void ParseFrom(String inFilePath)
        {
            Items.Clear();

            using(StreamReader inStream = new StreamReader(File.Open(inFilePath, FileMode.Open))){
                try
                {
                    while (!inStream.EndOfStream)
                    {
                        var line = inStream.ReadLine();

                        var splitLine = line.Split(' ');

                        if (splitLine[0] == "input.bind")
                        {
                            Items.Add(new BindingConfigurationItem(splitLine[1], splitLine[2], splitLine[3]));
                        }
                        else if (splitLine[1] == "True" || splitLine[1] == "False")
                        {
                            Items.Add(new ConfigurationItem<bool>(splitLine[0], StringToBool(splitLine[1])));
                        }
                        else
                        {
                            try{
                                var intVal = Int32.Parse(splitLine[1]);
                                Items.Add(new ConfigurationItem<int>(splitLine[0], intVal));
                            }catch(FormatException ex){
                                var doubleVal = Double.Parse(splitLine[1]);
                                Items.Add(new ConfigurationItem<double>(splitLine[0], doubleVal));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error parsing file - " + ex.GetType().ToString() + ":" + ex.Message);
                }
            }

            return;
        }

        public void WriteTo(String outFilePath)
        {
            using(StreamWriter outStream = new StreamWriter(new FileStream(outFilePath, FileMode.OpenOrCreate))){
                try
                {
                    foreach (IConfigurationItem i in Items)
                    {
                        outStream.WriteLine(i.Serialize());
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error saving file - " + ex.GetType().ToString() + ":" + ex.Message);
                }
            }
        }

        private static bool StringToBool(string val)
        {
            return val == "True" ? true : false;
        }
    }
}
