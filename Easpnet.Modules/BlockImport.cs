using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Easpnet.Modules.Models;
using System.Web;

namespace Easpnet.Modules
{
    public class BlockImport : BlockBase
    {
        /// <summary>
        /// 区块名称
        /// </summary>
        public string BlockName { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 是否使用默认的框架
        /// </summary>
        public string UseDefaultFrame { get; set; }

        /// <summary>
        /// 标题文字
        /// </summary>
        public string Title { get; set; }

        //
        private PlaceHolder place_holder;


        //
        public BlockImport()
        {
            place_holder = new PlaceHolder();
            place_holder.ID = "place_holder";
            Controls.Add(place_holder);
        }

        protected override void OnLoad(EventArgs e)
        {   
            //
            BlockInfo block = new BlockInfo();
            block = BlockInfo.GetBlockInfo(Module, BlockName);

            //UseDefaultFrame
            if (!string.IsNullOrEmpty(UseDefaultFrame))
            {
                if (UseDefaultFrame.ToLower() == "true")
                {
                    block.UseDefaultFrame = true;
                }
                else if (UseDefaultFrame.ToLower() == "false")
                {
                    block.UseDefaultFrame = false;
                }
            }
            //Title
            if (!string.IsNullOrEmpty(Title))
            {
                block.Title = Title;
            }


            //
            base.Block = block;
            base.InitBlock(block);

            if (block != null)
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
                    string main_content = "";
                    string tmpstr = "";

                    //
                    if (System.IO.File.Exists(Server.MapPath(tmpstr = "~/Themes/" + MasterPage.CurrentTheme + "/Modules/" + block.Module + "/Blocks/" + BlockName + "/Block.ascx")))
                    {
                        main_content = tmpstr;
                    }
                    else if (System.IO.File.Exists(Server.MapPath(tmpstr = "~/Themes/Default/Modules/" + block.Module + "/Blocks/" + BlockName + "/Block.ascx")))
                    {
                        main_content = tmpstr;
                    }

                    if (!string.IsNullOrEmpty(main_content))
                    {
                        BlockBase control = (BlockBase)LoadControl(main_content);
                        //将属性转移到子控件
                        string[] arr = new string[Attributes.Keys.Count];
                        Attributes.Keys.CopyTo(arr, 0);
                        for (int i = 0; i < Attributes.Count; i++)
                        {
                            control.Attributes.Add(arr[i], Attributes[arr[i]]);
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

                //添加控件
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
                        string block_class = "block";
                        if (!string.IsNullOrEmpty(block.Attibutes["block_class"]))
                        {
                            block_class += " " + block.Attibutes["block_class"];
                        }

                        //
                        Literal lit_begintag_a = new Literal();
                        lit_begintag_a.Text = "<div id=\"" + block_id + "\" class=\"" + block_class + "\"><h2><span class=\"before-title\"></span><font>" + block.Title
                            + "</font><span class=\"after-title\"></span></h2><div class=\"block-content\">";
                        place_holder.Controls.Add(lit_begintag_a);
                    }


                    //添加空间
                    place_holder.Controls.Add(control_block);

                    //若使用默认的框架
                    if (block.UseDefaultFrame)
                    {
                        Literal lit_begintag_b = new Literal();
                        lit_begintag_b.Text = "</div><h6><font></font><span></span></h6></div>";
                        place_holder.Controls.Add(lit_begintag_b);
                    }
                }

                //加载页面的js.load中的文件
                AddBlockJsLoad(this.MasterPage, block);                
            }

            base.OnLoad(e);
        }


        /// <summary>
        /// 加载页面的js.load中的文件
        /// </summary>
        public static void AddBlockJsLoad(MainMaster master, BlockInfo block)
        {
            string dir = "";
            string filePath;    //临时存储文件路径的变量
            string[] arr;       //临时存储器，在加载js.load，存储分析得到的js文件路径
            char[] sp = new char[] { '\n' };


            //加载js.load中的js文件
            dir = "Themes/Default/";
            if (System.IO.File.Exists(filePath = HttpContext.Current.Server.MapPath("~/" + dir + "Modules/" + block.Module + "/Blocks/" + block.BlockName + "/js.load")))
            {
                string content = System.IO.File.ReadAllText(filePath);
                arr = content.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in arr)
                {
                    if (!master.ScriptsFileList.Exists(s => s == item))
                    {
                        master.ScriptsFileList.Add(item);
                    }
                }
            }

            //
            dir = "Themes/" + master.CurrentTheme + "/";
            if (System.IO.File.Exists(filePath = HttpContext.Current.Server.MapPath("~/" + dir + "Modules/" + block.Module + "/Blocks/" + block.BlockName + "/js.load")))
            {
                string content = System.IO.File.ReadAllText(filePath);
                arr = content.Split(sp, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in arr)
                {
                    if (!master.ScriptsFileList.Exists(s => s == item))
                    {
                        master.ScriptsFileList.Add(item);
                    }
                }
            }
        }
    }
}
