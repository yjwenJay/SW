using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace SW.Commons.Enums
{

    /// <summary>
    /// 枚举操作类
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 读取枚举说明
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns></returns>
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return en.ToString();
        }

        /// <summary>
        /// 读取枚举说明(兼容方法)
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">int值</param>
        /// <returns></returns>
        public static string GetDescriptionTry<TEnum>(int value)
        {
            Type enumType = typeof(TEnum);
            if (Enum.IsDefined(enumType, value))
            {
                string enumName = Enum.GetName(enumType, value);
                if (string.IsNullOrEmpty(enumName))
                    return "";

                MemberInfo[] memInfo = enumType.GetMember(enumName);
                if (memInfo != null && memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attrs != null && attrs.Length > 0)
                    {
                        return ((DescriptionAttribute)attrs[0]).Description;
                    }
                }
                //var typeHandle = enumType.TypeHandle;
                ////获取字段信息
                //FieldInfo[] arrFieldInfo = enumType.GetFields();
                //for (int i = arrFieldInfo.Length - 1; i >= 0; i--)
                //{
                //    var fieldInfo = arrFieldInfo[i];
                //    //判断名称是否相等
                //    if (fieldInfo.Name != strTarget)
                //    {
                //        continue;
                //    }
                //    var arrAttr = fieldInfo.GetCustomAttributes(true);
                //    for (int j = arrAttr.Length - 1; j >= 0; j--)
                //    {
                //        var attr = arrAttr[j];

                //        //类型转换找到一个Description，用Description作为成员名称
                //        var dscript = attr as DescriptionAttribute;
                //        if (dscript != null)
                //        {
                //            return dscript.Description;
                //        }
                //    }
                //}
            }
            return "";
        }


        ///// <summary>
        ///// 读取枚举说明(兼容方法)
        ///// </summary>
        ///// <param name="en">枚举类型</param>
        ///// <param name="intValue">int值</param>
        ///// <returns></returns>
        //public static string GetDescriptionTry(Enum en, string intValue)
        //{
        //    int value = 0;
        //    if (!int.TryParse(intValue, out value))
        //        return intValue;
        //    return GetDescriptionTry(en, value);
        //}

        //public static string GetDescriptionTry(Enum en, int? intValue)
        //{
        //    if (intValue.HasValue)
        //        return GetDescriptionTry(en, intValue.Value);
        //    return "";
        //}

        ///// <summary>
        ///// 读取枚举说明(兼容方法)
        ///// </summary>
        ///// <param name="en">枚举类型</param>
        ///// <param name="intValue">int值</param>
        ///// <returns></returns>
        //public static string GetDescriptionTry(Enum en, int intValue)
        //{
        //    try
        //    {
        //        Type type = en.GetType();
        //        string enumName = type.GetEnumName(intValue);
        //        if (string.IsNullOrEmpty(enumName))
        //            return "";
        //        MemberInfo[] memInfo = type.GetMember(enumName);
        //        if (memInfo != null && memInfo.Length > 0)
        //        {
        //            object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        //            if (attrs != null && attrs.Length > 0)
        //            {
        //                return ((DescriptionAttribute)attrs[0]).Description;
        //            }
        //        }
        //    }
        //    catch { }
        //    return intValue.ToString();
        //}

        ///// <summary>
        ///// 读取枚举说明(传Name方式)
        ///// </summary>
        ///// <param name="enStr">枚举类型</param>
        ///// <param name="value">Name值</param>
        ///// <returns></returns>
        //public static string GetDescription(string enStr, string value)
        //{
        //    if (string.IsNullOrEmpty(enStr) || string.IsNullOrEmpty(value))
        //        return "";
        //    try
        //    {
        //        Type type = Type.GetType("SW.Commons.Enums." + enStr);
        //        MemberInfo[] memInfo = type.GetMember(value);
        //        if (memInfo != null && memInfo.Length > 0)
        //        {
        //            object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        //            if (attrs != null && attrs.Length > 0)
        //            {
        //                return ((DescriptionAttribute)attrs[0]).Description;
        //            }
        //        }
        //    }
        //    catch { }
        //    return value;
        //}

        /// <summary>
        /// 读出枚举的值
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetEnumValueToStr(Enum en)
        {
            Type type = en.GetType();
            foreach (int EnumValue in Enum.GetValues(type))
            {
                string name = Enum.GetName(type, EnumValue);
                if (en.ToString().Equals(name))
                {
                    return EnumValue.ToString();
                }
            }
            return en.ToString();
        }

        //public static string GetNextEnum(Enum en)
        //{
        //    en.next
        //}

        /// <summary>
        ///读出下一条枚举名称
        /// </summary>
        /// <param name="en"></param>
        /// <param name="maxInt">可读最大值</param>
        /// <returns></returns>
        public static string GetNextEnum(Enum en, int maxInt)
        {
            string reStr = "";
            Type type = en.GetType();
            int thisID = -1000;
            foreach (int tempEnumValue in Enum.GetValues(type))
            {
                if (tempEnumValue > maxInt)
                    break;
                string tempEnumName = Enum.GetName(type, tempEnumValue);
                if (tempEnumName == en.ToString())
                {
                    thisID = tempEnumValue;
                    continue;
                }
                else if (thisID != -1000)
                {
                    MemberInfo[] memInfo = type.GetMember(tempEnumName);
                    if (memInfo != null && memInfo.Length > 0)
                    {
                        object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (attrs != null && attrs.Length > 0)
                        {
                            return ((DescriptionAttribute)attrs[0]).Description;
                        }
                    }
                }
            }
            return reStr;
        }

        /// <summary>
        /// 根据 enum 名称 得到所有枚举
        /// </summary>
        /// <param name="enumString"></param>
        /// <returns></returns>
        public IList<EnumModel> GetEnumList<TEnum>()
        {
            IList<EnumModel> enums = new List<EnumModel>();
            Type type = typeof(TEnum);
            foreach (int tempEnumValue in Enum.GetValues(type))
            {
                EnumModel tempEnumModel = new EnumModel();
                string tempEnumName = Enum.GetName(type, tempEnumValue);
                tempEnumModel.Name = tempEnumName;
                tempEnumModel.Value = tempEnumValue;
                MemberInfo[] memInfo = type.GetMember(tempEnumName);

                if (memInfo != null && memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (attrs != null && attrs.Length > 0)
                    {
                        tempEnumModel.Description = ((DescriptionAttribute)attrs[0]).Description;
                    }
                }
                enums.Add(tempEnumModel);
            }
            return enums;
        }


        /// <summary>
        /// 根据 enum 名称 得到所有枚举
        /// </summary>
        /// <param name="enumString"></param>
        /// <returns></returns>
        public IList<EnumModel> GetEnumList(string enumString)
        {
            IList<EnumModel> enums = new List<EnumModel>();
            if (string.IsNullOrEmpty(enumString))
                return enums;
            Type type = Type.GetType(enumString);
            if (type == null)
                return enums;
            foreach (int tempEnumValue in Enum.GetValues(type))
            {
                EnumModel tempEnumModel = new EnumModel();
                string tempEnumName = Enum.GetName(type, tempEnumValue);
                tempEnumModel.Name = tempEnumName;
                tempEnumModel.Value = tempEnumValue;
                MemberInfo[] memInfo = type.GetMember(tempEnumName);

                if (memInfo != null && memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (attrs != null && attrs.Length > 0)
                    {
                        tempEnumModel.Description = ((DescriptionAttribute)attrs[0]).Description;
                    }
                }
                enums.Add(tempEnumModel);
            }
            return enums;
        }

        #region 直接输出

        /// <summary>
        /// 将枚举输出为select
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="listId">HtmlID</param>
        /// <param name="selectedValue">选中的int值</param>
        /// <param name="firstOptionHtml">起始Option</param>
        /// <param name="attr">属性</param>
        /// <param name="excludeInt">排除数组</param>
        /// <returns></returns>
        public static string GetEnumToDropDownList(Type enumType, string listId, int selectedValue, string firstOptionHtml, string attr, int[] excludeInt)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"<select id=""{0}"" name=""{0}"" {1}>\r\n", listId, attr);
            sb.Append(firstOptionHtml);
            foreach (int tempEnumValue in Enum.GetValues(enumType))
            {
                if (excludeInt != null && excludeInt.Length > 0 && excludeInt.Contains(tempEnumValue))
                    continue;
                string tempEnumName = Enum.GetName(enumType, tempEnumValue);
                sb.AppendFormat(@"<option value=""{0}""", tempEnumValue);
                if (tempEnumValue == selectedValue) sb.Append(@"selected=""selected""");
                sb.Append(">");
                MemberInfo[] memInfo = enumType.GetMember(tempEnumName);
                if (memInfo != null && memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attrs != null && attrs.Length > 0)
                    {
                        sb.Append(((DescriptionAttribute)attrs[0]).Description);
                    }
                }
                sb.Append("</option>\r\n");
            }
            sb.Append("</select>");
            return sb.ToString();
        }

        /// <summary>
        /// 将枚举输出为select
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="listId">HtmlID</param>
        /// <param name="selectedValue">选中的int值</param>
        /// <param name="firstOptionHtml">起始Option</param>
        /// <param name="attr">属性</param>
        /// <param name="excludeInt">排除数组</param>
        /// <returns></returns>
        public static string GetEnumToDropDownList(Enum en, string listId, int selectedValue, string firstOptionHtml, string attr, int[] excludeInt)
        {
            return GetEnumToDropDownList(en.GetType(), listId, selectedValue, firstOptionHtml, attr, excludeInt);
        }

        ///// <summary>
        ///// 将枚举输出为select
        ///// </summary>
        ///// <param name="enumString">枚举类型String</param>
        ///// <param name="listId">HtmlID</param>
        ///// <param name="selectedValue">选中的int值</param>
        ///// <param name="firstOptionHtml">起始Option</param>
        ///// <param name="attr">属性</param>
        ///// <param name="excludeInt">排除数组</param>
        ///// <returns></returns>
        //public static string GetEnumToDropDownList(string enumString, string listId, int selectedValue, string firstOptionHtml, string attr, int[] excludeInt)
        //{
        //    if (string.IsNullOrEmpty(enumString))
        //        return "";
        //    Type type = Type.GetType("SW.Commons.Enums." + enumString);
        //    if (type == null)
        //        return "";
        //    return GetEnumToDropDownList(type, listId, selectedValue, firstOptionHtml, attr, excludeInt);
        //}

        ///// <summary>
        ///// 将枚举输出为select
        ///// </summary>
        ///// <param name="enumString">枚举类型String</param>
        ///// <param name="listId"></param>
        ///// <param name="selectedValue"></param>
        ///// <returns></returns>
        //public static string GetEnumToDropDownList(string enumString, string listId, int selectedValue)
        //{
        //    return GetEnumToDropDownList(enumString, listId, selectedValue, "", "", null);
        //}

        ///// <summary>
        ///// 将枚举输出为select
        ///// </summary>
        ///// <param name="enumString">枚举类型String</param>
        ///// <param name="listId"></param>
        ///// <param name="selectedValue"></param>
        ///// <param name="firstOptionHtml"></param>
        ///// <returns></returns>
        //public static string GetEnumToDropDownList(string enumString, string listId, int selectedValue, string firstOptionHtml)
        //{
        //    return GetEnumToDropDownList(enumString, listId, selectedValue, firstOptionHtml, "", null);
        //}

        /// <summary>
        /// 将枚举输出为Radio
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="listName">Name</param>
        /// <param name="selectedValue">选中的值</param>
        /// <param name="attr">属性</param>
        /// <returns></returns>
        public static string GetEnumToRadio(Type enumType, string listName, int selectedValue, string attr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (int tempEnumValue in Enum.GetValues(enumType))
            {
                string tempEnumName = Enum.GetName(enumType, tempEnumValue);
                sb.AppendFormat(@"<label><input type=""radio"" value=""{0}"" name=""{1}"" id=""{1}_{0}"" {2}", tempEnumValue, listName, attr);
                if (tempEnumValue == selectedValue) sb.Append(@"checked=""checked""");
                sb.Append("/>");
                MemberInfo[] memInfo = enumType.GetMember(tempEnumName);
                if (memInfo != null && memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attrs != null && attrs.Length > 0)
                    {
                        sb.Append(((DescriptionAttribute)attrs[0]).Description);
                    }
                }
                sb.Append("</label>\r\n");
            }
            return sb.ToString();
        }
        #endregion
    }

    /// <summary>
    /// 枚举实体
    /// </summary>
    public class EnumModel
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }

}
