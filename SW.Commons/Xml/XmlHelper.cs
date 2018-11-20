using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Xml.Linq;
using System.IO;

namespace SW.Commons.Xml
{
    /// <summary>
    /// Xml的操作公共类
    /// </summary>    
    public static class XmlHelper
    {
        /// <summary>
        /// 获取XML内容
        /// </summary>
        /// <param name="_path">XML路径</param>
        /// <returns></returns>
        public static string GetXmlContents(string _path)
        {
            if (File.Exists(_path))
            {
                //将XML文件加载进来
                XDocument document = XDocument.Load(_path);//"D:\\123.xml"
                                                           //获取到XML的根元素进行操作
                XElement root = document.Root;
                XElement articles = root.Element("Articles");
                //获取name标签的值
                XElement info = articles.Element("Info");

                return info.Value;
            }
            else
            {
                return string.Empty;
            }
            #region
            /*
            IEnumerable<XElement> enumerable = root.Elements();
            foreach (XElement item in enumerable)
            {
                foreach (XElement item1 in item.Elements())
                {
                    Console.WriteLine(item1.Name);   //输出 name  name1            
                }
            }*/
            #endregion
        }
        /// <summary>
        /// 写入XML内容
        /// </summary>
        /// <param name="_rootpath">XML根路径 E:\\Articles\\</param>
        /// <param name="_value">内容</param>
        /// <returns></returns>
        public static string WriteXmlContents(string _rootpath,string _value)
        {
            //获取根节点对象
            XDocument document = new XDocument();
            XElement root = new XElement("SW");
            XElement Articles = new XElement("Articles");
            Articles.SetElementValue("Info", _value); 
            root.Add(Articles);
            //string guid = Guid.NewGuid().ToString();
            string url = _rootpath  + ".xml";
            root.Save(url); 
            return url;
        }

        /// <summary>
        /// 更新XML中指定节点的值
        /// </summary>
        /// <param name="Path">XML文件路径</param>
        
        /// <param name="NodeValue">需要更新的节点值</param>
        public static void UpdateNode(string _path, string _value)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(_path);
            XmlNode xn = doc.SelectSingleNode("//SW//Articles//Info");
            xn.InnerText = _value;
            doc.Save(_path);
        }

    }
}

