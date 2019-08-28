using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIP.System.Models.Entities
{
    /// <summary>
    /// 登录幻灯片表实体类
    /// </summary>
    [Serializable]
    [Table( "System_LoginSlide")]
    public class SystemLoginSlide 
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public Guid LoginSlideId { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Src { get; set; }

        /// <summary>
        /// 图片标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 冻结
        /// </summary>
        public bool IsFreeze { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNo { get; set; }=0;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }


    }
}