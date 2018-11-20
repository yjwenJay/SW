using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.Core.Pages
{
    public class AdminBlockDetails : PageBase
    {
        protected Models.BlockInfo block;
        protected string return_url;
        protected bool is_edit = false;     //是否是编辑，若为否，则表示为新增
        protected List<NameValue> display_html_list;
        protected List<NameValue> default_frame_html_list;

        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AddJsFile("Static/kindeditor/kindeditor.js");
            return_url = Html.HrefLink("Core", "AdminBlockManager", "");

            //display_html_list
            display_html_list = new List<NameValue>();
            display_html_list.Add(new NameValue(T("常规区块"), "False"));
            display_html_list.Add(new NameValue(T("静态区块"), "True"));


            //default_frame_html_list
            default_frame_html_list = new List<NameValue>();
            default_frame_html_list.Add(new NameValue(T("使用"), "True"));
            default_frame_html_list.Add(new NameValue(T("不使用"), "False"));
            

            //获取区块信息
            block = new Easpnet.Modules.Models.BlockInfo();
            if (!ExistGet("block_id"))
            {
                block.UseDefaultFrame = true;
                block.DisplayStaticHtml = true;
            }
            else
            {
                block.BlockId = TypeConvert.ToInt64(Get("block_id"));
                is_edit = block.GetModel();
                if (!is_edit)
                {
                    block.UseDefaultFrame = true;
                    block.DisplayStaticHtml = true;
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
            if (action == "save_block" || action == "insert_block")
            {
                //检测数据
                if (!ExistForm("BlockName"))
                {
                    AddErrorMessage("区块名称不能为空！");
                }
                if (!ExistForm("BlockTitle"))
                {
                    AddErrorMessage("区块标题不能为空！");
                }

                

                //获取数据
                block.BlockName = Post("BlockName");
                block.Title = Post("BlockTitle");
                block.Module = Post("module");
                block.DisplayStaticHtml = Convert.ToBoolean(Post("blockType"));
                block.StaticHtml = Post("static_html");
                block.UseDefaultFrame = Convert.ToBoolean(Post("UseDefaultFrame"));
                block.Description = Post("description");

                //
                if (HasError)
                {
                    return;
                }

                //
                bool success;
                //
                if (action == "save_block")
                {
                    success = block.Update();
                }
                else
                {
                    success = block.Create() > 0;
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
