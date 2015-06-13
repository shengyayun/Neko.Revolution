using System.Collections.Generic;
using Library.Common;

namespace Library.Entity
{
    public class ExtSettings
    {
        public ExtType Type { get; set; }

        public Dictionary<string, string> Settings { get; set; }

        /// <summary>
        /// 更新C级配置 (需自行更新运行时静态配置)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public static void ModifySetting(string key, string value, ExtType type)
        {
            XmlHelper.UpdateSettingByPath(key, value, "/Document/Level-C/" + type);
        }
    }
}
