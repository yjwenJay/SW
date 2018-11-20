using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Web;
using System.Web.Caching;

namespace Easpnet.Modules.Models
{
    /// <summary>
    /// 页面分组，同一页面分组的页面，若无特殊指定的话， 将共享页面分组指定的页面布局、SEO设置和样式表。
    /// </summary>
    [Table(PrimaryKey = "PageGroupId", TableName = "CorePageGroup", EnableCacheDependency = true)]
    public class PageGroup : ModelBase
    {
        /// <summary>
        /// 全局实例，用户数据操作
        /// </summary>
        public static readonly PageGroup Instance = new PageGroup();

        /// <summary>
        /// 页面分组id
        /// </summary>
        [TableField(IsTableField = true, IsIdentifier = true)]
        public long PageGroupId { get; set; }
        /// <summary>
        /// 页面分组的名称
        /// </summary>
        [TableField(50)]
        public string PageGroupName { get; set; }
        /// <summary>
        /// 是否显示左侧栏目
        /// </summary>
        [TableField]
        public bool DisplayLeftBox { get; set; }
        /// <summary>
        /// 是否显示右侧栏目
        /// </summary>
        [TableField]
        public bool DisplayRightBox { get; set; }
        /// <summary>
        /// 是否显示顶部栏目
        /// </summary>
        [TableField]
        public bool DisplayTopBox { get; set; }
        /// <summary>
        /// 是否显示底部栏目
        /// </summary>
        [TableField]
        public bool DisplayBottomBox { get; set; }
        /// <summary>
        /// 左侧栏目字符串
        /// </summary>
        [TableField]
        public string LeftBoxes { get; set; }
        /// <summary>
        /// 右侧栏目字符串
        /// </summary>
        [TableField]
        public string RightBoxes { get; set; }
        /// <summary>
        /// 顶部栏目字符串
        /// </summary>
        [TableField]
        public string TopBoxes { get; set; }
        /// <summary>
        /// 底部栏目字符串
        /// </summary>
        [TableField]
        public string BottomBoxes { get; set; }
        /// <summary>
        /// 网页标题
        /// </summary>
        [TableField(255)]
        public string MetaTitle { get; set; }
        /// <summary>
        /// 网页关键字
        /// </summary>
        [TableField(255)]
        public string MetaKeywords { get; set; }
        /// <summary>
        /// 网页描述
        /// </summary>
        [TableField(255)]
        public string MetaDescription { get; set; }
        /// <summary>
        /// 样式表
        /// </summary>
        [TableField]
        public string Style { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [TableField]
        public string Remark { get; set; }

        /// <summary>
        /// 左侧栏目列表
        /// </summary>
        public List<BlockInfo> LeftBoxesList { get; set; }
        /// <summary>
        /// 右侧栏目列表
        /// </summary>
        public List<BlockInfo> RightBoxesList { get; set; }
        /// <summary>
        /// 顶部栏目列表
        /// </summary>
        public List<BlockInfo> TopBoxesList { get; set; }
        /// <summary>
        /// 底部栏目列表
        /// </summary>
        public List<BlockInfo> BottomBoxesList { get; set; }


        /// <summary>
        /// 左侧栏目列表（仅仅是存储的数据结构）
        /// </summary>
        public PageBlockList PageBlockListLeft { get; set; }
        /// <summary>
        /// 右侧栏目列表（仅仅是存储的数据结构）
        /// </summary>
        public PageBlockList PageBlockListRight { get; set; }
        /// <summary>
        /// 顶部栏目列表（仅仅是存储的数据结构）
        /// </summary>
        public PageBlockList PageBlockListTop { get; set; }
        /// <summary>
        /// 底部栏目列表（仅仅是存储的数据结构）
        /// </summary>
        public PageBlockList PageBlockListBottom { get; set; }
        

        /// <summary>
        /// 构建实体类
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public override void MakeSelfFromIDataReader(System.Data.IDataReader dr)
        {
            base.MakeSelfFromIDataReader(dr);
            //
            try
            {
                PageBlockListTop = string.IsNullOrEmpty(TopBoxes) ? new PageBlockList() : JsonConvert.DeserializeObject<PageBlockList>(TopBoxes);
            }
            catch 
            {
                PageBlockListTop = new PageBlockList();
            }

            //
            try
            {
                PageBlockListBottom = string.IsNullOrEmpty(BottomBoxes) ? new PageBlockList() : JsonConvert.DeserializeObject<PageBlockList>(BottomBoxes);
            }
            catch
            {
                PageBlockListBottom = new PageBlockList();
            }
            
            //
            try
            {
                PageBlockListLeft = string.IsNullOrEmpty(LeftBoxes) ? new PageBlockList() : JsonConvert.DeserializeObject<PageBlockList>(LeftBoxes);
            }
            catch
            {
                PageBlockListLeft = new PageBlockList();
            }
            
            try
            {
                PageBlockListRight = string.IsNullOrEmpty(RightBoxes) ? new PageBlockList() : JsonConvert.DeserializeObject<PageBlockList>(RightBoxes);
            }
            catch
            {
                PageBlockListRight = new PageBlockList();
            }
            
            //
            TopBoxesList = PageInfo.GetBlockInfoList(PageBlockListTop);
            BottomBoxesList = PageInfo.GetBlockInfoList(PageBlockListBottom);
            LeftBoxesList = PageInfo.GetBlockInfoList(PageBlockListLeft);
            RightBoxesList = PageInfo.GetBlockInfoList(PageBlockListRight);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <returns></returns>
        public override long Create()
        {
            //分别设置区块的Id
            SetPageBlockListId(PageBlockListLeft);
            SetPageBlockListId(PageBlockListRight);
            SetPageBlockListId(PageBlockListTop);
            SetPageBlockListId(PageBlockListBottom);


            //区块集合对象序列话成字符串进行存储
            if (PageBlockListLeft != null)
            {
                LeftBoxes = JsonConvert.SerializeObject(PageBlockListLeft);
            }

            if (PageBlockListRight != null)
            {
                RightBoxes = JsonConvert.SerializeObject(PageBlockListRight);
            }

            if (PageBlockListTop != null)
            {
                TopBoxes = JsonConvert.SerializeObject(PageBlockListTop);
            }

            if (PageBlockListBottom != null)
            {
                BottomBoxes = JsonConvert.SerializeObject(PageBlockListBottom);
            }
            
            
            return base.Create();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        public override bool Update()
        {
            //分别设置区块的Id
            SetPageBlockListId(PageBlockListLeft);
            SetPageBlockListId(PageBlockListRight);
            SetPageBlockListId(PageBlockListTop);
            SetPageBlockListId(PageBlockListBottom);


            //区块集合对象序列话成字符串进行存储
            if (PageBlockListLeft != null)
            {
                LeftBoxes = JsonConvert.SerializeObject(PageBlockListLeft);
            }

            if (PageBlockListRight != null)
            {
                RightBoxes = JsonConvert.SerializeObject(PageBlockListRight);
            }

            if (PageBlockListTop != null)
            {
                TopBoxes = JsonConvert.SerializeObject(PageBlockListTop);
            }

            if (PageBlockListBottom != null)
            {
                BottomBoxes = JsonConvert.SerializeObject(PageBlockListBottom);
            }
                        
            return base.Update();
        }

        /// <summary>
        /// 获取页面分组
        /// </summary>
        /// <param name="pageGroupName"></param>
        /// <returns></returns>
        public static PageGroup GetPageGroup(string pageGroupName)
        {
            if (string.IsNullOrEmpty(pageGroupName))
            {
                return null;
            }

            string cachname = "GetPageGroup" + Instance.GetType().AssemblyQualifiedName + pageGroupName;
            PageGroup group = HttpRuntime.Cache.Get(cachname) as PageGroup;
            if (group == null)
            {
                Query query = Query.NewQuery();
                query.AddCondition(Query.CreateCondition("PageGroupName", Symbol.EqualTo, Ralation.End, pageGroupName));
                group = new PageGroup();
                if (group.GetModel(query))
                {
                    HttpRuntime.Cache.Add(cachname, group, Instance.Dependency, 
                        DateTime.Now.AddHours(1.0), TimeSpan.Zero, CacheItemPriority.Normal, null);
                }
            }

            return group;
        }



        /// <summary>
        /// 设置区块列表中区块的Id
        /// </summary>
        /// <param name="lst"></param>
        private void SetPageBlockListId(PageBlockList lst)
        {
            if (lst != null)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    lst[i].PageBlockId = i + 1;
                }
            }
        }

    }
}
