using System;
using System.Linq;
using System.Configuration;

namespace FrontierCodingTest
{
    public static class SettingsReader
    {
        public static T ReadValue<T>(string settingKey)
        {
            if (HasValue(settingKey))
            {
                T result = (T)Convert.ChangeType(ConfigurationManager.AppSettings[settingKey], typeof(T));
                if (result != null)
                {
                    return result;
                }
            }
            return default(T);
        }

        // One might actually want to log and then throw if an expected AppSettings key is missing.
        // Fail as early as possible, so a glaring error like this is less likely to go unnoticed.
        public static bool HasValue(string settingkey)
        {
            if (string.IsNullOrWhiteSpace(settingkey))
            {
                return false;
            }
            if (!ConfigurationManager.AppSettings.HasKeys())
            {
                return false;
            }
            if (!ConfigurationManager.AppSettings.AllKeys.Contains(settingkey))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[settingkey]))
            {
                return false;
            }
            return true;
        }
    }
}