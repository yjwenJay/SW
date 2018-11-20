using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Data;
using System.Web;
using System.Web.Caching;

namespace Easpnet.Modules.Models
{
    /// <summary>
    /// 配置
    /// </summary>
    [Table(PrimaryKey = "ConfigId", TableName = "CoreConfig", EnableCacheDependency = true)]
    public class Config : ModelBase
    {
        /// <summary>
        /// 操作类
        /// </summary>
        public readonly static Config Instanse = new Config();

        /// <summary>
        /// 配置id
        /// </summary>
        [TableField(IsTableField = true, IsIdentifier = true)]
        public long ConfigId { get; set; }
        /// <summary>
        /// 配置键
        /// </summary>
        [TableField(255)]
        public string ConfigKey { get; set; }
        /// <summary>
        /// 配置值
        /// </summary>
        [TableField]
        public string ConfigValue { get; set; }
        /// <summary>
        /// 配置种类
        /// </summary>
        [TableField(255)]
        public string ConfigType { get; set; }
        /// <summary>
        /// 配置名称
        /// </summary>
        [TableField(50)]
        public string ConfigName { get; set; }
        /// <summary>
        /// 配置说明
        /// </summary>
        [TableField]
        public string Remark { get; set; }
        /// <summary>
        /// 配置排序
        /// </summary>
        [TableField]
        public int SortOrder { get; set; }
        /// <summary>
        /// 录入方式
        /// </summary>
        [TableField]
        public InputMethod InputMethod { get; set; }        
        /// <summary>
        /// 若录入方式为选择性质，则该项表示可供选择的值字符串表示形式
        /// </summary>
        [TableField]
        public string OptionValues { get; set; }
        /// <summary>
        /// 类型Id
        /// </summary>
        [TableField]
        public int ConfigTypeKey { get; set; }


        public long InputCheck { get; set; }

        /// <summary>
        /// 若录入方式为选择性质，则该项表示可供选择的值列表表示形式
        /// 不做为数据库字段存储
        /// </summary>        
        public List<NameValue> OptionValueList { get; set; }

        /// <summary>
        /// 获取配置集合
        /// </summary>
        /// <returns></returns>
        public static NameValueCollection GetConfigs()
        {
            string cachname = "GetConfigsAllKeys" + Instanse.GetType().AssemblyQualifiedName;
            NameValueCollection Configs = HttpRuntime.Cache.Get(cachname) as NameValueCollection;

            if (Configs == null)
            {
                Configs = new NameValueCollection();
                DataTable dt = Instanse.SelectTable(Query.NewQuery().Select("ConfigValue").Select("ConfigKey"));
                foreach (DataRow dr in dt.Rows)
                {
                    Configs.Add(dr["ConfigKey"].ToString(), dr["ConfigValue"].ToString());
                }

                HttpRuntime.Cache.Add(cachname, Configs,
                    Instanse.Dependency, DateTime.Now.AddHours(1.0), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
            
            return Configs;
        }
        

        /// <summary>
        /// 通过IDataReader构造实体对象本身
        /// </summary>
        /// <param name="dr"></param>
        public override void MakeSelfFromIDataReader(System.Data.IDataReader dr)
        {
            base.MakeSelfFromIDataReader(dr);
            if (string.IsNullOrEmpty(OptionValues))
            {
                OptionValueList = new List<NameValue>();
            }
            else
            {
                OptionValueList = JsonConvert.DeserializeObject<List<NameValue>>(OptionValues);
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <returns></returns>
        public override long Create()
        {
            if (OptionValueList != null)
            {
                this.OptionValues = JsonConvert.SerializeObject(OptionValueList);
            }            

            long id = base.Create();
            if (id > 0)
            {
                Configs.ReadConfigs();
            }

            return id;
        }


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <returns></returns>
        public override bool Update()
        {
            if (OptionValueList != null)
            {
                this.OptionValues = JsonConvert.SerializeObject(OptionValueList);
            }            

            bool succ = base.Update();
            if (succ)
            {
                Configs.ReadConfigs();
            }

            return succ;
        }


        /// <summary>
        /// 获取所有的配置类型列表
        /// </summary>
        /// <returns></returns>
        public List<NameValue> GetConfigTypeList()
        {
            List<NameValue> lst = new List<NameValue>();
            Query q = Query.NewQuery().Select("ConfigTypeKey").Select("ConfigType")
                .GroupBy("ConfigType").GroupBy("ConfigTypeKey").OrderBy("ConfigTypeKey");
            DataTable dt = SelectTable(q);
            foreach (DataRow dr in dt.Rows)
            {
                NameValue nv = new NameValue(dr["ConfigType"].ToString(), dr["ConfigTypeKey"].ToString());
                lst.Add(nv);
            }
            return lst;
        }


        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="config_key"></param>
        /// <returns></returns>
        public static string GetConfigValue(string config_key)
        {
            Query q = Query.NewQuery();
            q.Select("ConfigValue");
            q.Where("ConfigKey", Symbol.EqualTo, Ralation.End, config_key);
            DataTable dt = Instanse.SelectTable(q, 1);
            if (dt.Rows.Count == 1)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
    }
}
