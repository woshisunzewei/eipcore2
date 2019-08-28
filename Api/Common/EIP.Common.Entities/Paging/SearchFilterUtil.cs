using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace EIP.Common.Entities.Paging
{
    /// <summary>
    ///     说  明:处理查询规则
    ///     备  注:
    ///     编写人:孙泽伟-2015/03/27
    /// </summary>
    public static class SearchFilterUtil
    {
        /// <summary>
        /// 序列化Json字符串
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="json">条件</param>
        /// <returns>返回的实体信息</returns>
        public static T Deserialize<T>(this string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }
        /// <summary>
        ///     根据传入的json格式字符串,组装Sql语句
        /// </summary>
        /// <param name="filters">Jqgrid查询参数字符串</param>
        /// <returns>拼接后Sql</returns>
        public static string ConvertFilters(string filters)
        {
            var searchCase = string.Empty;
            if (!string.IsNullOrEmpty(filters))
            {
                var filter = filters.Deserialize<Filter>();
                string group = filter.groupOp == "AND" ? " AND " : " OR ";
                if (filter.groupOp != "AND")
                {
                    searchCase += " and (1<>1";
                }
                foreach (var rule in filter.rules)
                {
                    //过滤输入的数据
                    string data = StripSqlInjection(rule.data);
                    switch (rule.op)
                    {
                        case "eq": //等于
                            searchCase += GetFilter(group, rule.field, " ='" + data + "'");
                            break;
                        case "ne": //不等于
                            searchCase += GetFilter(group, rule.field, " !='" + data + "'");
                            break;
                        case "bw": //以...开始
                            searchCase += GetFilter(group, rule.field, " like '" + data + "%'");
                            break;
                        case "bn": //不以...开始
                            searchCase += GetFilter(group, rule.field, " not like '" + data + "%'");
                            break;
                        case "ew": //结束于
                            searchCase += GetFilter(group, rule.field, " like '%" + data + "'");
                            break;
                        case "en": //不结束于
                            searchCase += GetFilter(group, rule.field, " not like '%" + data + "'");
                            break;
                        case "lt": //小于
                            searchCase += GetFilter(group, rule.field, " <'" + data + "'");
                            break;
                        case "le": //小于等于
                            searchCase += GetFilter(group, rule.field, " <='" + data + "'");
                            break;
                        case "gt": //大于
                            searchCase += GetFilter(group, rule.field, " >'" + data + "'");
                            break;
                        case "ge": //大于等于
                            searchCase += GetFilter(group, rule.field, " >='" + data + "'");
                            break;
                        case "in": //包括
                            searchCase += GetFilter(group, rule.field, " in ('" + data + "')");
                            break;
                        case "ni": //不包含
                            searchCase += GetFilter(group, rule.field, " not in ('" + data + "')");
                            break;
                        case "cn"://包含
                            searchCase += GetFilter(group, rule.field, " like '%" + data + "%' ");
                            break;
                        case "nc"://不包含
                            searchCase += GetFilter(group, rule.field, " not like '%" + data + "%'");
                            break;
                        case "nu"://空值
                            searchCase += GetFilter(group, rule.field, " =null");
                            break;
                        case "nn"://非空值
                            searchCase += GetFilter(group, rule.field, " !=null");
                            break;
                        case "time"://针对时间特别处理
                            searchCase += GetFilter(group, rule.field, " between '" + data + " 00:00:00' AND '" + data + " 23:59:59'");
                            break;
                    }
                    if (filter.groupOp != "AND")
                    {
                        searchCase += ")";
                    }
                }
            }
            return searchCase;
        }

        private static string GetFilter(string group, string field, string formula)
        {
            string search = string.Empty, searchCase = string.Empty;
            var split = field.Split(',');
            if (split.Length > 1)
            {
                foreach (var fi in field.Split(','))
                {
                    search += fi + formula + " OR "/* " like '%" + data + "%' OR "*/;
                }
                searchCase += group + "(" + search.Substring(0, search.Length - 4) + ")";
            }
            else
            {
                searchCase += group + field + formula;
            }
            return searchCase;
        }

        /// <summary>
        /// 替换SQL注入特殊字符
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string StripSqlInjection(string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                //过滤 ' --
                const string pattern1 = @"(\%27)|(\')|(\-\-)";
                //防止执行 ' or
                const string pattern2 = @"((\%27)|(\'))\s*((\%6F)|o|(\%4F))((\%72)|r|(\%52))";
                //防止执行sql server 内部存储过程或扩展存储过程
                const string pattern3 = @"\s+exec(\s|\+)+(s|x)p\w+";
                sql = Regex.Replace(sql, pattern1, string.Empty, RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, pattern2, string.Empty, RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, pattern3, string.Empty, RegexOptions.IgnoreCase);
            }
            return sql;
        }
    }


    /// <summary>
    ///     Jqgrid查询规则类
    /// </summary>
    /// <remarks>2015-05-26 by 孙泽伟</remarks>
    public class Filter
    {
        public string groupOp { get; set; }
        public List<FilterRules> rules { get; set; }
    }

    /// <summary>
    ///     Jqgrid查询规则类,用于序列化查询实体用
    /// </summary>
    /// <remarks>2015-05-26 by 孙泽伟</remarks>
    public class FilterRules
    {
        /// <summary>
        ///     查询字段
        /// </summary>
        public string field { get; set; }

        /// <summary>
        ///     操作符
        /// </summary>
        public string op { get; set; }

        /// <summary>
        ///     查询数据
        /// </summary>
        public string data { get; set; }
    }
}