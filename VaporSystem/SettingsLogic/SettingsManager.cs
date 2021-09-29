using SettingsLogic.Interface;
using System;
using System.Configuration;

namespace SettingsLogic
{
    public class SettingsManager : ISettingsManager
    {
        public string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                return appSettings[key] ?? string.Empty;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error al leer app settings");
                return string.Empty;
            }
        }
    }
}