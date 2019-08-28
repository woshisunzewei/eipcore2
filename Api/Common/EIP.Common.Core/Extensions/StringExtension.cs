using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace EIP.Common.Core.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// 判断字符串是否相等
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        /// <returns></returns>
        public static bool EqualsEx(this string text1, string text2)
        {
            return string.Equals(text1, text2, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 根据传入的字符串组装为符合更新或者删除sql语句in部分的字符串(字符串必须以‘,’分割)
        /// </summary>
        /// <param name="value">扩展类型</param>
        /// <returns>替换后的字符串</returns>
        /// <remarks>2015-05-15 by 孙泽伟</remarks>
        public static string InSql(this string value)
        {
            string param = "";
            if (!string.IsNullOrEmpty(value))
            {
                var strList = value.Split(',');
                param = strList.Aggregate(param, (current, str) => current + ("'" + str + "',"));
            }
            return param.TrimEnd(',');
        }

        /// <summary>
        /// 判断是否包含字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ContainsEx(this string text, string value)
        {
            return text.IndexOf(value, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        /// <summary>
        /// 判断是否以指定字符串开头
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool StartWithEx(this string text, string value)
        {
            return text.StartsWith(value, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 判断是否以指定字符串结尾
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool EndWithEx(this string text, string value)
        {
            return text.EndsWith(value, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 判断字符串是否空
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }

        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 以指定字符串作为分隔符将指定字符串分隔成数组
        /// </summary>
        /// <param name="value">要分割的字符串</param>
        /// <param name="strSplit">字符串类型的分隔符</param>
        /// <param name="removeEmptyEntries">是否移除数据中元素为空字符串的项</param>
        /// <returns>分割后的数据</returns>
        public static string[] Split(this string value, string strSplit, bool removeEmptyEntries = false)
        {
            return value.Split(new[] { strSplit },
                removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
        }

        /// <summary>
        /// 替换Html标签
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns>替换后的字符串</returns>
        public static string ReplaceHtmlTag(this string value, int length = 0)
        {
            if (value.IsNullOrEmpty()) return value;
            string strText = Regex.Replace(value, "<[^>]+>", "");
            strText = Regex.Replace(strText, "&[^;]+;", "");
            if (length > 0 && strText.Length > length)
                return strText.Substring(0, length);
            return strText;
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ReplaceFistLower(this string value)
        {
            return value.Substring(0, 1).ToLower() + value.Substring(1);
        }

        /// <summary>
        /// 过滤Sql
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FilterSql(this string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            s = s.Trim().ToLower();
            s = s.Replace("=", "");
            s = s.Replace("'", "");
            s = s.Replace(";", "");
            s = s.Replace(" or ", "");
            s = s.Replace("select", "");
            s = s.Replace("update", "");
            s = s.Replace("insert", "");
            s = s.Replace("delete", "");
            s = s.Replace("declare", "");
            s = s.Replace("exec", "");
            s = s.Replace("drop", "");
            s = s.Replace("create", "");
            s = s.Replace("%", "");
            s = s.Replace("--", "");
            s = s.Replace("master", "");
            s = s.Replace("truncate", "");
            s = s.Replace("xp_", "no");
            return s;
        }

    }
}