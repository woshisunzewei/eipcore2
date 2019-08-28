using System;

namespace EIP.Common.Entities.Tree
{
    /// <summary>
    ///     Ztree树结构
    /// </summary>
    public class ZTreeEntity
    {
        /// <summary>
        ///     主键
        /// </summary>
        public Object id { get; set; }

        /// <summary>
        ///     父级
        /// </summary>
        public Object pId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///     打开的地址
        /// </summary>
        public string url { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        ///     展开时的图标
        /// </summary>
        public string iconOpen { get; set; }

        /// <summary>
        ///     收缩时的图标
        /// </summary>
        public string iconClose { get; set; }

        /// <summary>
        ///     表示是否显示单选或多选框
        /// </summary>
        public bool nocheck { get; set; }

        /// <summary>
        ///     图标样式
        /// </summary>
        public string iconSkin { get; set; }

        /// <summary>
        ///     是否是父级
        /// </summary>
        public bool isParent { get; set; }

        /// <summary>
        ///     是否默认打开
        /// </summary>
        public bool open { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 是否有右键功能
        /// </summary>
        public bool noR { get; set; }
    }
}
