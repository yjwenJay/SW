using System.Collections.Specialized;
using System.IO;
using System.Xml;

namespace Easpnet
{
    /// <summary>
    /// 多语言操作助手
    /// </summary>
    public class LanguageHelper
    {
        /// <summary>
        /// 读取语言配置
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static NameValueCollection ReadLanguages(NameValueCollection nv, string path)
        {
            if (nv == null)
            {
                nv = new NameValueCollection();
            }

            if (File.Exists(path))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNodeList nodes = doc.SelectNodes("translations/item");
                foreach (XmlNode item in nodes)
                {
                    string from = GetNodeText(item, "from", "");
                    string to = GetNodeText(item, "to", "");
                    if (!string.IsNullOrEmpty(from))
                    {
                        nv[from] = to;
                    }
                }
            }

            return nv;
        }



        /// <summary>
        /// 获取节点Text内容
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="nodeName"></param>
        /// <param name="defaultValue">若没有找到时的默认值</param>
        /// <returns></returns>
        private static string GetNodeText(XmlNode parentNode, string nodeName, object defaultValue)
        {
            if (parentNode == null)
            {
                return defaultValue.ToString();
            }

            XmlNode node = parentNode.SelectSingleNode(nodeName);
            return node == null ? defaultValue.ToString() : node.InnerText;
        }
    }


}
