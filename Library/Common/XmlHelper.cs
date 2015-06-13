using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using Library.Entity;

namespace Library.Common
{
    // ReSharper disable PossibleNullReferenceException
    public static class XmlHelper
    {


        /// <summary>
        /// 返回该路径下的所有setting
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Dictionary<string, string> LoadSettingByPath(string path)
        {
            var dom = new XmlDocument();
            dom.Load(RunTime.SettingXmlPath);
            var xmlSettings = new Dictionary<string, string>();
            var levelA = dom.SelectSingleNode(path);
            foreach (var setting in levelA.ChildNodes.Cast<XmlNode>().Where(p => p.NodeType == XmlNodeType.Element))
            {
                var key = setting.Attributes["key"].Value;
                var value = setting.Attributes["value"].Value;
                xmlSettings.Add(key, value);
            }
            return xmlSettings;
        }

        /// <summary>
        /// 更新该路径下的setting，不存在的话则新建一个
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="path"></param>
        public static void UpdateSettingByPath(string key, string value, string path)
        {
            var dom = new XmlDocument();
            dom.Load(RunTime.SettingXmlPath);
            var container = dom.SelectSingleNode(path);
            var settings = container.ChildNodes.Cast<XmlNode>().Where(p => p.NodeType == XmlNodeType.Element);
            var setting = settings.FirstOrDefault(p => p.Attributes["key"].Value == key);
            if (setting == null)
            {
                XmlElement xe = dom.CreateElement("setting");
                xe.SetAttribute("key", key);
                xe.SetAttribute("value", value);
                container.AppendChild(xe);
            }
            else
            {
                setting.Attributes["value"].Value = value;
            }
            dom.Save(RunTime.SettingXmlPath);
        }
    }

}
