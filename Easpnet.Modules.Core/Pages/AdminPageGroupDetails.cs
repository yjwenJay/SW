using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.Core.Pages
{
    public class AdminPageGroupDetails : PageBase
    {
        protected Models.PageGroup group;
        protected string return_url;
        protected bool is_edit = false;     //是否是编辑，若为否，则表示为新增

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            return_url = Html.HrefLink("Core", "AdminPageGroupManager", "");

            group = new Easpnet.Modules.Models.PageGroup();
            if (!ExistGet("pgid"))
            {
                group.DisplayBottomBox = false;
                group.DisplayLeftBox = false;
                group.DisplayRightBox = false;
                group.DisplayTopBox = false;
            }
            else
            {
                group.PageGroupId = TypeConvert.ToInt64(Get("pgid"));
                is_edit = group.GetModel();
                if (!is_edit)
                {
                    group.DisplayBottomBox = false;
                    group.DisplayLeftBox = false;
                    group.DisplayRightBox = false;
                    group.DisplayTopBox = false;
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
            if (action == "save" || action == "insert")
            {
                //检测数据
                if (!ExistForm("PageGroupName"))
                {
                    AddErrorMessage("页面分组名称不能为空！");
                }

                //获取数据
                group.PageGroupName = Post("PageGroupName");
                group.MetaTitle = Post("MetaTitle");
                group.MetaKeywords = Post("MetaKeywords");
                group.MetaDescription = Post("MetaDescription");
                group.Style = Post("Style");
                group.Remark = Post("Remark");


                //
                if (HasError)
                {
                    return;
                }

                //
                bool success;
                //
                if (action == "save")
                {
                    success = group.Update();
                }
                else
                {
                    success = group.Create() > 0;
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
