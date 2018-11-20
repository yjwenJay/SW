using System;
using System.IO;
using Easpnet.Modules.Models;

namespace Easpnet.Modules
{
    /// <summary>
    /// 网页区块基类
    /// </summary>
    public class BlockBase : PageBase
    {
        private bool _display_block;
        private BlockInfo block;

        /// <summary>
        /// 是否显示区块
        /// </summary>
        public bool display_block
        {
            get { return _display_block; }
            set { _display_block = value; }
        }


        /// <summary>
        /// 获取或设置区块实体对象
        /// </summary>
        public BlockInfo Block
        {
            get { return block; }
            set { block = value; }
        }

        /// <summary>
        /// 初始化区块
        /// </summary>
        public virtual void InitBlock(BlockInfo _block)
        {
            _display_block = true;
            block = _block;
        }

        /// <summary>
        /// 获取图片的路径
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        protected override string ImageUrl(string img)
        {
            string imgpath = "";
            string tmpstr = "";
            //检测当前模块下的当前页面是否存在图片
            if (File.Exists(Server.MapPath(tmpstr = "~/Themes/" + MasterPage.CurrentTheme + "/Modules/" + MasterPage.CurrentPage.Module + "/Blocks/" + block.BlockName + "/Images/" + img)))
            {
                imgpath = tmpstr.Replace("~/", "");
            }

            if (!string.IsNullOrEmpty(imgpath))
            {
                return WebRootUrl + imgpath;
            }
            else
            {
                return base.ImageUrl(img);
            } 
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (block != null)
            {
                string dir = "";
                FileInfo[] page_style_files;

                //
                //默认模版
                dir = "Themes/Default/Modules/" + block.Module + "/Blocks/" + block.BlockName + "/";
                if (Directory.Exists(Server.MapPath("~/" + dir)))
                {
                    page_style_files = new DirectoryInfo(Server.MapPath("~/" + dir)).GetFiles("*.css");
                    foreach (System.IO.FileInfo item in page_style_files)
                    {
                        MasterPage.StyleFileList.Add(dir + item.Name);
                    }

                    //
                    page_style_files = new DirectoryInfo(Server.MapPath("~/" + dir)).GetFiles("*.js");
                    foreach (System.IO.FileInfo item in page_style_files)
                    {
                        string filepath = dir + item.Name;
                        if (MasterPage.ScriptsFileList.Find(s => s == filepath) == null)
                        {
                            MasterPage.ScriptsFileList.Add(filepath);
                        }
                    }
                }

                //当前模版
                dir = "Themes/" + MasterPage.CurrentTheme + "/Modules/" + block.Module + "/Blocks/" + block.BlockName + "/";
                if (Directory.Exists(Server.MapPath("~/" + dir)))
                {
                    page_style_files = new DirectoryInfo(Server.MapPath("~/" + dir)).GetFiles("*.css");
                    foreach (System.IO.FileInfo item in page_style_files)
                    {
                        MasterPage.StyleFileList.Add(dir + item.Name);
                    }
                }

            }            
        }


        /// <summary>
        /// 获取ajax的链接
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected string AjaxLink(string action)
        {
            return Html.AjaxLink(this.Block, action);
        }
    }
}
