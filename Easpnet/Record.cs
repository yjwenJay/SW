using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easpnet
{
    /// <summary>
    /// 数据库记录
    /// </summary>
    [Serializable]
    public class Record
    {
        /// <summary>
        /// 字段列表
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public Dictionary<string, object> Fields = new Dictionary<string, object>();
        /// <summary>
        /// 构造函数
        /// </summary>
        public Record()
        {

        }

        /// <summary>
        /// 添加字段值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddField(string name, object value)
        {
            Fields.Add(name, value);
        }

        /// <summary>
        /// 删除字段值
        /// </summary>
        /// <param name="name"></param>
        public void RemoveField(string name)
        {
            Fields.Remove(name);
        }


        /// <summary>
        /// 获取字段值-返回字符串
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object Get(string name)
        {
            return Fields[name];
        }

        /// <summary>
        /// 获取字段值-返回字符串
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetStr(string name)
        {
            object val = Get(name);
            return val == null ? null : val.ToString();
        }


        /// <summary>
        /// 获取字段值-返回Int32
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetInt32(string name)
        {
            object val = Get(name);
            return val == null ? default(int) : val.ToInt32();
        }

        /// <summary>
        /// 获取字段值-返回Int32
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public int GetInt32(string name, int defaultValue)
        {
            object val = Get(name);
            return val == null ? defaultValue : val.ToInt32();
        }


        /// <summary>
        /// 转化为Json
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this.Fields);
        }


    }
}
