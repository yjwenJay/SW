using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Web;
using System.IO;
using System.Web.Caching;

namespace Easpnet.Modules.Models
{
    /// <summary>
    /// 区块，用于描述在网页上的一个特定区域。
    /// </summary>
    [Table(PrimaryKey = "BlockId", TableName = "CoreBlockInfo", EnableCacheDependency = true)]
    public class BlockInfo : ModelBase
    {
        /// <summary>
        /// 全局实例
        /// </summary>
        public static readonly BlockInfo Instance = new BlockInfo();

        public BlockInfo()
        {
            Attibutes = new NameValueCollection();
            BlockOptionList = new List<BlockOption>();
        }

        /// <summary>
        /// 区块id
        /// </summary>
        [TableField(IsTableField = true, IsIdentifier = true)]
        public long BlockId { get; set; }
        /// <summary>
        /// 区块名称
        /// </summary>
        [TableField(255)]
        public string BlockName { get; set; }
        /// <summary>
        /// 区块所属的模块
        /// </summary>
        [TableField(50)]
        public string Module { get; set; }
        /// <summary>
        /// 是否显示静态html内容
        /// </summary>
        [TableField]
        public bool DisplayStaticHtml { get; set; }
        /// <summary>
        /// 静态html内容
        /// </summary>
        [TableField]
        public string StaticHtml { get; set; }
        /// <summary>
        /// 区块的标题
        /// </summary>
        [TableField(50)]
        public string Title { get; set; }
        /// <summary>
        /// 是否使用默认的盒子框架
        /// </summary>
        [TableField]
        public bool UseDefaultFrame { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        [TableField]
        public string Options { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [TableField(255)]
        public string Description { get; set; }
        /// <summary>
        /// 商户Id
        /// </summary>
        [TableField]
        public long MerchantId { get; set; }
        /// <summary>
        /// 是否是商户页面
        /// </summary>
        [TableField]
        public bool IsOpenToMerchant { get; set; }

        /// <summary>
        /// 区块的附加属性
        /// </summary>
        public NameValueCollection Attibutes { get; set; }

        /// <summary>
        /// 属性列表
        /// </summary>
        public List<BlockOption> BlockOptionList { get; set; }

        /// <summary>
        /// 根据区块名称获取区块
        /// </summary>
        /// <param name="blockName"></param>
        /// <returns></returns>
        public static BlockInfo GetBlockInfo(string blockName)
        {
            Query q = Query.NewQuery();
            q.AddCondition(Query.CreateCondition("BlockName", Symbol.EqualTo, Ralation.End, blockName));
            List<BlockInfo> lst = Instance.GetList<BlockInfo>(q, 1);
            if (lst.Count == 1)
            {
                return lst[0] as BlockInfo;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据模块名称和区块名称来获取区块信息
        /// </summary>
        /// <param name="module">模块名称</param>
        /// <param name="blockName">区块名称</param>
        /// <returns></returns>
        public static BlockInfo GetBlockInfo(string module, string blockName)
        {
            string cachname = "GetBlockInfoByModuleAndBlockName" + Instance.GetType().AssemblyQualifiedName + "MODULE_" + module + "BLOCKNAME_" + blockName;
            BlockInfo block = HttpRuntime.Cache.Get(cachname) as BlockInfo;
            if (block == null)
            {
                //是否加入缓存（只对数据库总存储的进行添加缓存）
                bool addCache = true;
                //
                block = new BlockInfo();
                Query q = Query.NewQuery();
                q.Where("Module", Symbol.EqualTo, Ralation.And, module);
                q.Where("BlockName", Symbol.EqualTo, Ralation.End, blockName);
                if (!block.GetModel(q))
                {
                    if (Directory.Exists(HttpContext.Current.Server.MapPath("~/Themes/" + Html.CurrentThemeName + "/Modules/" + module + "/Blocks/" + blockName))
                        || Directory.Exists(HttpContext.Current.Server.MapPath("~/Themes/Default/Modules/" + module + "/Blocks/" + blockName)))
                    {
                        block.Module = module;
                        block.BlockName = blockName;                        
                    }
                    else
                    {
                        block = GetBlockInfo(blockName);
                    }
                }

                //加入缓存
                if (addCache && block != null)
                {
                    HttpRuntime.Cache.Add(cachname, block, Instance.Dependency, DateTime.Now.AddHours(1.0), TimeSpan.Zero, CacheItemPriority.Normal, null);
                }
            }
            return block;
        }
        
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <returns></returns>
        public override long Create()
        {
            if (BlockOptionList != null)
            {
                this.Options = JsonConvert.SerializeObject(BlockOptionList);
            }
            
            return base.Create(); 
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <returns></returns>
        public override bool Update()
        {
            if (BlockOptionList != null)
            {
                this.Options = JsonConvert.SerializeObject(BlockOptionList);
            }

            return base.Update();
        }


        /// <summary>
        /// 通过IDataReader构造实体对象本身
        /// </summary>
        /// <param name="dr"></param>
        public override void MakeSelfFromIDataReader(System.Data.IDataReader dr)
        {
            base.MakeSelfFromIDataReader(dr);
            this.BlockOptionList = GetBlockOptionList(this.Options);
        }
        
        
        /// <summary>
        /// 通过字符串来构造区块属性列表
        /// </summary>
        /// <param name="optionStr"></param>
        /// <returns></returns>
        private List<BlockOption> GetBlockOptionList(string optionStr)
        {
            List<BlockOption> lst;
            if (!string.IsNullOrEmpty(optionStr))
            {
                lst = JsonConvert.DeserializeObject<List<BlockOption>>(optionStr);
            }
            else
            {
                lst = new List<BlockOption>();
            }
            return lst;
        }


        /// <summary>
        /// 从文件读取区块列表信息，调用请确保路径文件路径存在
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        public static List<BlockInfo> ReadBlockInfoFromFile(string filepath)
        {
            string content = System.IO.File.ReadAllText(filepath);
            List<BlockInfo> lst = new List<BlockInfo>();
            //以两个换行符来区分不同的区块
            string[] arr1 = content.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in arr1)
            {
                BlockInfo block = new BlockInfo();
                block.UseDefaultFrame = true;   //默认使用自带的框架
                block.Attibutes = new NameValueCollection();

                string[] arr2 = item.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in arr2)
                {
                    int i = s.IndexOf('=');
                    string propertyName = s.Substring(0, i);
                    string propertyValue = s.Substring(i + 1);
                    switch (propertyName)
                    {
                        case "BlockName":
                            block.BlockName = propertyValue;
                            break;
                        case "Module":
                            block.Module = propertyValue;
                            break;
                        case "Title":
                            block.Title = propertyValue;
                            break;
                        case "UseDefaultFrame":
                            block.UseDefaultFrame = propertyValue.ToLower() == "false" ? false : block.UseDefaultFrame;
                            break;
                        default:
                            block.Attibutes.Add(propertyName, propertyValue);
                            break;
                    }
                }

                lst.Add(block);
            }

            return lst;
        }



    }
}
