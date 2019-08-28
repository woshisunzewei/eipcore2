using System.Collections.Generic;

namespace EIP.Common.Entities.Tree
{
    /// <summary>
    /// Wd树形结构:模块权限
    /// </summary>
    public class WdTreeEntity
    {
        public WdTreeEntity()
        {
        }

        public WdTreeEntity(ZTreeEntity tree)
        {
            id = tree.id.ToString();
            text = tree.name;
            value = tree.id.ToString();
            url = tree.url;
            icon = tree.icon;
            if (!string.IsNullOrEmpty(icon))
            {
                img = string.Format("/Contents/images/icons/{0}.png", icon);
            }
        }

        /// <summary>
        ///     标识
        /// </summary>
        public string id { get; set; }

        /// <summary>
        ///     显示内容
        /// </summary>
        public string text { get; set; }

        /// <summary>
        ///     节点值
        /// </summary>
        public string value { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        ///     节点是否加载完（默认为True）
        /// </summary>
        public bool complete => true;

        /// <summary>
        ///     节点是否展开
        /// </summary>
        public bool isexpand { get; set; }

        /// <summary>
        ///     点击该节点，跳转的Url地址
        /// </summary>
        public string url { get; set; }

        /// <summary>
        ///     当前节点是否存在子节点
        /// </summary>
        public bool hasChildren => (ChildNodes != null && ChildNodes.Count > 0);

        /// <summary>
        ///     节点子节点
        /// </summary>
        public IList<WdTreeEntity> ChildNodes { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string img { get; set; }
    }
}