using System;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Xml;

namespace SW
{
    public enum ConfigFileType
    {
        WebConfig,
        AppConfig
    }

    public class ConfigHelper
    {
        public string docName = String.Empty;
        private XmlNode node = null;
        private int _configType;
        public int ConfigType
        {
            get { return _configType; }
            set { _configType = value; }
        }

        #region SetValue
        public bool SetValue(string key, string value)
        {
            XmlDocument cfgDoc = new XmlDocument();
            loadConfigDoc(cfgDoc);
            // retrieve the appSettings node   
            node = cfgDoc.SelectSingleNode("//appSettings");
            if (node == null)
            {
                throw new InvalidOperationException("appSettings section not found");
            }
            try
            {
                // XPath select setting "add" element that contains this key       
                XmlElement addElem = (XmlElement)node.SelectSingleNode("//add[@key='" + key + "']");
                if (addElem != null)
                {
                    addElem.SetAttribute("value", value);
                }
                // not found, so we need to add the element, key and value   
                else
                {
                    XmlElement entry = cfgDoc.CreateElement("add");
                    entry.SetAttribute("key", key);
                    entry.SetAttribute("value", value);
                    node.AppendChild(entry);
                }
                //save it   
                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region saveConfigDoc
        private void saveConfigDoc(XmlDocument cfgDoc, string cfgDocPath)
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter(cfgDocPath, null);
                writer.Formatting = Formatting.Indented;
                cfgDoc.WriteTo(writer);
                writer.Flush();
                writer.Close();
                return;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region removeElement
        public bool removeElement(string elementKey)
        {
            try
            {
                XmlDocument cfgDoc = new XmlDocument();
                loadConfigDoc(cfgDoc);
                // retrieve the appSettings node  
                node = cfgDoc.SelectSingleNode("//appSettings");
                if (node == null)
                {
                    throw new InvalidOperationException("appSettings section not found");
                }
                // XPath select setting "add" element that contains this key to remove      
                node.RemoveChild(node.SelectSingleNode("//add[@key='" + elementKey + "']"));
                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region modifyElement
        public bool modifyElement(string elementKey)
        {
            try
            {
                XmlDocument cfgDoc = new XmlDocument();
                loadConfigDoc(cfgDoc);
                // retrieve the appSettings node  
                node = cfgDoc.SelectSingleNode("//appSettings");
                if (node == null)
                {
                    throw new InvalidOperationException("appSettings section not found");
                }
                // XPath select setting "add" element that contains this key to remove      
                node.RemoveChild(node.SelectSingleNode("//add[@key='" + elementKey + "']"));
                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region loadConfigDoc
        private XmlDocument loadConfigDoc(XmlDocument cfgDoc)
        {
            // load the config file   
            if (Convert.ToInt32(ConfigType) == Convert.ToInt32(ConfigFileType.AppConfig))
            {
                docName = ((Assembly.GetEntryAssembly()).GetName()).Name;
                docName += ".exe.config";
            }
            else
            {
                docName = HttpContext.Current.Server.MapPath("../web.config");
            }
            cfgDoc.Load(docName);
            return cfgDoc;
        }

        /// <summary>
        /// 根据配置项键获取配置项的值
        /// </summary>
        /// <param name="key">配置项键名称</param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            try
            {
                return System.Configuration.ConfigurationManager.AppSettings[key];

                //Uri uri;
                //ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                //Assembly assembly = Assembly.GetCallingAssembly();
                //uri = new Uri(Path.GetDirectoryName(assembly.CodeBase));
                //map.ExeConfigFilename = Path.Combine(uri.LocalPath, assembly.GetName().Name + ".dll.config");
                //string str = ConfigurationManager.OpenMappedExeConfiguration(map, 0).AppSettings.Settings["MyString"].Value;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 根据配置项键获取配置项的数据库连接串
        /// </summary>
        /// <param name="key">配置项键名称</param>
        /// <returns></returns>
        public static string GetConnStr(string key)
        {
            try
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings[key].ConnectionString;
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion


        public static string AppSettings(string key)
        {
            string result = string.Empty;
            try
            {
                return ConfigurationManager.AppSettings[key].ToString();
            }
            catch
            {

            }
            return result;
        }

    }
}
