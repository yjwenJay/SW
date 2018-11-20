using System.Collections.Generic;

namespace Easpnet.Modules
{
    /// <summary>
    /// 区块的属性，描述了一个网页区块在向用户呈现时，有哪些属性可供选择。
    /// </summary>
    public class BlockOption
    {
        /// <summary>
        /// 属性的名称(文本描述)
        /// </summary>
        public string OptionName { get; set; }
        /// <summary>
        /// 属性的键
        /// </summary>
        public string OptionKey { get; set; }
        /// <summary>
        /// 录入方式
        /// </summary>
        public InputMethod InputMethod { get; set; }
        /// <summary>
        /// 若录入方式为选择性质，则该项表示可供选择的值列表
        /// </summary>
        public List<NameValue> Values { get; set; }
        /// <summary>
        /// 录入时的数据检测
        /// </summary>
        public long InputCheck { get; set; }
        /// <summary>
        /// 对属性的描述
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// 是否相等。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is BlockOption)
            {
                //
                BlockOption tmp = obj as BlockOption;
                return tmp.OptionKey == this.OptionKey;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }


    /// <summary>
    /// 选项录入方式枚举
    /// </summary>
    public enum InputMethod
    { 
        /// <summary>
        /// 文本框录入方式
        /// </summary>
        TextBox = 1,
        /// <summary>
        /// 文本域录入方式
        /// </summary>
        TextArea = 2,
        /// <summary>
        /// 单项选择
        /// </summary>
        Radio = 3,
        /// <summary>
        /// 复选框多项选择
        /// </summary>
        CheckBox = 4,
        /// <summary>
        /// 是/否
        /// </summary>
        YesOrNo = 5,
        /// <summary>
        /// 下拉列表选择框
        /// </summary>
        Select = 6,
        /// <summary>
        /// Html编辑器
        /// </summary>
        HtmlEditor = 7,
        /// <summary>
        /// 文件上传框
        /// </summary>
        FileUpload = 8,
        /// <summary>
        /// 密码框
        /// </summary>
        Password = 9
    }

    /// <summary>
    /// 输入检测
    /// </summary>
    public enum InputCheck
    { 
        /// <summary>
        /// 必须的
        /// </summary>
        Required = 1,
        /// <summary>
        /// 必须为数字
        /// </summary>
        MustInteger = 2
    }
}
