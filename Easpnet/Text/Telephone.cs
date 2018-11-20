using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Easpnet.Text
{
    /// <summary>
    /// 移动电话类型
    /// </summary>
    public enum MobilePhoneNumberType
    { 
        其他 = 0,
        中国移动 = 1,
        中国联通 = 2,
        中国电信 = 3
    }

    public class Telephone
    {
        /// <summary>
        /// 判别手机号码类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static MobilePhoneNumberType GetMobilePhoneNumberType(string s)
        {
            //if (Regex.Match(s, "^1(3[4-9]|5[012789]|8[78])\\d{8}$").Success)
            //{ 
            //    return MobilePhoneNumberType.中国移动;
            //}
            //else if (Regex.Match(s, "^1(3[0-2]|5[56]|8[56])\\d{8}$").Success)
            //{
            //    return MobilePhoneNumberType.中国联通;
            //}
            //else if (Regex.Match(s,"^18[09]\\d{8}$").Success)
            //{
            //    return MobilePhoneNumberType.中国电信;
            //}
            //else
            //{
            //    return MobilePhoneNumberType.其他;
            //}

            if (s.StartsWith("134")
                || s.StartsWith("135")
                || s.StartsWith("136")
                || s.StartsWith("137")
                || s.StartsWith("138")
                || s.StartsWith("139")
                || s.StartsWith("150")
                || s.StartsWith("151")
                || s.StartsWith("152")
                || s.StartsWith("157")
                || s.StartsWith("158")
                || s.StartsWith("159")
                || s.StartsWith("147")
                || s.StartsWith("182")
                || s.StartsWith("187")
                || s.StartsWith("188"))
            {
                return MobilePhoneNumberType.中国移动;
            }
            else if (s.StartsWith("130")
                || s.StartsWith("131")
                || s.StartsWith("132")
                || s.StartsWith("156")
                || s.StartsWith("186")
                || s.StartsWith("155")
                || s.StartsWith("145")
                || s.StartsWith("185"))
            {
                return MobilePhoneNumberType.中国联通;
            }
            else if (s.StartsWith("180")
                || s.StartsWith("189")
                || s.StartsWith("133")
                || s.StartsWith("153"))
            {
                return MobilePhoneNumberType.中国电信;
            }
            else
            {
                return MobilePhoneNumberType.其他;
            }
        }
    }
}
