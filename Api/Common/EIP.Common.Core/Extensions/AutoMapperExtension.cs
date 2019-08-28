using System.Collections;
using System.Collections.Generic;
using AutoMapper;

namespace EIP.Common.Core.Extensions
{
    /// <summary>
    /// AutoMapper扩展:实体映射转换
    ///     基于:AutoMapper第三方组件
    /// </summary>
    public static class AutoMapperExtension
    {
        #region 类型映射
        /// <summary>
        /// 类型映射:类中间转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj">待转换的对象</param>
        /// <returns></returns>
        public static T MapTo<T>(this object obj)
        {
            if (obj == null) return default(T);
            Mapper.Map(obj.GetType(), typeof(T));
            return Mapper.Map<T>(obj);
        }
        #endregion

        #region 集合列表类型映射
        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="source">待转换的对象</param>
        /// <returns></returns>
        public static List<T> MapToList<T>(this IEnumerable source)
        {
            foreach (var first in source)
            {
                var type = first.GetType();
                Mapper.Map(type, typeof(T));
                break;
            }
            return Mapper.Map<List<T>>(source);
        }
        #endregion
        
        #region 集合列表类型映射
        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            return Mapper.Map<List<TDestination>>(source);
        }
        #endregion
    }
}
