using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EIP.System.Models.Dtos.Identity
{
    public class SystemOrganizationChartOutput
    {
        /// <summary>
        /// 名称
        /// </summary>
        [JsonIgnore]
        public Guid id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 下级
        /// </summary>
        public IList<SystemOrganizationChartOutput> children { get; set; }
    }

}