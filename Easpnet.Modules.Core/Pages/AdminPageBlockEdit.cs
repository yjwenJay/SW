using System;
using System.Collections.Generic;
using System.Text;
using Easpnet.Modules.Models;

namespace Easpnet.Modules.Core.Pages
{
    public class AdminPageBlockEdit : PageBase
    {
        protected bool isgroup = false;         //指示是否处理的PageGroup区块
        protected Models.PageInfo page;         //页面信息
        protected Models.PageGroup group;       //页面分组信息
        protected string blockType;             //区块类型：上、左、右、下
        protected Models.BlockInfo block;       //要编辑的区块
        protected PageBlock page_block;
        protected List<NameValue> yesno = new List<NameValue>();
        protected string redirect_url;

        //获取操作的那个区块集合
        PageBlockList list = null;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            redirect_url = Html.HrefLink("Core", "AdminPageBlockManage", "pid=" + Get("pid"));
            //
            yesno.Add(new NameValue(T("是"), "True"));
            yesno.Add(new NameValue(T("否"), "False"));

            //
            blockType = Get("btype");

            //约定：若存在pgid则为页面组
            if (ExistGet("pgid"))
            {
                redirect_url = Html.HrefLink("Core", "AdminPageGroupBlockManage", "pgid=" + Get("pgid"));
                isgroup = true;
                group = new PageGroup();
                group.PageGroupId = TypeConvert.ToInt64(Get("pgid"));
                if (!group.GetModel())
                {
                    Die(T("参数有误！"));
                }
            }
            else
            {
                page = new Easpnet.Modules.Models.PageInfo();
                page.PageId = TypeConvert.ToInt64(Get("pid"));
                if (!page.GetModel())
                {
                    Die(T("参数有误！"));
                }
            }
                        
            if (isgroup)
            {
                switch (blockType)
                {
                    case "top":
                        list = group.PageBlockListTop;
                        break;
                    case "left":
                        list = group.PageBlockListLeft;
                        break;
                    case "right":
                        list = group.PageBlockListRight;
                        break;
                    case "bottom":
                        list = group.PageBlockListBottom;
                        break;
                    default:
                        Die(T("参数有误！"));
                        break;
                }
            }
            else
            {
                switch (blockType)
                {
                    case "top":
                        list = page.PageBlockListTop;
                        break;
                    case "left":
                        list = page.PageBlockListLeft;
                        break;
                    case "right":
                        list = page.PageBlockListRight;
                        break;
                    case "bottom":
                        list = page.PageBlockListBottom;
                        break;
                    default:
                        Die(T("参数有误！"));
                        break;
                }
            }

            if (list == null)
            {
                Die(T("参数有误！"));
            }
            else
            {
                page_block = list.Find(s => s.PageBlockId == Convert.ToInt32(Get("pbid")));
            }
            
            //若未能找到，则跳转
            if (page_block == null)
            {
                Response.Redirect(redirect_url);
            }
            
            //获取区块
            block = new Easpnet.Modules.Models.BlockInfo();
            block.BlockId = page_block.BlockId;
            if (!block.GetModel())
            {
                Response.Redirect(redirect_url);
            }

            //
            Handler();
        }


        /// <summary>
        /// 处理
        /// </summary>
        void Handler()
        {
            if (Post("action") == "save_block")
            {
                //
                //PageBlockList list;

                ////
                //switch (blockType)
                //{
                //    case "top":
                //        list = page.PageBlockListTop;
                //        break;
                //    case "left":
                //        list = page.PageBlockListLeft;
                //        break;
                //    case "right":
                //        list = page.PageBlockListRight;
                //        break;
                //    case "bottom":
                //        list = page.PageBlockListBottom;
                //        break;
                //    default:
                //        Die(T("参数有误！"));
                //        return;
                //}

                //
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].PageBlockId == page_block.PageBlockId)
                    {
                        list[i].Title = Post("Title");
                        list[i].UseDefaultFrame = Convert.ToBoolean(Post("UseDefaultFrame"));
                        list[i].Options = new List<NameValue>();

                        for (int j = 0; j < Request.Form.Count; j++)
                        {
                            string key = Request.Form.GetKey(j);
                            if (key != "action" && key != "Title" && key != "UseDefaultFrame")
                            {
                                list[i].Options.Add(new NameValue(key, Request.Form[j]));
                            }
                        }
                    }
                }

                //更新页面分组布局
                if (isgroup)
                {
                    if (group.Update())
                    {
                        Response.Redirect(redirect_url);
                    }
                }
                //更新页面布局
                else
                {
                    if (page.Update())
                    {
                        Response.Redirect(redirect_url);
                    }
                }
                
            }
        }


        /// <summary>
        /// 构造表单元素
        /// </summary>
        /// <param name="opt"></param>
        /// <returns></returns>
        protected string PopulateBlockOptionInput(BlockOption opt)
        {
            string s = "";
            NameValue nv = page_block.Options.Find(b => b.Name == opt.OptionKey);
            string val = nv == null ? "" : nv.Value;

            switch (opt.InputMethod)
            {
                case InputMethod.TextBox:
                    s = Html.InputText(opt.OptionKey, opt.OptionKey, val, "textInput valid", "size=\"40\"");
                    break;
                case InputMethod.TextArea:
                    break;
                case InputMethod.Radio:
                    s = Html.RadioList(opt.OptionKey, opt.Values, val);
                    break;
                case InputMethod.CheckBox:
                    break;
                case InputMethod.YesOrNo:
                    break;
                default:
                    break;
            }

            return s;
        }
    }
}
