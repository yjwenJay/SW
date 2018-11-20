using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.Xsl;
using System.Web;
using System.Data;
using System.IO;

namespace SW.Commons
{
    /// <summary>
    /// ����Helper
    /// </summary>
    public class ExportHelper
    {
        /// <summary>
        /// ����SmartGridView������Դ������
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <param name="exportFormat">�����ļ��ĸ�ʽ</param>
        public static void Export(DataTable dt, string fileName, string exportFormat)
        {
            Export(dt, exportFormat, fileName, Encoding.GetEncoding("GB2312"));
        }

        /// <summary>
        /// ����SmartGridView������Դ������ΪExcel
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        public static void Export(DataTable dt)
        {
            Export(dt, new System.Random(10000).Next().ToString() + ".xls", "CSV");
        }

        /// <summary>
        /// ����ΪExcel
        /// </summary>
        /// <param name="dt">����Դ</param>
        /// <param name="exportFormat">�����ļ��ĸ�ʽ</param>
        /// <param name="fileName">����ļ���</param>
        /// <param name="encoding">����</param>
        public static void Export(DataTable dt, string exportFormat, string fileName, Encoding encoding)
        {
            DataSet dsExport = new DataSet("Export");
            DataTable dtExport = dt.Copy();

            dtExport.TableName = "Values";
            dsExport.Tables.Add(dtExport);

            string[] headers = new string[dtExport.Columns.Count];
            string[] fields = new string[dtExport.Columns.Count];

            for (int i = 0; i < dtExport.Columns.Count; i++)
            {
                headers[i] = dtExport.Columns[i].ColumnName;
                fields[i] = ReplaceSpecialChars(dtExport.Columns[i].ColumnName);
            }

            Export(dsExport, headers, fields, exportFormat, fileName, encoding);
        }

        /// <summary>
        /// ����ΪExcel
        /// </summary>
        /// <param name="dt">����Դ</param>
        /// <param name="columnIndexList">����������</param>
        /// <param name="exportFormat">�����ļ��ĸ�ʽ</param>
        /// <param name="fileName">����ļ���</param>
        /// <param name="encoding">����</param>
        public static void Export(DataTable dt, int[] columnIndexList, string exportFormat, string fileName, Encoding encoding)
        {
            DataSet dsExport = new DataSet("Export");
            DataTable dtExport = dt.Copy();

            dtExport.TableName = "Values";
            dsExport.Tables.Add(dtExport);

            string[] headers = new string[columnIndexList.Length];
            string[] fields = new string[columnIndexList.Length];

            for (int i = 0; i < columnIndexList.Length; i++)
            {
                headers[i] = dtExport.Columns[columnIndexList[i]].ColumnName;
                fields[i] = ReplaceSpecialChars(dtExport.Columns[columnIndexList[i]].ColumnName);
            }

            Export(dsExport, headers, fields, exportFormat, fileName, encoding);
        }

        /// <summary>
        /// ����ΪExcel
        /// </summary>
        /// <param name="dt">����Դ</param>
        /// <param name="columnIndexList">����������</param>
        /// <param name="headers">�ֶ�����</param>
        /// <param name="exportFormat">�����ļ��ĸ�ʽCSV,DOC,TXT</param>
        /// <param name="fileName">����ļ���</param>
        /// <param name="encoding">����</param>
        public static void Export(DataTable dt, int[] columnIndexList, string[] headers, string exportFormat, string fileName, Encoding encoding)
        {
            DataSet dsExport = new DataSet("Export");
            DataTable dtExport = dt.Copy();

            dtExport.TableName = "Values";
            dsExport.Tables.Add(dtExport);

            string[] fields = new string[columnIndexList.Length];

            for (int i = 0; i < columnIndexList.Length; i++)
            {
                fields[i] = ReplaceSpecialChars(dtExport.Columns[columnIndexList[i]].ColumnName);
            }

            Export(dsExport, headers, fields, exportFormat, fileName, encoding);
        }

        /// <summary>
        /// ����ΪExcel
        /// </summary>
        /// <param name="ds">����Դ</param>
        /// <param name="headers">��ͷ����</param>
        /// <param name="fields">�ֶ�����</param>
        /// <param name="exportFormat">�����ļ��ĸ�ʽ</param>
        /// <param name="fileName">����ļ���</param>
        /// <param name="encoding">����</param>
        private static void Export(DataSet ds, string[] headers, string[] fields, string exportFormat, string fileName, Encoding encoding)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = String.Format("text/{0}", exportFormat.ToLower());
            HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment;filename={0}.{1}", System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8), exportFormat.ToString().ToLower()));
            HttpContext.Current.Response.ContentEncoding = encoding;

            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, encoding);

            CreateStylesheet(writer, headers, fields, exportFormat);
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            XmlDataDocument xmlDoc = new XmlDataDocument(ds);
            XslCompiledTransform xslTran = new XslCompiledTransform();
            xslTran.Load(new XmlTextReader(stream));

            System.IO.StringWriter sw = new System.IO.StringWriter();
            xslTran.Transform(xmlDoc, null, sw);

            HttpContext.Current.Response.Write(sw.ToString());
            sw.Close();
            writer.Close();
            stream.Close();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// ��̬����XSL����д��XML��
        /// </summary>
        /// <param name="writer">XML��</param>
        /// <param name="headers">��ͷ����</param>
        /// <param name="fields">�ֶ�����</param>
        /// <param name="exportFormat">�����ļ��ĸ�ʽ</param>
        private static void CreateStylesheet(XmlTextWriter writer, string[] headers, string[] fields, string exportFormat)
        {
            string ns = "http://www.w3.org/1999/XSL/Transform";
            writer.Formatting = Formatting.None;
            writer.WriteStartDocument();
            writer.WriteStartElement("xsl", "stylesheet", ns);
            writer.WriteAttributeString("version", "1.0");
            writer.WriteStartElement("xsl:output");
            writer.WriteAttributeString("method", "text");
            writer.WriteAttributeString("version", "4.0");
            writer.WriteEndElement();

            // xsl-template
            writer.WriteStartElement("xsl:template");
            writer.WriteAttributeString("match", "/");

            // xsl:value-of for headers
            for (int i = 0; i < headers.Length; i++)
            {
                writer.WriteString("\"");
                writer.WriteStartElement("xsl:value-of");
                writer.WriteAttributeString("select", "'" + headers[i] + "'");
                writer.WriteEndElement(); // xsl:value-of
                writer.WriteString("\"");
                if (i != fields.Length - 1) writer.WriteString((exportFormat == "CSV") ? "," : "	");
            }

            // xsl:for-each
            writer.WriteStartElement("xsl:for-each");
            writer.WriteAttributeString("select", "Export/Values");
            writer.WriteString("\r\n");

            // xsl:value-of for data fields
            for (int i = 0; i < fields.Length; i++)
            {
                long value = 0;
                string text = fields[i];
                if (long.TryParse(text, out value))
                    text = "'" + text;
                writer.WriteString("\"");
                writer.WriteStartElement("xsl:value-of");
                writer.WriteAttributeString("select", text);
                writer.WriteEndElement(); // xsl:value-of
                writer.WriteString("\"");
                if (i != fields.Length - 1) writer.WriteString((exportFormat == "CSV") ? "," : "	");
            }

            writer.WriteEndElement(); // xsl:for-each
            writer.WriteEndElement(); // xsl-template
            writer.WriteEndElement(); // xsl:stylesheet
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ReplaceSpecialChars(string input)
        {
            input = input.Replace(" ", "_x0020_")
                .Replace("%", "_x0025_")
                .Replace("#", "_x0023_")
                .Replace("&", "_x0026_")
                .Replace("/", "_x002F_");

            return input;
        }
    }
}
