using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.Core.Pages
{
    public class AdminPageDetails : PageBase
    {
        protected Models.PageInfo page;
        protected string return_url;
        protected bool is_edit = false;     //是否是编辑，若为否，则表示为新增
        protected List<NameValue> display_html_list;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            AddJsFile("Static/kindeditor/kindeditor.js");
            return_url = Html.HrefLink("Core", "AdminPageManager", "");

            //display_html_list
            display_html_list = new List<NameValue>();
            display_html_list.Add(new NameValue(T("常规页面"), "False"));
            display_html_list.Add(new NameValue(T("静态页面"), "True"));

            //获取页面信息
            page = new Easpnet.Modules.Models.PageInfo();
            if (!ExistGet("pid"))
            {
                page.DisplayBottomBox = false;
                page.DisplayLeftBox = false;
                page.DisplayRightBox = false;
                page.DisplayTopBox = false;
                page.DisplayStaticHtml = true;
            }
            else
            {
                page.PageId = TypeConvert.ToInt64(Get("pid"));
                is_edit = page.GetModel();
                if (!is_edit)
                {
                    page.DisplayBottomBox = false;
                    page.DisplayLeftBox = false;
                    page.DisplayRightBox = false;
                    page.DisplayTopBox = false;
                    page.DisplayStaticHtml = true;
                }
            }

            //
            Handler();
        }

        /// <summary>
        /// 处理
        /// </summary>
        void Handler()
        {
            string action = Post("action");
            if (action == "save_page" || action == "insert_page")
            {
                //检测数据
                if (!ExistForm("PageName"))
                {
                    AddErrorMessage("页面名称不能为空！");
                }

                //获取数据
                page.PageName = Post("PageName");                
                page.PageGroupName = Post("PageGroupName");
                page.DisplayStaticHtml = Convert.ToBoolean(Post("pageType"));
                page.StaticHtml = Post("static_html");
                page.MetaTitle = Post("MetaTitle");
                page.MetaKeywords = Post("MetaKeywords");
                page.MetaDescription = Post("MetaDescription");
                page.Style = Post("Style");
                page.Remark = Post("Remark");
                page.IsAdminPage = Convert.ToBoolean(Post("isAdminPage"));

                //
                if (HasError)
                {
                    return;
                }

                //
                bool success;
                //
                if (action == "save_page")
                {
                    success = page.Update();
                }
                else
                {
                    page.Module = Post("module");
                    success = page.Create() > 0;
                }

                //
                if (success)
                {
                    Response.Redirect(return_url);
                }
                else
                {
                    AddErrorMessage("对不起，操作失败，请重试！");
                }
            }
        }
    }
}
