using Easpnet.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easpnet
{
    /// <summary>
    /// 查询结果集合
    /// </summary>
    public class RecordList<T> : List<T> where T : Record
    {
        /// <summary>
        /// 分页参数
        /// </summary>
        public PageParam PageParam { get; set; }
        
        /// <summary>
        /// 转化为Json
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            if (this.PageParam != null && !this.PageParam.IsSimplePage)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(GetPagedDataList());
            }
            else
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(GetDataList());
            }
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetDataList()
        {
            List<Dictionary<string, object>> lst = new List<Dictionary<string, object>>();
            foreach (var item in this)
            {
                lst.Add(item.Fields);
            }
            return lst;
        }

        /// <summary>
        /// 获取分页+数据列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetPagedDataList()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            if (this.PageParam != null)
            {
                data.Add("pages", PageParam.Count);
                data.Add("total", PageParam.Total);
                //附加的统计数据
                if (PageParam.StatisticResult != null && PageParam.StatisticResult.Count() > 0)
                {
                    data.Add("statistic", PageParam.StatisticResult);
                }
            }
            data.Add("list", GetDataList());
            return data;
        }


    }
}
