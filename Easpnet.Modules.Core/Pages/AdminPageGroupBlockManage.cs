using System;
using System.Collections.Generic;
using System.Text;
using Easpnet.Modules.Models;
using Newtonsoft.Json;

namespace Easpnet.Modules.Core.Pages
{
    public class AdminPageGroupBlockManage : PageBase
    {
        protected Models.PageGroup page;
        protected string blockType;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AddJsFile("Static/js/jquery-ui/jquery-ui.min.js");
            AddJsFile("Static/js/jquery.dimensions.js");
            AddJsFile("Static/js/jquery.positionBy.js");
            AddJsFile("Static/js/jdMenu/jquery.jdMenu.js");
            AddJsFile("Static/js/jquery.form.js");
            InsertCssFile("Static/js/jdMenu/jquery.jdMenu.css");
            InsertCssFile("Static/js/jquery-ui/css/start/jquery-ui.custom.css");

            //
            page = new Easpnet.Modules.Models.PageGroup();
            page.PageGroupId = TypeConvert.ToInt64(Get("pgid"));
            if (!page.GetModel())
            {
                Die(T("参数有误！"));
            }

            //
            Handler();
        }

        /// <summary>
        /// 处理
        /// </summary>
        void Handler()
        {
            switch (Post("action"))
            {
                /// 保存布局
                case "save_page":
                    save_page();
                    break;
                /// 编辑栏目可见性
                case "edit_column_visibility":
                    edit_column_visibility();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 编辑栏目可见性
        /// </summary>
        void edit_column_visibility()
        {
            JSONHelper json = new JSONHelper();
            bool display = Post("act") == "open" ? true : false;

            //
            switch (Post("btype"))
            {
                case "top":
                    page.DisplayTopBox = display;
                    break;
                case "left":
                    page.DisplayLeftBox = display;
                    break;
                case "right":
                    page.DisplayRightBox = display;
                    break;
                case "bottom":
                    page.DisplayBottomBox = display;
                    break;
                default:
                    json.successS = false;
                    json.errorS = T("操作失败，参数传递有误！");
                    Die(json.ToString());
                    break;
            }

            if (page.Update())
            {
                json.successS = true;
                json.SingleInfo = Microsoft.JScript.GlobalObject.escape(Html.HrefLink("Core", "AdminPageGroupBlockManage", "pgid=" + page.PageGroupId));
            }
            else
            {
                json.successS = false;
                json.errorS = T("对不起，操作失败！");
            }

            Die(json.ToString());
        }


        /// <summary>
        /// 保存布局 Ajax调用
        /// </summary>
        void save_page()
        {
            page.PageBlockListBottom = MakePageBlockList(Post("bottom"));
            page.PageBlockListLeft = MakePageBlockList(Post("left"));
            page.PageBlockListRight = MakePageBlockList(Post("right"));
            page.PageBlockListTop = MakePageBlockList(Post("top"));
            JSONHelper json = new JSONHelper();
            json.successS = page.Update();
            Die(json.ToString());
        }


        /// <summary>
        /// 通过Post过来的字符串构造区块集合
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        PageBlockList MakePageBlockList(string str)
        {
            PageBlockList list = new PageBlockList();
            if (!string.IsNullOrEmpty(str))
            {
                string[] arr = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in arr)
                {
                    string tempstr = Microsoft.JScript.GlobalObject.unescape(s);
                    PageBlock block = JsonConvert.DeserializeObject<PageBlock>(tempstr);
                    if (block != null)
                    {
                        list.Add(block);
                    }
                }
            }

            return list;
        }


        /// <summary>
        /// 构造区块项html
        /// </summary>
        /// <param name="blist"></param>
        /// <param name="lst"></param>
        /// <returns></returns>
        protected string BlockItemHtml(PageBlockList blist, List<BlockInfo> lst, string btype)
        {
            StringBuilder sb = new StringBuilder();
            foreach (PageBlock item in blist)
            {
                BlockInfo binfo = lst.Find(s => s.BlockId == item.BlockId);
                if (binfo == null)
                {
                    continue;
                }

                sb.Append("<div class=\"portlet\">");
                sb.Append("  <div class=\"portlet-header\">");
                sb.Append("     <ul class='jd_menu'><li>");
                sb.Append(item.Title);
                sb.Append("         &raquo;<ul>");
                sb.Append("             <li><a href=\"" + Html.HrefLink("Core", "AdminPageBlockEdit", "pgid=" + page.PageGroupId, "btype=" + btype, "pbid=" + item.PageBlockId) + "\">编辑属性</a></li>");
                sb.Append("             <li class=\"delete_block\">删除</li>");
                sb.Append("         </ul>");
                sb.Append("        </li>");

                sb.Append("     </ul>");
                sb.Append("  </div>");
                sb.Append("  <div class=\"portlet-content\">");


                sb.Append("<p>" + T("区块名称") + "：" + item.BlockName + "</p>");
                sb.Append("<p>" + T("所属模块") + "：" + binfo.Module + "</p>");
                sb.Append("<p>" + T("区块描述") + "：" + binfo.Description + "</p>");
                sb.Append("<p>" + T("默认框架") + "：" + (item.UseDefaultFrame ? T("是") : T("否")) + "</p>");

                foreach (NameValue nv in item.Options)
                {
                    BlockOption boption = binfo.BlockOptionList.Find(b => b.OptionKey == nv.Name);
                    if (boption == null)
                    {
                        continue;
                    }

                    string val = nv.Value;
                    if (boption.InputMethod == InputMethod.Radio || boption.InputMethod == InputMethod.Select)
                    {
                        NameValue vv = boption.Values.Find(ss => ss.Value == val);
                        if (vv != null)
                        {
                            val = T(vv.Name);
                        }
                    }

                    sb.Append("<p>" + T(boption.OptionName) + "：" + val + "</p>");
                }
                sb.Append("  </div>");
                string tmpstr = "";
                if (item != null)
                {
                    tmpstr = Microsoft.JScript.GlobalObject.escape(JsonConvert.SerializeObject(item));
                }
                sb.Append(Html.InputHidden(btype + item.PageBlockId, btype, tmpstr));
                sb.Append("</div>");
            }

            return sb.ToString();
        }


    }
}
