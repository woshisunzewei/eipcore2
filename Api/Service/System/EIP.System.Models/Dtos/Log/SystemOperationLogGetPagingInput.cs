using EIP.Common.Core.Extensions;
using EIP.Common.Entities.Paging;
using System;

namespace EIP.System.Models.Dtos.Log
{
    public class SystemOperationLogGetPagingInput : QueryParam
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string CreateTime { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Describe { get; set; }

        public DateTime BeginCreateTime =>
            CreateTime.IsNullOrEmpty() ? DateTime.Now : TimeZone.CurrentTimeZone.ToUniversalTime(Convert.ToDateTime(CreateTime.Trim().Split("至")[0]));

        public DateTime? EndCreateTime =>
            CreateTime.IsNullOrEmpty() ? DateTime.Now : TimeZone.CurrentTimeZone.ToUniversalTime(Convert.ToDateTime(CreateTime.Trim().Split("至")[1]));
    }
}