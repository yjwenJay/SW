using System;
using System.Collections.Generic;
using System.Text;
using Easpnet.Modules.Models;
using System.IO;
using System.Text.RegularExpressions;

namespace Easpnet.Modules.Core.Pages
{
    public class AdminConfigManage : PageBase
    {
        protected List<Config> configs;
        protected List<NameValue> yesnolist;
        protected List<NameValue> deleteFileList;
        Config config;

        //
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AddJsFile("Static/kindeditor/kindeditor.js");

            //yesnolist
            yesnolist = new List<NameValue>();
            yesnolist.Add(new NameValue(T("是"), "True"));
            yesnolist.Add(new NameValue(T("否"), "False"));

            //
            deleteFileList = new List<NameValue>();
            deleteFileList.Add(new NameValue(T("删除现有的文件"), "True"));
            
            //
            config = new Config();
            Query q = Query.NewQuery();
            q.AddCondition(Query.CreateCondition("ConfigTypeKey", Symbol.EqualTo, Ralation.End, Get("type")));
            q.OrderBy("SortOrder");
            configs = config.GetList<Config>(q);

            //
            Handler();
        }

        /// <summary>
        /// 生成输入配置的表单元素
        /// </summary>
        /// <param name="md"></param>
        /// <returns></returns>
        protected string PopulateFormItem(Easpnet.Modules.Models.Config md)
        {
            string s = "";
            //
            switch (md.InputMethod)
            {
                case InputMethod.TextBox:
                    s = Html.InputText(md.ConfigKey, md.ConfigKey, md.ConfigValue, "textInput", "size=\"60\"");
                    break;
                case InputMethod.TextArea:
                    s = Html.TextArea(md.ConfigKey, md.ConfigKey, md.ConfigValue, 3, 80);
                    break;
                case InputMethod.Radio:
                    s = Html.RadioList(md.ConfigKey, md.OptionValueList, md.ConfigValue);
                    break;
                case InputMethod.CheckBox:
                    s = Html.CheckBoxList(md.ConfigKey, md.OptionValueList, md.ConfigValue);
                    break;
                case InputMethod.YesOrNo:
                    s = Html.RadioList(md.ConfigKey, yesnolist, md.ConfigValue);
                    break;
                case InputMethod.Select:
                    s = Html.Select(md.ConfigKey, md.ConfigKey, md.OptionValueList, md.ConfigValue);
                    break;
                case InputMethod.HtmlEditor:
                    s = Html.TextArea(md.ConfigKey, md.ConfigKey, md.ConfigValue, 8, 80);
                    s += "<script type=\"text/javascript\">";
                    s += "KE.show({";
                    s += "  id: '" + md.ConfigKey + "',imageUploadJson: '" + Html.Url("Static/kindeditor/asp.net/upload_json.ashx") + " '";
                    s += "  });";
                    s += "</script>";
                    break;
                case InputMethod.FileUpload:
                    s = Html.InputFile(md.ConfigKey, md.ConfigKey, md.ConfigValue, null);
                    if (!string.IsNullOrEmpty(md.ConfigValue))
                    {
                        s += "<br />" + Html.CheckBoxList("deleteFile" + md.ConfigKey, deleteFileList, null);
                    }                    
                    break;
                case InputMethod.Password:
                    s = Html.InputPassword(md.ConfigKey, md.ConfigKey, md.ConfigValue, "");
                    break;
                default:
                    break;
            }

            return s;
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        private void Handler()
        {
            //保存 
            if (Post("action") == "save")
            {
                bool succ = true;
                foreach (ModelBase item in configs)
                {
                    Config md = item as Config;                    

                    //
                    if (md.InputMethod == InputMethod.FileUpload)
                    {
                        //删除以前的文件
                        bool deleteFile = Convert.ToBoolean(Post("deleteFile" + md.ConfigKey));
                        string oldPath = Server.MapPath("~/" + md.ConfigValue);
                        if (deleteFile)
                        {
                            md.ConfigValue = "";
                        }

                        if (deleteFile && File.Exists(oldPath))
                        {
                            File.Delete(oldPath);
                        }

                        //
                        if (Request.Files[md.ConfigKey] != null)
                        {
                            string fname = Request.Files[md.ConfigKey].FileName;
                            Match mat = Regex.Match(fname, "\\.\\w*$");
                            if (mat.Success)
                            {
                                //限制文件格式
                                string ext = mat.Value.Replace(".", "").ToLower();
                                if (ext == "jpg" || ext == "gif" || ext == "png")
                                {

                                    string filename = "Static/UploadFiles/logo-" + DateTime.Now.ToString("yyyyMMddHHssmm") + mat.Value;
                                    Request.Files[md.ConfigKey].SaveAs(Server.MapPath("~/" + filename));
                                    md.ConfigValue = filename;
                                }
                            }
                            else
                            {
                                succ = false;
                            }
                        }
                    }
                    else
                    {
                        md.ConfigValue = Post(md.ConfigKey);
                    }

                    //存储值
                    if (!md.Update())
                    {
                        succ = false;
                    }
                }

                if (succ)
                {
                    AddSuccessMessage("配置保存成功！");
                }
                else
                {
                    AddErrorMessage("只能上传jpg, gif, png格式的文件！");
                }

            }
        }
    }
}
