using System;

namespace EIP.System.Models.Dtos.Config
{
    public class SystemDictionaryEditInput
    {
        /// <summary>
        /// 字典Id
        /// </summary>
        public Guid DictionaryId { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public Guid ParentId { get; set; }
    }
}