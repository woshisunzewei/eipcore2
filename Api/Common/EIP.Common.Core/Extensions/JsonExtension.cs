using Newtonsoft.Json;
using System.Collections.Generic;

namespace EIP.Common.Core.Extensions
{
    /// <summary>
    /// Json扩展
    /// </summary>
    public static class JsonExtension
    {
        /// <summary>
        /// 字符串序列化为集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static List<T> JsonStringToList<T>(this string jsonStr)
        {
            return JsonConvert.DeserializeObject<List<T>>(jsonStr);
        }

        /// <summary>
        /// 字符串序列化为集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T JsonStringToObject<T>(this string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
       
        }

        /// <summary>
        /// 字符串序列化为集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ListToJsonString<T>(IEnumerable<T> t)
        {
            return JsonConvert.SerializeObject(t);
        }
    }
}