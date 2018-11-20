using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Easpnet.Modules.Models;

namespace Easpnet.Modules
{
    public class IndexPage : System.Web.UI.Page
    {
        #region [ 控件定义 ]

        protected global::System.Web.UI.HtmlControls.HtmlGenericControl pagestyle;
        protected global::System.Web.UI.WebControls.PlaceHolder PlaceHolderTopBox;
        protected global::System.Web.UI.WebControls.PlaceHolder PlaceHolderLeftBox;
        protected global::System.Web.UI.WebControls.PlaceHolder PlaceHolderMain;
        protected global::System.Web.UI.WebControls.PlaceHolder PlaceHolderRightBox;
        protected global::System.Web.UI.WebControls.PlaceHolder PlaceHolderBottomBox;

        #endregion


        public string CurrentTheme = "Default";
        public string CurrentModuleName = "";
        public string CurrentPageName = "index";


        /// <summary>
        /// 当前的页面信息
        /// </summary>
        public PageInfo CurrentPage;

        /// <summary>
        /// 当前所属的页面分组，若没有指定，则为空
        /// </summary>
        public PageGroup CurrentPageGroup;

        /// <summary>
        /// 配置
        /// </summary>
        public NameValueCollection Configs;

        /// <summary>
        /// 网站根目录地址
        /// </summary>
        public string WebRootUrl;

        /// <summary>
        /// 母版页
        /// </summary>
        public MainMaster MasterPage;

        /// <summary>
        /// 构造方法
        /// </summary>
        public IndexPage()
        {
            //CurrentTheme = Html.CurrentThemeName; 
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            ///// 获取并设置当前页面
            //if (!string.IsNullOrEmpty(Request.QueryString["page"]))
            //{
            //    CurrentPageName = Request.QueryString["page"];
            //}

            /////// 获取页面信息
            //CurrentPage = new PageInfo().GetPageInfo(CurrentPageName);

            ////页面不存在时
            //if (CurrentPage == null)
            //{
            //    CurrentPage = new PageInfo().GetPageInfo("PageNotFound");
            //}

            ////设置Html会话中的当前页面信息
            //Html.CurrentPage = CurrentPage;


            ///默认首页
            //if (string.IsNullOrEmpty(Request.QueryString["page"]))
            //{
            //    CurrentPageName = "Index";
            //    //设置模板
            //    CurrentTheme = Easpnet.Modules.Models.Theme.GetCurrentTheme();
            //    CurrentPage = PageInfo.Instance.GetPageInfo(CurrentPageName);

            //    //配置
            //    Configs = Config.GetConfigs();
                
            //    //// 获取页面分组
            //    CurrentPageGroup = new PageGroup();
            //    CurrentPageGroup.GetPageGroup(CurrentPage.PageGroupName);
            //}
            //else
            //{
            //    Html.CurrentPageName = Request.QueryString["page"];
            //}

            


            /// 获取配置
            //Configs = Html.Configs;



            ///// 设置母板页
            SetMasterPage();

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            // 构造边栏内容。
            AddBlocks(1);
            AddBlocks(2);
            AddBlocks(3);
            AddBlocks(4);

            // 添加主要内容
            AddMainContent();
        }



        /// <summary>
        /// 设置母版页
        /// 母版页加载优先级顺序
        /// 1、当前主题/Modules/页面所属模块名称/Pages/页面名称/MainMaster.Master
        /// 2、默认主题/Modules/页面所属模块名称/Pages/页面名称/MainMaster.Master
        /// 3、当前主题/Modules/页面所属模块名称/PageGroups/页面组名称/MainMaster.Master
        /// 4、默认主题/Modules/页面所属模块名称/PageGroups/页面组名称/MainMaster.Master
        /// 5、当前主题/PageGroups/页面组名称/MainMaster.Master
        /// 6、默认主题/PageGroups/页面组名称/MainMaster.Master
        /// 7、当前主题/Modules/页面所属模块名称/MainMaster.Master
        /// 8、默认主题/Modules/页面所属模块名称/MainMaster.Master
        /// 9、当前主题/MainMaster.Master
        /// 10、默认主题/MainMaster.Master
        /// </summary>
        private void SetMasterPage()
        {
            string master_file;

            string tmpstr = "";
            if (File.Exists(Server.MapPath(tmpstr = "~/Themes/" + CurrentTheme + "/Modules/" + CurrentPage.Module + "/Pages/" + CurrentPageName + "/MainMaster.Master")))
            {
                master_file = tmpstr;
            }
            else if (File.Exists(Server.MapPath(tmpstr = "~/Themes/Default/Modules/" + CurrentPage.Module + "/Pages/" + CurrentPageName + "/MainMaster.Master")))
            {
                master_file = tmpstr;
            }
            else if (!string.IsNullOrEmpty(CurrentPage.PageGroupName)
                && File.Exists(Server.MapPath(tmpstr = "~/Themes/" + CurrentTheme + "/Modules/" + CurrentPage.Module + "/PageGroups/" + CurrentPage.PageGroupName + "/MainMaster.Master")))
            {
                master_file = tmpstr;
            }
            else if (!string.IsNullOrEmpty(CurrentPage.PageGroupName)
                && File.Exists(Server.MapPath(tmpstr = "~/Themes/Default/Modules/" + CurrentPage.Module + "/PageGroups/" + CurrentPage.PageGroupName + "/MainMaster.Master")))
            {
                master_file = tmpstr;
            }
            else if (!string.IsNullOrEmpty(CurrentPage.PageGroupName)
                && File.Exists(Server.MapPath(tmpstr = "~/Themes/" + CurrentTheme + "/PageGroups/" + CurrentPage.PageGroupName + "/MainMaster.Master")))
            {
                master_file = tmpstr;
            }
            else if (!string.IsNullOrEmpty(CurrentPage.PageGroupName)
                && File.Exists(Server.MapPath(tmpstr = "~/Themes/Default/PageGroups/" + CurrentPage.PageGroupName + "/MainMaster.Master")))
            {
                master_file = tmpstr;
            }
            else if (File.Exists(Server.MapPath(tmpstr = "~/Themes/" + CurrentTheme + "/Modules/" + CurrentPage.Module + "/MainMaster.Master")))
            {
                master_file = tmpstr;
            }
            else if (File.Exists(Server.MapPath(tmpstr = "~/Themes/Default/Modules/" + CurrentPage.Module + "/MainMaster.Master")))
            {
                master_file = tmpstr;
            }
            else if (File.Exists(Server.MapPath(tmpstr = "~/Themes/" + CurrentTheme + "/MainMaster.Master")))
            {
                master_file = tmpstr;
            }
            else if (File.Exists(Server.MapPath(tmpstr = "~/Themes/Default/MainMaster.Master")))
            {
                master_file = tmpstr;
            }
            else
            {
                throw new Exception("程序错误，没有找到任何母板页面！");
            }

            if (!string.IsNullOrEmpty(master_file))
            {
                this.Page.MasterPageFile = master_file;
                this.MasterPage = this.Master as MainMaster;
            }

            //是否为web调试
            if (System.Configuration.ConfigurationManager.AppSettings["IsWebDebug"] == "true")
            {
                Response.Write("母板页：" + master_file + "<br/>");
            }
        }


        /// <summary>
        /// 添加区块内容
        /// </summary>
        /// <param name="block_type">
        ///     区块类型：1、顶部栏目，2、左侧栏目，3、右侧栏目，4、底部栏目
        /// </param>
        private void AddBlocks(int block_type)
        {
            //
            bool display = false;
            PlaceHolder place = new PlaceHolder();
            List<BlockInfo> blocks = new List<BlockInfo>();
            bool wrapper = false;           //是否将内容进行包裹
            string wrapper_id = "";      //包裹的div的id

            //
            switch (block_type)
            {
                case 1:
                    place = PlaceHolderTopBox;                    
                    if (CurrentPageGroup != null && CurrentPageGroup.PageGroupId > 0)
                    {
                        blocks = CurrentPageGroup.TopBoxesList;
                        display = CurrentPageGroup.DisplayTopBox && CurrentPageGroup.TopBoxesList.Count > 0;
                    }
                    else
                    {
                        blocks = CurrentPage.TopBoxesList;
                        display = CurrentPage.DisplayTopBox && CurrentPage.TopBoxesList.Count > 0;
                    }                    
                    wrapper = false;
                    wrapper_id = "top";
                    break;
                case 2:
                    place = PlaceHolderLeftBox;
                    if (CurrentPageGroup != null && CurrentPageGroup.PageGroupId > 0)
                    {
                        blocks = CurrentPageGroup.LeftBoxesList;
                        display = CurrentPageGroup.DisplayLeftBox && CurrentPageGroup.LeftBoxesList.Count > 0;
                    }
                    else
                    {
                        blocks = CurrentPage.LeftBoxesList;
                        display = CurrentPage.DisplayLeftBox && CurrentPage.LeftBoxesList.Count > 0;
                    }                    
                    wrapper = true;
                    wrapper_id = "left";
                    break;
                case 3:
                    place = PlaceHolderRightBox;
                    if (CurrentPageGroup != null && CurrentPageGroup.PageGroupId > 0)
                    {
                        blocks = CurrentPageGroup.RightBoxesList;
                        display = CurrentPageGroup.DisplayRightBox && CurrentPageGroup.RightBoxesList.Count > 0;
                    }
                    else
                    {
                        blocks = CurrentPage.RightBoxesList;
                        display = CurrentPage.DisplayRightBox && CurrentPage.RightBoxesList.Count > 0;
                    }                    
                    wrapper = true;
                    wrapper_id = "right";
                    break;
                case 4:
                    place = PlaceHolderBottomBox;
                    if (CurrentPageGroup != null && CurrentPageGroup.PageGroupId > 0)
                    {
                        blocks = CurrentPageGroup.BottomBoxesList;
                        display = CurrentPageGroup.DisplayBottomBox && CurrentPageGroup.BottomBoxesList.Count > 0;
                    }
                    else
                    {
                        blocks = CurrentPage.BottomBoxesList;
                        display = CurrentPage.DisplayBottomBox && CurrentPage.BottomBoxesList.Count > 0;
                    }                    
                    wrapper = false;
                    wrapper_id = "bottom";
                    break;
                default:
                    break;
            }


            //
            if (display)
            {
                //包裹
                if (wrapper)
                {
                    Literal lit_begintag = new Literal();
                    lit_begintag.Text = "<div id=\"column-" + wrapper_id + "\" class=\"column-" + wrapper_id + "\">";
                    place.Controls.Add(lit_begintag);
                }

                //内容
                foreach (BlockInfo block in blocks)
                {
                    Control control_block = null;

                    if (block.DisplayStaticHtml)
                    {
                        Literal lit_content = new Literal();
                        lit_content.Text = string.IsNullOrEmpty(block.StaticHtml) ? "请进入后台添加内容" : block.StaticHtml;
                        //place.Controls.Add(lit_content);
                        control_block = lit_content;
                    }
                    else
                    {
                        ///// 加载主要内容的用户控件
                        string main_content = "";
                        string tmpstr = "";
                        //
                        if (File.Exists(Server.MapPath(tmpstr = "~/Themes/" + CurrentTheme + "/Modules/" + block.Module + "/Blocks/" + block.BlockName + "/Block.ascx")))
                        {
                            main_content = tmpstr;
                        }
                        else if (File.Exists(Server.MapPath(tmpstr = "~/Themes/Default/Modules/" + block.Module + "/Blocks/" + block.BlockName + "/Block.ascx")))
                        {
                            main_content = tmpstr;
                        }

                        if (!string.IsNullOrEmpty(main_content))
                        {
                            BlockBase control = (BlockBase)LoadControl(main_content);
                            //控件的附加属性
                            if (block.Attibutes != null)
                            {
                                for (int i = 0; i < block.Attibutes.Count; i++)
                                {
                                    control.Attributes.Add(block.Attibutes.GetKey(i), block.Attibutes[i]);
                                }
                            }

                            //初始化区块
                            control.InitBlock(block);

                            //
                            if (control.display_block)
                            {
                                control_block = control;
                            }
                        }
                    }

                    if (control_block != null)
                    {
                        //若使用默认的框架
                        if (block.UseDefaultFrame)
                        {
                            string block_id = "";
                            if (!string.IsNullOrEmpty(block.Module))
                            {
                                block_id = block.Module;
                            }
                            block_id += "-" + block.BlockName;


                            if (!string.IsNullOrEmpty(block.Attibutes["block_id"]))
                            {
                                block_id = block.Attibutes["block_id"];
                            }

                            //
                            string block_class = "block block-" + wrapper_id;
                            if (!string.IsNullOrEmpty(block.Attibutes["block_class"]))
                            {
                                block_class += " " + block.Attibutes["block_class"];
                            }

                            //
                            Literal lit_begintag_a = new Literal();
                            lit_begintag_a.Text = "<div id=\"" + block_id + "\" class=\"" + block_class + "\"><h2 class=\"block-title\"><span class=\"before-title\"></span><font>" + block.Title
                                + "</font><span class=\"after-title\"></span></h2><div class=\"block-content\">";
                            place.Controls.Add(lit_begintag_a);
                        }

                        //
                        place.Controls.Add(control_block);

                        //若使用默认的框架
                        if (block.UseDefaultFrame)
                        {
                            Literal lit_begintag_b = new Literal();
                            lit_begintag_b.Text = "</div><h6 class=\"block-title\"><font></font><span></span></h6></div>";
                            place.Controls.Add(lit_begintag_b);
                        }
                    }

                    //加载页面的js.load中的文件
                    
                    BlockImport.AddBlockJsLoad(this.MasterPage, block);
                    
                }

                //包裹结束
                if (wrapper)
                {
                    Literal lit_endtag = new Literal();
                    lit_endtag.Text = "</div>";
                    place.Controls.Add(lit_endtag);
                }
            }
        }


        /// <summary>
        /// 添加主要内容
        /// </summary>
        private void AddMainContent()
        {
            //显示静态内容
            if (CurrentPage.DisplayStaticHtml)
            {
                Literal lit_content = new Literal();
                lit_content.Text = string.IsNullOrEmpty(CurrentPage.StaticHtml) ? "请登录后台添加内容" : CurrentPage.StaticHtml;
                lit_content.Text = "<div class=\"static_html\">" + lit_content.Text + "</div>";
                PlaceHolderMain.Controls.Add(lit_content);
            }
            else
            {
                ///// 加载主要内容的用户控件
                string main_content = "";
                string tmpstr = "";
                //
                if (System.IO.File.Exists(Server.MapPath(tmpstr = "~/Themes/" + CurrentTheme + "/Modules/" + CurrentPage.Module + "/Pages/" + CurrentPageName + "/Content.ascx")))
                {
                    main_content = tmpstr;
                }
                else if (System.IO.File.Exists(Server.MapPath(tmpstr = "~/Themes/Default/Modules/" + CurrentPage.Module + "/Pages/" + CurrentPageName + "/Content.ascx")))
                {
                    main_content = tmpstr;
                }

                //是否为web调试
                if (System.Configuration.ConfigurationManager.AppSettings["IsWebDebug"] == "true")
                {
                    Response.Write("页面路径：" + main_content + "<br/>");
                }

                if (!string.IsNullOrEmpty(main_content))
                {
                    PageBase control = (PageBase)LoadControl(main_content);
                    PlaceHolderMain.Controls.Add(control);
                    control.AfterAdded();
                }
            }


            //设置当前页面的样式
            if (string.IsNullOrEmpty(CurrentPage.Style))
            {
                pagestyle.Visible = false;
            }
            else
            {
                pagestyle.InnerText = CurrentPage.Style;
            }
        }
    }
}
