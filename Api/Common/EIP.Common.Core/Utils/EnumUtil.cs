using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace EIP.Common.Core.Utils
{
    public static class EnumUtil
    {

        #region 根据下标值获取枚举名称
        /// <summary>
        /// 根据下标获取枚举名称
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetEnumNameByIndex<T>(int index)
        {
            return Enum.GetName(typeof(T), index);
        }
        #endregion

        /// <summary>
        /// 根据下标值获取枚举名称
        /// </summary>
        /// <param name="t"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string GetName(Type t,
            object v)
        {
            try
            {
                return Enum.GetName(t, v);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 把枚举转换为键值对集合
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="getText">获得值得文本</param>
        /// <returns>以枚举值为key，枚举文本为value的键值对集合</returns>
        public static Dictionary<Int32, String> EnumToDictionary(Type enumType, Func<Enum, String> getText)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("传入的参数必须是枚举类型！", "enumType");
            }
            Dictionary<Int32, String> enumDic = new Dictionary<int, string>();
            Array enumValues = Enum.GetValues(enumType);
            foreach (Enum enumValue in enumValues)
            {
                Int32 key = Convert.ToInt32(enumValue);
                String value = getText(enumValue);
                enumDic.Add(key, value);
            }
            return enumDic;
        }

        /// <summary>
        ///  获取枚举的中文描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum obj)
        {
            string objName = obj.ToString();
            Type t = obj.GetType();
            FieldInfo fi = t.GetField(objName);
            DescriptionAttribute[] arrDesc = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return arrDesc[0].Description;
        }
    }
}