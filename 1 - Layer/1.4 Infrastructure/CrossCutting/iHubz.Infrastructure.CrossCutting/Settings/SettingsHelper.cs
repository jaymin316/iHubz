using System.Collections.Generic;

namespace iHubz.Infrastructure.CrossCutting.Settings
{
    public static class SettingsHelper
    {
        static Dictionary<string, string> _settings = new Dictionary<string, string>();

        public static int? DBCommandTimeout
        {
            get
            {
                var setting = GetSetting("DBCommandTimeout");
                if (setting != null)
                    return int.Parse(setting);
                return null;
            }
        }

        public static int? MaxFileUploadSize
        {
            get
            {
                var setting = GetSetting("MaxFileUploadSize");
                if (setting != null)
                    return int.Parse(setting);
                return null;
            }
        }

        public static string GetSetting(string key)
        {
            if (_settings.ContainsKey(key))
                return _settings[key];

            return null;
        }

        public static string GetSetting(string key, string defaultIfNull)
        {
            var setting = GetSetting(key);
            if (setting == null)
                return defaultIfNull;

            return setting;
        }

        public static void AddSetting(string key, string value)
        {
            _settings[key] = value;
        }
    }
}
