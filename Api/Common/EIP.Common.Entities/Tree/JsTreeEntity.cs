using System;
using Newtonsoft.Json;

namespace EIP.Common.Entities.Tree
{
    public class JsTreeEntity
    {
        /// <summary>
        ///     主键
        /// </summary>
        public Object id { get; set; }

        /// <summary>
        ///     父级
        /// </summary>
        public Object parent { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string text { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public JsTreeStateEntity state { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [JsonIgnore]
        public string url { get; set; }

        /// <summary>
        /// 设置
        /// </summary>
        public bool children { get; set; }
    }

    public class JsTreeStateEntity
    {
        public bool opened { get; set; } = true;
    }
}