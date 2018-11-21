using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using SW.Enums;

namespace SW
{
    /// <summary>
    /// 数据操作类
    /// </summary>
    public class DataHelper
    {
        #region 检查是否有数据
        /// <summary>
        /// 检查DataTable是否有数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool DTHasValue(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 检查DataSet是否有数据
        /// </summary>
        /// <param name="_ds">数据集</param>
        /// <returns></returns>
        public static bool DSHasValue(DataSet _ds)
        {
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 检查DataSet是否有数据 (两个表)
        /// </summary>
        /// <param name="_ds">数据集</param>
        /// <param name="isTableTwo">true</param>
        /// <returns></returns>
        public static bool DSHasValue(DataSet _ds, bool isTableTwo)
        {
            if (_ds != null && _ds.Tables.Count > 1 && _ds.Tables[0].Rows.Count > 0 && _ds.Tables[1].Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 检查DataSet是否有值 (只检查指定下标的表,从0开始)
        /// </summary>
        /// <param name="_ds">数据集</param>
        /// <param name="tableID">表下标</param>
        /// <returns></returns>
        public static bool DSHasValue(DataSet _ds, int tableID)
        {
            if (_ds != null && _ds.Tables.Count > tableID && _ds.Tables[tableID].Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 检查DataSet所有表是否有值 (从0开始的所有表)
        /// </summary>
        /// <param name="_ds">数据集</param>
        /// <param name="tableCount">表数量</param>
        /// <returns></returns>
        public static bool DSHasValueAll(DataSet _ds, int tableCount)
        {
            if (_ds != null && _ds.Tables.Count >= tableCount)
            {
                for (int i = 0; i < tableCount; i++)
                {
                    if (_ds.Tables[i].Rows.Count <= 0)
                        return false;
                }
                return true;
            }
            else
                return false;
        }
        #endregion


        #region 数据集转为字符串

        /// <summary>
        /// 将DataTable转换为字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DtToString(DataTable dt, Dictionary<string, string[]> formatDic = null)
        {
            string data = "";
            try
            {
                foreach (DataColumn column in dt.Columns)
                {
                    data += column.ColumnName.Replace(",", "") + ",";
                }
                data += "\r\n";

                //写出数据
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        string t = row[column].ToString();
                        if (!string.IsNullOrEmpty(t))
                        {
                            #region 数据格式
                            //Dictionary<string, string[]> _params = new Dictionary<string, string[]>();
                            //_params.Add("对账状态", new string[] { "enum", "OrderBizAccountState" });
                            //_params.Add("有上楼费", new string[] { "bool", "有无" });//是否
                            //_params.Add("服务商姓名", new string[] { "regRep", @"([-_])*(\d)*" });
                            //_params.Add("价格日志", new string[] { "regRep", "" });                
                            //string data = DataHelper.DsToString(_ds, _params);
                            //DataHelper.StringToCSV(data, listName, "");
                            try
                            {
                                if (formatDic != null && formatDic.Count > 0)
                                {
                                    if (formatDic.ContainsKey(column.ColumnName))
                                    {
                                        string[] temp = formatDic[column.ColumnName];
                                        switch (temp[0])
                                        {
                                            case "enum":
                                                {
                                                    //t = EnumHelper.GetDescriptionTry(temp[1], t);
                                                }
                                                break;
                                            case "bool":
                                                {
                                                    char[] c = temp[1].ToCharArray();
                                                    if (t == "True" || t == "1")
                                                        t = c[0].ToString();
                                                    else
                                                        t = c[1].ToString();
                                                }
                                                break;
                                            case "regRep":
                                                {
                                                    t = RegExp.RegReplace(t, temp[1], "");
                                                }
                                                break;
                                            case "regRepGroup":
                                                {
                                                    string v = RegExp.GetValueFirst(t, temp[1]);
                                                    t = t.Replace(v, "");
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                            catch { }
                            #endregion

                            t = t.Replace(",", "，");
                            t = t.Replace("&", " ");
                            t = t.Replace("\"", "“");
                            t = t.Replace("'", "’");
                            t = t.Replace("\r", "");
                            t = t.Replace("\n", "");
                            t = t.Replace("&nbsp;", "");
                            //t = HttpContext.Current.Server.HtmlEncode(t);
                            data += t + ",";
                        }
                        else
                            data += ",";
                    }
                    data += "\r\n";
                }
                data += "\r\n";
            }
            catch { }
            return data;
        }

        /// <summary>
        /// 将DataSete转换为字符串
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string DsToString(DataSet ds, Dictionary<string, string[]> formatDic = null)
        {
            if (DSHasValue(ds))
                return DtToString(ds.Tables[0],formatDic);
            else
                return "";
        }

        /// <summary>
        /// 将DataRow[]转换为字符串
        /// </summary>
        /// <param name="drs"></param>
        /// <returns></returns>
        public static string DrsToString(DataRow[] drs)
        {
            string data = "";
            //写出数据
            try
            {
                foreach (DataRow row in drs)
                {
                    foreach (object obj in row.ItemArray)
                    {
                        string t = obj.ToString();
                        if (!string.IsNullOrEmpty(t))
                        {
                            t = t.Replace(",", "");
                            t = t.Replace("\r", "");
                            t = t.Replace("\n", "");
                            t = HttpContext.Current.Server.HtmlEncode(t);
                            data += t + ",";
                        }
                        else
                            data += ",";
                    }
                    data += "\r\n";
                }
                data += "\r\n";
            }
            catch { }
            return data;

        }

        /// <summary>
        /// 将字符串转换为CSV格式输出
        /// </summary>
        /// <param name="content">字符串内容</param>
        /// <param name="fileName">文件名(无扩展名)</param>
        /// <param name="msg">表头消息</param>
        public static void StringToCSV(string content, string fileName, string msg)
        {
            try
            {
                string temp = string.Format("attachment;filename={0}.csv", fileName);
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Charset = "UTF-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
                //HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.AppendHeader("Content-disposition", temp);
                if (!string.IsNullOrEmpty(msg))
                    HttpContext.Current.Response.Write(msg + "\r\n");
                HttpContext.Current.Response.Write(content);
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
                HttpContext.Current.Response.End();
            }
            catch { }
        }

        #endregion
    }
}
