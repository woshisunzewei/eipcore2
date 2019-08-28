using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIP.Common.Core.Extensions
{
    /// <summary>
    ///     集合扩展方法类
    /// </summary>
    public static class CollectionExtensions
    {
        #region IEnumerable的扩展

        /// <summary>
        ///     将集合展开并分别转换成字符串，再以指定的分隔符衔接，拼成一个字符串返回。默认分隔符为逗号
        /// </summary>
        /// <param name="collection"> 要处理的集合 </param>
        /// <param name="separator"> 分隔符，默认为逗号 </param>
        /// <returns> 拼接后的字符串 </returns>
        public static string ExpandAndToString<T>(this IEnumerable<T> collection, string separator = ",")
        {
            return collection.ExpandAndToString(t => t.ToString(), separator);
        }

        /// <summary>
        ///     循环集合的每一项，调用委托生成字符串，返回合并后的字符串。默认分隔符为逗号
        /// </summary>
        /// <param name="collection">待处理的集合</param>
        /// <param name="itemFormatFunc">单个集合项的转换委托</param>
        /// <param name="separetor">分隔符，默认为逗号</param>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <returns></returns>
        public static string ExpandAndToString<T>(this IEnumerable<T> collection, Func<T, string> itemFormatFunc,
            string separetor = ",")
        {
            collection = collection as IList<T> ?? collection.ToList();
            if (!collection.Any())
            {
                return null;
            }
            var sb = new StringBuilder();
            var i = 0;
            var count = collection.Count();
            foreach (var t in collection)
            {
                if (t != null)
                {
                    if (i == count - 1)
                    {
                        sb.Append(itemFormatFunc(t));
                    }
                    else
                    {
                        sb.Append(itemFormatFunc(t) + separetor);
                    }
                }
                i++;
            }
            return sb.ToString();
        }

        /// <summary>
        /// 分组
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }

        #endregion
    }
}