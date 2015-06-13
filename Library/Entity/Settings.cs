using System;
using System.Collections.Generic;

namespace Library.Entity
{
    public static class Settings
    {
        #region Level-A
        private static Dictionary<string, string> _xmlSettingsA;
        public static Dictionary<string, string> XmlSettingsA
        {
            get
            {
                return _xmlSettingsA ?? (_xmlSettingsA = Common.XmlHelper.LoadSettingByPath("/Document/Level-A"));
            }
        }

        public static string QQ
        {
            get { return XmlSettingsA["QQ"]; }
        }
        public static string Pwd
        {
            get { return XmlSettingsA["Pwd"]; }
        }

        public static string NekoName
        {
            get { return XmlSettingsA["NekoName"]; }
        }

        #endregion

        #region Level-B
        private static Dictionary<string, string> _xmlSettingsB;
        public static Dictionary<string, string> XmlSettingsB
        {
            get
            {
                return _xmlSettingsB ?? (_xmlSettingsB = Common.XmlHelper.LoadSettingByPath("/Document/Level-B"));
            }
        }

        public static double MasterRate
        {
            get { return double.Parse(XmlSettingsB["MasterRate"]); }
            set
            {
                Common.XmlHelper.UpdateSettingByPath("MasterRate", value.ToString("f2"), "/Document/Level-B");
                _xmlSettingsB = null;
            }
        }

        public static double FriendRate
        {
            get { return double.Parse(XmlSettingsB["FriendRate"]); }
            set
            {
                Common.XmlHelper.UpdateSettingByPath("FriendRate", value.ToString("f2"), "/Document/Level-B");
                _xmlSettingsB = null;
            }
        }

        public static double CustomRate
        {
            get { return double.Parse(XmlSettingsB["CustomRate"]); }
            set
            {
                Common.XmlHelper.UpdateSettingByPath("CustomRate", value.ToString("f2"), "/Document/Level-B");
                _xmlSettingsB = null;
            }
        }

        #endregion

        #region Level-C

        private static List<ExtSettings> _extSettings;
        public static List<ExtSettings> ExtSettings
        {
            get
            {
                if (_extSettings == null)
                {
                    _extSettings = new List<ExtSettings>();
                    foreach (var type in Enum.GetNames(typeof(ExtType)))
                    {
                        var settings = new ExtSettings();
                        ExtType temp;
                        Enum.TryParse(type, out  temp);
                        settings.Type = temp;
                        settings.Settings = Common.XmlHelper.LoadSettingByPath("/Document/Level-C/" + type);
                        _extSettings.Add(settings);
                    }
                }
                return _extSettings;
            }
        }

        #endregion
    }
}
