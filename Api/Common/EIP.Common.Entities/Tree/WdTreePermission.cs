using System;
using System.Collections.Generic;

namespace EIP.Common.Entities.Tree
{
    /// <summary>
    /// wdTree权限信息
    /// </summary>
    public class WdTreePermission
    {
        public Guid id { get; set; }

        public string name { get; set; }

        public string icon { get; set; }

        public IList<WdTreeEntity> tree { get; set; }
    }
}