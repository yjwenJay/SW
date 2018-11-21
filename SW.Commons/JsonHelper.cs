using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;


namespace SW
{
    public class JsonHelper
    {
        /// <summary>
        /// 字典转JSON
        /// </summary>
        /// <param name="dics"></param>
        /// <returns></returns>
        public string DictionaryToJson(System.Collections.Generic.Dictionary<string, object> dics)
        {
            StringBuilder builder = new StringBuilder("{");
            foreach (System.Collections.Generic.KeyValuePair<string, object> item in dics)
            {
                builder.Append(item.Key.Replace("@", ""));
                builder.Append(":\"");
                builder.Append(item.Value.ToString());
                builder.Append("\",");
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append("}");
            return builder.ToString();
        }

        /// <summary>
        /// 数据表转JSON
        /// </summary>
        /// <param name="jsonName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string DataTableToJson(string jsonName, DataTable dt)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    builder.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        builder.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                        if (j < (dt.Columns.Count - 1))
                        {
                            builder.Append(",");
                        }
                    }
                    builder.Append("}");
                    if (i < (dt.Rows.Count - 1))
                    {
                        builder.Append(",");
                    }
                }
            }
            builder.Append("]}");
            return builder.ToString();
        }

        public string ObjectToJson<T>(string jsonName, IList<T> IL)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{\"" + jsonName + "\":[");
            if (IL.Count > 0)
            {
                for (int i = 0; i < IL.Count; i++)
                {
                    PropertyInfo[] properties = Activator.CreateInstance<T>().GetType().GetProperties();
                    builder.Append("{");
                    for (int j = 0; j < properties.Length; j++)
                    {
                        builder.Append(string.Concat(new object[] { "\"", properties[j].Name.ToString(), "\":\"", properties[j].GetValue(IL[i], null), "\"" }));
                        if (j < (properties.Length - 1))
                        {
                            builder.Append(",");
                        }
                    }
                    builder.Append("}");
                    if (i < (IL.Count - 1))
                    {
                        builder.Append(",");
                    }
                }
            }
            builder.Append("]}");
            return builder.ToString();
        }


        /// <summary>
        /// 去掉JSON数据中的引号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DeleteJsonMark(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";
            str = str.Trim('"');
            return str.Replace("\\\"", "'");
        }

        public static string DeleteJsonMark(object obj)
        {
            if (obj == null)
                return "";
            return DeleteJsonMark(obj.ToString());
        }

        /// <summary>
        /// 返回JsonObject
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static JObject StringToJObject(string json)
        {
            return (JObject)JsonConvert.DeserializeObject(json);
        }

        [Obsolete]
        /// <summary>
        /// 不使用
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <param name="keys"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static string ReadJson(string jsonStr, string[] keys, string formats = "<br/>{0}:{1}", int jsonType = 0)
        {

            jsonStr = "{\"name\":\"123\",\"value\":\"kkk\"}";
            if (string.IsNullOrEmpty(jsonStr))
                return "";

            //JObject 用于操作JSON对象
            //JArray 用于操作JSON数组
            //JToken 用于存放Linq to JSON查询后的结果

            //JValue 表示数组中的值
            //JProperty 表示对象中的属性,以"key/value"形式

            try
            {
                JObject jObj = JObject.Parse(jsonStr);
                //JObject jobj = (JObject)JsonConvert.DeserializeObject(jsonStr);

                //jsonStr = "{\"name\":\"123\",\"value\":\"kkk\"}";
                foreach (KeyValuePair<string, JToken> kvp in jObj)
                {
                    string attrName = DeleteJsonMark(kvp.Key);
                    string value = DeleteJsonMark(kvp.Value.ToString());
                }

                jsonStr = "{\"data\":[{\"name\":\"123\",\"value\":\"kkk\"},{\"name\":\"123\",\"value\":\"kkk\"}]}";
                jObj = JObject.Parse(jsonStr);
                foreach (KeyValuePair<string, JToken> kvp in jObj)
                {
                    string attrName = DeleteJsonMark(kvp.Key);
                    JArray jarr = (JArray)kvp.Value;

                    foreach (JToken p in jarr)
                    {
                        JObject t = (JObject)p;
                        JProperty tt = (JProperty)p;
                    }
                    //string value = DeleteJsonMark(kvp.Value.ToString());
                }
            }
            catch
            {
            }
            return "";
        }

        #region 使用Newtonsoft

        /// <summary>
        /// 将DataRows转换为JSON数据，默认为全部的列
        /// </summary>
        /// <param name="drs">DataRows</param>
        /// <returns></returns>
        public static string DataRowsToJson(DataRow[] drs)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            writer.WriteStartArray();

            foreach (DataRow dr in drs)
            {
                writer.WriteStartObject();
                foreach (DataColumn dc in dr.Table.Columns)
                {
                    writer.WritePropertyName(dc.ColumnName);
                    writer.WriteValue(dr[dc.ColumnName].ToString());
                }
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.Flush();
            return sw.GetStringBuilder().ToString();
        }

        public static string DataRowsToJson(DataRow[] drs, Dictionary<string, string[]> formatDic = null)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            writer.WriteStartArray();

            foreach (DataRow dr in drs)
            {
                writer.WriteStartObject();
                foreach (DataColumn dc in dr.Table.Columns)
                {
                    string name = dc.ColumnName;
                    writer.WritePropertyName(name);
                    string value = dr[dc.ColumnName].ToString();
                    if (string.IsNullOrEmpty(value))
                        writer.WriteValue("");
                    else
                    {
                        #region 数据格式
                        //Dictionary<string, string[]> _params = new Dictionary<string, string[]>();
                        //_params.Add("State", new string[] { "enum", "OrderBizAccountState" });
                        //_params.Add("hasData", new string[] { "bool", "有","无" });//是否
                        //_params.Add("name", new string[] { "regRep", @"([-_])*(\d)*" });
                        try
                        {
                            if (formatDic != null && formatDic.Count > 0)
                            {
                                if (formatDic.ContainsKey(name))
                                {
                                    string t = value;
                                    string[] temp = formatDic[name];
                                    switch (temp[0])
                                    {
                                        case "enum":
                                            {
                                                //t = Enums.EnumHelper.GetDescriptionTry(temp[1], t);
                                            }
                                            break;
                                        case "bool":
                                            {
                                                if (t == "True" || t == "1")
                                                    t = temp[1];
                                                else
                                                    t = temp[2];
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
                                    value = t;
                                }
                            }
                        }
                        catch { }
                        #endregion
                        writer.WriteValue(value);
                    }
                }
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.Flush();
            return sw.GetStringBuilder().ToString();
        }

        /// <summary>
        /// 将DataRows转换为JSON数据
        /// </summary>
        /// <param name="drs">DataRows</param>
        /// <param name="useColumns">要输出的列名称</param>
        /// <returns></returns>
        public static string DataRowsToJson(DataRow[] drs, string[] useColumns)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            writer.WriteStartArray();

            foreach (DataRow dr in drs)
            {
                writer.WriteStartObject();
                foreach (string columnName in useColumns)
                {
                    writer.WritePropertyName(columnName);
                    writer.WriteValue(dr[columnName].ToString());
                }
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.Flush();
            return sw.GetStringBuilder().ToString();
        }

        /// <summary>
        /// 将DataRows转换为JSON数据
        /// </summary>
        /// <param name="drs">DataRows</param>
        /// <param name="removeColumns">要排除的列</param>
        /// <returns></returns>
        public static string DataRowsToJson(DataRow[] drs, string removeColumns)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            writer.WriteStartArray();

            foreach (DataRow dr in drs)
            {
                writer.WriteStartObject();
                foreach (DataColumn dc in dr.Table.Columns)
                {
                    if (!("," + removeColumns.ToLower() + ",").Contains("," + dc.ColumnName.ToLower() + ","))
                    {
                        writer.WritePropertyName(dc.ColumnName);
                        writer.WriteValue(dr[dc.ColumnName].ToString());
                    }
                }
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.Flush();
            return sw.GetStringBuilder().ToString();
        }

        /// <summary>
        /// 将DataRow转换为JSON数据
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <returns></returns>
        public static string DataRowToJson(DataRow dr)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            writer.WriteStartObject();
            foreach (DataColumn dc in dr.Table.Columns)
            {
                writer.WritePropertyName(dc.ColumnName);
                writer.WriteValue(dr[dc.ColumnName].ToString());
            }
            writer.WriteEndObject();


            writer.Flush();
            return sw.GetStringBuilder().ToString();
        }

        /// <summary>
        /// 将指定的两列输出为JSON格式
        /// </summary>
        /// <param name="drs"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DataRowsToJson(DataRow[] drs, string key, string value)
        {
            if (drs.Length > 0 && drs[0].Table.Columns.Contains(key) && drs[0].Table.Columns.Contains(value))
            {
                System.IO.StringWriter sw = new System.IO.StringWriter();
                JsonTextWriter writer = new JsonTextWriter(sw);

                writer.WriteStartObject();
                foreach (DataRow dr in drs)
                {
                    writer.WritePropertyName(dr[key].ToString());
                    writer.WriteValue(dr[value].ToString());
                }
                writer.WriteEndObject();
                writer.Flush();
                return sw.GetStringBuilder().ToString();
            }
            else
                return "[]";

        }

        /// <summary>
        /// APP专用
        /// </summary>
        /// <param name="drs">数据集</param>
        /// <param name="key">键值</param>
        /// <param name="value">值值</param>
        /// <param name="line">在线列表</param>
        /// <returns></returns>
        public static string DataRowsToJson(DataRow[] drs, string key, string value, string line)
        {
            if (drs.Length > 0 && drs[0].Table.Columns.Contains(key) && drs[0].Table.Columns.Contains(value))
            {
                bool hasTName = false;
                if (drs[0].Table.Columns.Contains("userNickName") && drs[0].Table.Columns.Contains("userTrueName"))
                    hasTName = true;
                bool hasTel = false;
                if (drs[0].Table.Columns.Contains("userTel"))
                    hasTel = true;

                string names = "," + line + ",";
                Dictionary<string, string> online = new Dictionary<string, string>();
                Dictionary<string, string> offline = new Dictionary<string, string>();
                foreach (DataRow dr in drs)
                {
                    string val = dr[value].ToString();

                    //显示真实姓名或昵称
                    string name = val;
                    if (hasTName)
                    {
                        string t = dr["userTrueName"].ToString();
                        if (string.IsNullOrEmpty(t))
                        {
                            t = dr["userNickName"].ToString();
                            if (!string.IsNullOrEmpty(t))
                                name = t;
                        }
                        else
                            name = t;
                    }
                    if (hasTel)
                    {
                        string tel = dr["userTel"].ToString();
                        if (!string.IsNullOrEmpty(tel))
                            name = string.Format("{0}({1})", name, FormatHelper.FormatTel(tel));
                    }

                    if (names.Contains("," + val + ","))
                        online.Add(dr[key].ToString(), name);
                    else
                        offline.Add(dr[key].ToString(), name);
                }

                System.IO.StringWriter sw = new System.IO.StringWriter();
                JsonTextWriter writer = new JsonTextWriter(sw);

                writer.WriteStartObject();
                foreach (KeyValuePair<string, string> it in online)
                {
                    writer.WritePropertyName(it.Key);
                    writer.WriteValue(it.Value);
                }
                foreach (KeyValuePair<string, string> it in offline)
                {
                    writer.WritePropertyName(it.Key);
                    writer.WriteValue(string.Format("[离线]_{0}", it.Value));
                }
                //foreach (DataRow dr in drs)
                //{
                //    writer.WritePropertyName(dr[key].ToString());
                //    string val = dr[value].ToString();
                //    if (names.Contains("," + val + ","))
                //        val = string.Format("{0}", val);
                //    else
                //        val = string.Format("[离线]_{0}", val);
                //    //val = string.Format("[在线]_{0}", val);

                //    writer.WriteValue(val);
                //}
                writer.WriteEndObject();
                writer.Flush();
                return sw.GetStringBuilder().ToString();
            }
            else
                return "[]";

        }

        #region 对象转JSON

        private static IsoDateTimeConverter timeFormat = null;
        /// <summary>
        /// 序列化对象转JSON
        /// JsonConvert.SerializeObject
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ConvertObjToJson(object obj, bool formatDate = true)
        {
            if (formatDate)
            {
                if (timeFormat == null)
                {
                    timeFormat = new IsoDateTimeConverter();
                    timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                }
                return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, timeFormat);
            }
            else
                return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 将MODEL实体过滤并转换为JSON
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="RemoveType">类型 0:不过滤, 1:包含, 2:排除</param>
        /// <param name="names">属性集,逗号分隔</param>
        /// <returns></returns>
        public static string ConvertModelToJson(object obj, int RemoveType = 0, string names = null)
        {
            Dictionary<string, object> dics = ConvertModelToDict(obj, RemoveType, names);
            if (dics == null)
                return ConvertObjToJson(obj);
            else
                return ConvertObjToJson(dics);
        }

        public static Dictionary<string, object> ConvertModelToDict(object obj, int RemoveType = 0, string names = null)
        {
            if (RemoveType != 0 && !string.IsNullOrEmpty(names))
            {
                Type _type = obj.GetType();
                names = ("," + names + ",").ToLower();
                Dictionary<string, object> dics = new Dictionary<string, object>();
                //读所有属性
                foreach (PropertyInfo pi in _type.GetProperties())
                {
                    string name = pi.Name.ToLower();
                    if (name.ToString() == "dependency")
                        continue;
                    if (RemoveType == 1)
                    {
                        if (names.Contains("," + name + ","))
                            dics.Add(pi.Name, pi.GetValue(obj, null));
                    }
                    else if (RemoveType == 2)
                    {
                        if (!names.Contains("," + name + ","))
                            dics.Add(pi.Name, pi.GetValue(obj, null));
                    }
                }
                return dics;
            }
            else
                return null;
        }
        #endregion

        /// <summary>
        /// JSON转对象
        /// </summary>
        /// <param name="json"></param>
        /// <param name="objType"></param>
        /// <returns></returns>
        public static object ConvertJsonToObj(string json, Type objType)
        {
            try
            {
                return JsonConvert.DeserializeObject(json, objType);
            }
            catch { }
            return null;
        }

        #endregion
    }
}

