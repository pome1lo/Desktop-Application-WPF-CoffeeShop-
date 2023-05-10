using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Xml.Serialization;
using System.Configuration;
using Coffee_Shop.Database;
using static Coffee_Shop.Database.ApplicationContext;
using Coffee_Shop.ViewModels;

namespace Coffee_Shop.Models.Entities
{
    internal static class DataProcessing
    {
        private static ConfigurationSettings? configurationSettings = null!;
        public static ConfigurationSettings? GetConfigurationSettings()
        {
            XmlSerializer xml = new XmlSerializer(typeof(ConfigurationSettings));

            using (FileStream fs = new FileStream(@"../../../StaticFiles/ConfigurationSettings.xml", FileMode.OpenOrCreate))
            {
                var temp = (ConfigurationSettings?)xml.Deserialize(fs);
                configurationSettings = temp;
                return temp;
            }
        }

        public static void SaveConfigurationSettings(this ConfigurationSettings? configuration)
        {
            XmlSerializer xml = new XmlSerializer(typeof(ConfigurationSettings));

            using (FileStream fs = new FileStream(@"../../../StaticFiles/ConfigurationSettings.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, configuration);
            }
        }
        public static void SetDefaultSettings(this ConfigurationSettings configuration)
        {
            SetLanguage();
            SetTheme();
            ViewModelBase.SetDatabase(configuration.Database);
        }

        private static void SetLanguage()
        {
            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(
                        String.Format($"StaticFiles/Resources/Lang{configurationSettings?.Language}.xaml"),
                        UriKind.Relative
                    )
                }
            );
        }
        private static void SetTheme()
        {
            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(
                        String.Format($"StaticFiles/Themes/{configurationSettings?.Theme}.xaml"),
                        UriKind.Relative
                    )
                }
            );
        }
    }
}
