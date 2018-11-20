using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace Easpnet.Modules.Core.Pages
{
    public class AdminBlockEditOptions : PageBase
    {
        //
        protected Models.BlockInfo block;

        //选项类型
        protected NameValueCollection InputMethodList = new NameValueCollection();

        //数据检测
        protected NameValueCollection InputCheckList = new NameValueCollection();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //
            AddJsFile("Themes/Default/Js/jquery.form.js");
            AddJsFile("Themes/Default/Js/jquery-impromptu/impromptu.js");

            //选项类型
            string[] names = Enum.GetNames(typeof(InputMethod));
            Array values = Enum.GetValues(typeof(InputMethod));
            for (int i = 0; i < names.Length; i++)
            {
                InputMethodList.Add(T(names[i]), Convert.ToInt32(values.GetValue(i)).ToString());
            }

            //数据检测
            names = Enum.GetNames(typeof(InputCheck));
            values = Enum.GetValues(typeof(InputCheck));
            for (int i = 0; i < names.Length; i++)
            {
                InputCheckList.Add(T(names[i]), Convert.ToInt32(values.GetValue(i)).ToString());
            }

            //
            block = new Easpnet.Modules.Models.BlockInfo();
            block.BlockId = TypeConvert.ToInt64(Get("block_id"));
            if (block.GetModel())
            {

            }

            //
            Hander();
        }

        void Hander()
        {
            switch (Post("action"))
            {
                /// 插入
                case "insert":
                    insert();
                    break;
                /// 删除
                case "delete_option":
                    delete_option();
                    break;
                /// 编辑
                case "edit_option":
                    edit_option();
                    break;
                /// 更新
                case "update_option":
                    update_option();
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 更新
        /// </summary>
        void update_option()
        {
            JSONHelper json = new JSONHelper();
            json.successS = false;

            for (int i = 0; i < block.BlockOptionList.Count; i++)
            {
                if (block.BlockOptionList[i].OptionKey == Post("key"))
                {
                    block.BlockOptionList[i].Description = Post("description");
                    block.BlockOptionList[i].InputCheck = 0;
                    block.BlockOptionList[i].InputMethod = (InputMethod)Convert.ToInt32(Post("InputMethod"));
                    block.BlockOptionList[i].OptionName = Post("optionName");
                    block.BlockOptionList[i].Values = PopulateValuesFromString(Post("values"));
                    //
                    
                }
            }

            if (block.Update())
            {
                json.successS = true;
            }
            else
            {
                json.errorS = T("对不起，操作失败！");
            }

            Die(json.ToString());
        }

        /// <summary>
        /// 编辑
        /// </summary>
        void edit_option()
        {
            BlockOption opt = block.BlockOptionList.Find(s => s.OptionKey == Post("key"));
            if (opt != null)
            {
                Die(JsonConvert.SerializeObject(opt));
            }
            else
            {
                Die("");
            }
            
        }

        /// <summary>
        /// 删除
        /// </summary>
        void delete_option()
        {
            JSONHelper json = new JSONHelper();
            json.successS = false;
            if (block.BlockOptionList != null)
            {
                BlockOption opt = new BlockOption();
                opt.OptionKey = Post("key");
                block.BlockOptionList.Remove(opt);
            }
            if (block.Update())
            {
                json.successS = true;
            }
            else
            {
                json.errorS = T("对不起，操作失败！");
            }
            Die(json.ToString());
        }

        /// <summary>
        /// 插入
        /// </summary>
        void insert()
        {
            JSONHelper json = new JSONHelper();
            json.successS = false;

            BlockOption opt = new BlockOption();
            opt.Description = Post("description");
            opt.InputCheck = 0;
            opt.InputMethod = (InputMethod)Convert.ToInt32(Post("InputMethod"));
            opt.OptionKey = Post("key");
            opt.OptionName = Post("optionName");
            opt.Values = PopulateValuesFromString(Post("values"));

            //
            if (block.BlockOptionList == null)
            {
                block.BlockOptionList = new List<BlockOption>();
            }

            block.BlockOptionList.Add(opt);

            if (block.Update())
            {
                json.successS = true;
            }
            else
            {
                json.errorS = T("对不起，操作失败！");
            }

            Die(json.ToString());
        }


        /// <summary>
        /// 通过字符串构造可选值列表
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        List<NameValue> PopulateValuesFromString(string s)
        {
            List<NameValue> lst = new List<NameValue>();

            string[] arr = s.Split(new char[] { '\n'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in arr)
            {
                string[] arr1 = item.Split(new char[] { '='}, StringSplitOptions.RemoveEmptyEntries);
                if (arr1.Length == 2)
                {
                    lst.Add(new NameValue(arr1[0], arr1[1]));
                }
            }

            return lst;
        }
    }
}
