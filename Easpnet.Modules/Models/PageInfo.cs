using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using Newtonsoft.Json;

namespace Easpnet.Modules.Models
{
    /// <summary>
    /// 页面
    /// </summary>
    [Table(PrimaryKey = "PageId", TableName = "CorePageInfo", EnableCacheDependency=true)]
    public class PageInfo : ModelBase
    {
        public static readonly PageInfo Instance = new PageInfo();

        /// <summary>
        /// 页面id
        /// </summary>
        [TableField(IsTableField = true, IsIdentifier = true)]
        public long PageId { get; set; }
        /// <summary>
        /// 页面名称
        /// </summary>
        [TableField(255)]
        public string PageName { get; set; }
        /// <summary>
        /// 页面所属的模块
        /// </summary>
        [TableField(50)]
        public string Module { get; set; }
        /// <summary>
        /// 页面所属的分组
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
        /// 是否只显示静态网页内容-若该选项为是，则显示static_html的内容
        /// </summary>
        [TableField]
        public bool DisplayStaticHtml { get; set; }
        /// <summary>
        /// 网页静态内容
        /// </summary>
        [TableField]
        public string StaticHtml { get; set; }
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
        /// 页面说明
        /// </summary>
        [TableField]
        public string Remark { get; set; }
        /// <summary>
        /// 是否是后台管理页面
        /// </summary>
        [TableField]
        public bool IsAdminPage { get; set; }
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
        /// 页面布局
        /// </summary>
        [TableField]
        public PageLayout Layout { get; set; }
        /// <summary>
        /// 左栏宽度
        /// </summary>
        [TableField]
        public string LayoutLeftWidth { get; set; }
        /// <summary>
        /// 右栏宽度
        /// </summary>
        [TableField]
        public string LayoutRightWidth { get; set; }
        /// <summary>
        /// 中栏宽度
        /// </summary>
        [TableField]
        public string LayoutMiddleWidth { get; set; }
        /// <summary>
        /// 是否是用户自定义页面
        /// </summary>
        [TableField]
        public bool IsCustomPage { get; set; }

        /// <summary>
        /// 左侧栏目列表（最终使用的区块集合）
        /// </summary>
        public List<BlockInfo> LeftBoxesList { get; set; }
        /// <summary>
        /// 右侧栏目列表（最终使用的区块集合）
        /// </summary>
        public List<BlockInfo> RightBoxesList { get; set; }
        /// <summary>
        /// 顶部栏目列表（最终使用的区块集合）
        /// </summary>
        public List<BlockInfo> TopBoxesList { get; set; }
        /// <summary>
        /// 底部栏目列表（最终使用的区块集合）
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
        public override void MakeSelfFromIDataReader(System.Data.IDataReader dr)
        {
            base.MakeSelfFromIDataReader(dr);
            //
            //
            try
            {
                if (TopBoxes == null || string.IsNullOrEmpty(TopBoxes))
                {
                    PageBlockListTop = new PageBlockList();
                }
                else
                {
                    PageBlockListTop = JsonConvert.DeserializeObject<PageBlockList>(TopBoxes);
                }
            }
            catch
            {
                PageBlockListTop = new PageBlockList();
            }

            //
            try
            {
                if (BottomBoxes == null || string.IsNullOrEmpty(BottomBoxes))
                {
                    PageBlockListBottom = new PageBlockList();
                }
                else
                {
                    PageBlockListBottom = JsonConvert.DeserializeObject<PageBlockList>(BottomBoxes);
                }
            }
            catch
            {
                PageBlockListBottom = new PageBlockList();
            }

            //
            try
            {
                if (LeftBoxes == null || string.IsNullOrEmpty(LeftBoxes))
                {
                    PageBlockListLeft = new PageBlockList();
                }
                else
                {
                    PageBlockListLeft = JsonConvert.DeserializeObject<PageBlockList>(LeftBoxes);
                }
            }
            catch
            {
                PageBlockListLeft = new PageBlockList();
            }

            try
            {
                if (RightBoxes == null || string.IsNullOrEmpty(RightBoxes))
                {
                    PageBlockListRight = new PageBlockList();
                }
                else
                {
                    PageBlockListRight = JsonConvert.DeserializeObject<PageBlockList>(RightBoxes);
                }
            }
            catch
            {
                PageBlockListRight = new PageBlockList();
            }
            //
            TopBoxesList = GetBlockInfoList(PageBlockListTop);
            BottomBoxesList = GetBlockInfoList(PageBlockListBottom);
            LeftBoxesList = GetBlockInfoList(PageBlockListLeft);
            RightBoxesList = GetBlockInfoList(PageBlockListRight);
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
        /// 读取区块列表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<BlockInfo> GetBlockInfoList(PageBlockList blist)
        {
            List<BlockInfo> lst = new List<BlockInfo>();
            if (blist != null)
            {
                foreach (PageBlock item in blist)
                {
                    BlockInfo block = new BlockInfo();
                    block.BlockId = item.BlockId;
                    if (block.GetModel())
                    {
                        //
                        block.Title = item.Title;
                        block.UseDefaultFrame = item.UseDefaultFrame;

                        //附加参数
                        block.Attibutes = new NameValueCollection();
                        foreach (NameValue nv in item.Options)
                        {
                            block.Attibutes.Add(nv.Name, nv.Value);
                        }

                        //
                        lst.Add(block);
                    }

                }                
            }

            return lst;
        }

        /// <summary>
        /// 根据页面名称获取
        /// </summary>
        /// <param name="pageName">页面名称</param>
        /// <returns></returns>
        public static PageInfo GetPageInfo(string pageName)
        {
            string cachname = "GetPageInfoByPageName" + Instance.GetType().AssemblyQualifiedName + "PAGENAME_" + pageName;
            PageInfo page = HttpRuntime.Cache.Get(cachname) as PageInfo;
            if (page == null)
            {
                Query q = Query.NewQuery();
                q.AddCondition(Query.CreateCondition("PageName", Symbol.EqualTo, Ralation.End, pageName));
                List<PageInfo> lst = Instance.GetList<PageInfo>(q, 1);
                if (lst.Count == 1)
                {
                    page = lst[0] as PageInfo;
                }
                else
                {
                    page = null;
                }

                if (page != null)
                {
                    HttpRuntime.Cache.Add(cachname, page,
                    Instance.Dependency, DateTime.Now.AddHours(1.0), TimeSpan.Zero, CacheItemPriority.Normal, null);
                }                
            }

            return page;            
        }



        /// <summary>
        /// 根据模块名称和页面名称获取页面信息
        /// </summary>
        /// <param name="module">模块名称</param>
        /// <param name="pageName"></param>
        /// <returns></returns>
        public static PageInfo GetPageInfo(string module, string pageName)
        {
            //若模块名称为空，则根据页面名称获取
            if (string.IsNullOrEmpty(pageName))
            {
                return GetPageInfo(pageName);
            }

            //
            string cachname = "GetPageInfoByPageName" + Instance.GetType().AssemblyQualifiedName + "MODULE_" + module + "PAGENAME_" + pageName;
            PageInfo page = HttpRuntime.Cache.Get(cachname) as PageInfo;
            if (page == null)
            {
                //是否加入缓存（只对数据库总存储的进行添加缓存）
                bool addCache = false;

                //
                Query q = Query.NewQuery();
                q.Where("Module", Symbol.EqualTo, Ralation.And, module);
                q.Where("PageName", Symbol.EqualTo, Ralation.End, pageName);
                List<PageInfo> lst = Instance.GetList<PageInfo>(q, 1);
                if (lst.Count == 1)
                {
                    page = lst[0] as PageInfo;
                    addCache = true;
                }
                else
                {
                    if (Directory.Exists(HttpContext.Current.Server.MapPath("~/Themes/" + Html.CurrentThemeName + "/Modules/" + module + "/Pages/" + pageName))
                        || Directory.Exists(HttpContext.Current.Server.MapPath("~/Themes/Default/Modules/" + module + "/Pages/" + pageName)))
                    {
                        page = MakePageInfoByFileSystem(module, pageName);
                    }
                    else
                    {
                        page = GetPageInfo(pageName);
                        addCache = true;
                    }
                }

                if (addCache && page != null)
                {
                    HttpRuntime.Cache.Add(cachname, page,
                    Instance.Dependency, DateTime.Now.AddHours(1.0), TimeSpan.Zero, CacheItemPriority.Normal, null);
                }
            }

            return page;
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

        /// <summary>
        /// 通过文件系统来构造
        /// </summary>
        /// <param name="module">模块名称</param>
        /// <param name="pageName">页面名称</param>
        /// <returns></returns>
        public static PageInfo MakePageInfoByFileSystem(string module, string pageName)
        {
            PageInfo page = new PageInfo();
            page.Module = module;
            page.PageName = pageName;

            string tmpstr = "";


            //读取区块信息
            //上
            if (File.Exists(tmpstr = HttpContext.Current.Server.MapPath("~/Themes/" + Html.CurrentThemeName + "/Modules/" + module + "/Pages/" + pageName + "/top.block")))
            {
                page.TopBoxesList = BlockInfo.ReadBlockInfoFromFile(tmpstr);
                page.DisplayTopBox = true;
            }
            else if (File.Exists(tmpstr = HttpContext.Current.Server.MapPath("~/Themes/Default/Modules/" + module + "/Pages/" + pageName + "/top.block")))
            {
                page.TopBoxesList = BlockInfo.ReadBlockInfoFromFile(tmpstr);
                page.DisplayTopBox = true;
            }

            //左
            if (File.Exists(tmpstr = HttpContext.Current.Server.MapPath("~/Themes/" + Html.CurrentThemeName + "/Modules/" + module + "/Pages/" + pageName + "/left.block")))
            {
                page.LeftBoxesList = BlockInfo.ReadBlockInfoFromFile(tmpstr);
                page.DisplayLeftBox = true;
            }
            else if (File.Exists(tmpstr = HttpContext.Current.Server.MapPath("~/Themes/Default/Modules/" + module + "/Pages/" + pageName + "/left.block")))
            {
                page.LeftBoxesList = BlockInfo.ReadBlockInfoFromFile(tmpstr);
                page.DisplayLeftBox = true;
            }


            //右
            if (File.Exists(tmpstr = HttpContext.Current.Server.MapPath("~/Themes/" + Html.CurrentThemeName + "/Modules/" + module + "/Pages/" + pageName + "/right.block")))
            {
                page.RightBoxesList = BlockInfo.ReadBlockInfoFromFile(tmpstr);
                page.DisplayRightBox = true;
            }
            else if (File.Exists(tmpstr = HttpContext.Current.Server.MapPath("~/Themes/Default/Modules/" + module + "/Pages/" + pageName + "/right.block")))
            {
                page.RightBoxesList = BlockInfo.ReadBlockInfoFromFile(tmpstr);
                page.DisplayRightBox = true;
            }

            //下
            if (File.Exists(tmpstr = HttpContext.Current.Server.MapPath("~/Themes/" + Html.CurrentThemeName + "/Modules/" + module + "/Pages/" + pageName + "/bottom.block")))
            {
                page.BottomBoxesList = BlockInfo.ReadBlockInfoFromFile(tmpstr);
                page.DisplayBottomBox = true;
            }
            else if (File.Exists(tmpstr = HttpContext.Current.Server.MapPath("~/Themes/Default/Modules/" + module + "/Pages/" + pageName + "/bottom.block")))
            {
                page.BottomBoxesList = BlockInfo.ReadBlockInfoFromFile(tmpstr);
                page.DisplayBottomBox = true;
            }


            //读取页面信息文件 page.info
            string strPageInfo = "";
            if (File.Exists(tmpstr = HttpContext.Current.Server.MapPath("~/Themes/" + Html.CurrentThemeName + "/Modules/" + module + "/Pages/" + pageName + "/page.info")))
            {
                strPageInfo = File.ReadAllText(tmpstr);
            }
            else if (File.Exists(tmpstr = HttpContext.Current.Server.MapPath("~/Themes/Default/Modules/" + module + "/Pages/" + pageName + "/page.info")))
            {
                strPageInfo = File.ReadAllText(tmpstr);
            }

            //
            if (!string.IsNullOrEmpty(strPageInfo))
            {
                Match m = Regex.Match(strPageInfo, "(?<=PageGroupName=)[^\r\n]+");
                page.PageGroupName = m.Success ? m.Value : "";

                //是否显示左栏
                m = Regex.Match(strPageInfo, "(?<=DisplayLeftBox=)[^\r\n]+");
                page.DisplayLeftBox = m.Success ? TypeConvert.ToBoolean(m.Value) : page.DisplayLeftBox;

                //是否显示右栏
                m = Regex.Match(strPageInfo, "(?<=DisplayRightBox=)[^\r\n]+");
                page.DisplayRightBox = m.Success ? TypeConvert.ToBoolean(m.Value) : page.DisplayRightBox;

                //是否显示顶栏
                m = Regex.Match(strPageInfo, "(?<=DisplayTopBox=)[^\r\n]+");
                page.DisplayTopBox = m.Success ? TypeConvert.ToBoolean(m.Value) : page.DisplayTopBox;

                //是否显示底栏
                m = Regex.Match(strPageInfo, "(?<=DisplayBottomBox=)[^\r\n]+");
                page.DisplayBottomBox = m.Success ? TypeConvert.ToBoolean(m.Value) : page.DisplayBottomBox;

                //网页标题
                m = Regex.Match(strPageInfo, "(?<=MetaTitle=)[^\r\n]+");
                page.MetaTitle = m.Success ? m.Value : "";

                //关键字
                m = Regex.Match(strPageInfo, "(?<=MetaKeywords=)[^\r\n]+");
                page.MetaKeywords = m.Success ? m.Value : "";

                //网页描述
                m = Regex.Match(strPageInfo, "(?<=MetaDescription=)[^\r\n]+");
                page.MetaDescription = m.Success ? m.Value : "";

                //IsAdminPage
                m = Regex.Match(strPageInfo, "(?<=IsAdminPage=)[^\r\n]+");
                page.IsAdminPage = m.Success ? TypeConvert.ToBoolean(m.Value) : page.IsAdminPage;


            }

            return page;
        }
    }
}