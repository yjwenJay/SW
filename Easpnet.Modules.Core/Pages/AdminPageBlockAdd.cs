using System;
using System.Collections.Generic;
using System.Text;
using Easpnet.Modules.Models;

namespace Easpnet.Modules.Core.Pages
{
    public class AdminPageBlockAdd : PageBase
    {
        protected bool isgroup = false;         //指示是否处理的PageGroup区块
        protected Models.PageInfo page;         //页面信息
        protected Models.PageGroup group;       //页面分组信息
        protected string blockType;             //区块类型：上、左、右、下
        protected Models.BlockInfo block;       //选择的区块
        protected bool block_selected = false;  //判断是否选择了区块
        protected List<BlockInfo> blocks;       //可选区块列表
        protected List<NameValue> yesno = new List<NameValue>();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            blockType = Get("btype");

            //约定：若存在pgid则为页面组
            if (ExistGet("pgid"))
            {
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

            block = new Easpnet.Modules.Models.BlockInfo();
            //判断是否选择了区块
            if (ExistGet("bid"))
            {                
                block.BlockId = TypeConvert.ToInt64(Get("bid"));
                block_selected = block.GetModel();
            }
            else
            {
                Query q = Query.NewQuery();
                q.OrderBy("Module").OrderBy("BlockName");
                blocks = block.GetList<BlockInfo>(q);
            }

            //
            yesno.Add(new NameValue(T("是"), "True"));
            yesno.Add(new NameValue(T("否"), "False"));

            //
            Handler();
        }


        /// <summary>
        /// 处理
        /// </summary>
        void Handler()
        {
            if (block_selected && Post("action") == "add_block")
            {
                PageBlock pb = new PageBlock();
                pb.BlockId = block.BlockId;
                pb.BlockName = block.BlockName;
                pb.Title = Post("Title");
                pb.UseDefaultFrame = Convert.ToBoolean(Post("UseDefaultFrame"));
                pb.Options = new List<NameValue>();

                for (int i = 0; i < Request.Form.Count; i++)
                {
                    string key = Request.Form.GetKey(i);
                    if (key != "action" && key != "Title" && key != "UseDefaultFrame")
                    {
                        pb.Options.Add(new NameValue(key, Request.Form[i]));
                    }
                }

                //
                if (isgroup)
                {
                    switch (blockType)
                    {
                        case "top":
                            group.PageBlockListTop.Add(pb);
                            break;
                        case "left":
                            group.PageBlockListLeft.Add(pb);
                            break;
                        case "right":
                            group.PageBlockListRight.Add(pb);
                            break;
                        case "bottom":
                            group.PageBlockListBottom.Add(pb);
                            break;
                        default:
                            Die(T("参数有误！"));
                            break;
                    }

                    //
                    if (group.Update())
                    {
                        Response.Redirect(Html.HrefLink("Core", "AdminPageGroupBlockManage", "pgid=" + Get("pgid")));
                    }
                }
                else
                {
                    switch (blockType)
                    {
                        case "top":
                            page.PageBlockListTop.Add(pb);
                            break;
                        case "left":
                            page.PageBlockListLeft.Add(pb);
                            break;
                        case "right":
                            page.PageBlockListRight.Add(pb);
                            break;
                        case "bottom":
                            page.PageBlockListBottom.Add(pb);
                            break;
                        default:
                            Die(T("参数有误！"));
                            break;
                    }

                    //
                    if (page.Update())
                    {
                        Response.Redirect(Html.HrefLink("Core", "AdminPageBlockManage", "pid=" + Get("pid")));
                    }
                }                
            }
        }


        //
        protected string PopulateBlockOptionInput(BlockOption opt)
        {
            string s = "";
            switch (opt.InputMethod)
            {
                case InputMethod.TextBox:
                    s = Html.InputText(opt.OptionKey, opt.OptionKey, "", "textInput valid", "size=\"40\"");
                    break;
                case InputMethod.TextArea:
                    break;
                case InputMethod.Radio:
                    s = Html.RadioList(opt.OptionKey, opt.Values, null);
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
