using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EIP.System.Models.Entities
{
    /// <summary>
    /// System_Field表实体类
    /// </summary>
	[Serializable]
	[Table( "System_Field")]
    public class SystemField
    {
       
        /// <summary>
        /// 字段主键
        /// </summary>		
		[Key]
        public Guid FieldId{ get; set; }
       
        /// <summary>
        /// 菜单id
        /// </summary>		
		public Guid MenuId{ get; set; }
       
        /// <summary>
        /// 字段名称
        /// </summary>		
		public string Name{ get; set; }

        /// <summary>
        /// 查询sql字段
        /// </summary>		
        public string SqlField { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>		
		public string Label{ get; set; }
       
        /// <summary>
        /// 排序名称
        /// </summary>		
		public string Index{ get; set; }
       
        /// <summary>
        /// 显示列宽
        /// </summary>		
		public int? Width{ get; set; }
       
        /// <summary>
        /// 对齐方式
        /// </summary>		
		public string Align{ get; set; }
       
        /// <summary>
        /// 是否显示
        /// </summary>		
        public bool Hidden{ get; set; }
       
        /// <summary>
        /// 列宽是否重新计算
        /// </summary>		
		public bool Fixed{ get; set; }
       
        /// <summary>
        /// 自定义转换
        /// </summary>		
		public string Formatter{ get; set; }
       
        /// <summary>
        /// 排序类型
        /// </summary>		
		public string Sorttype{ get; set; }
       
        /// <summary>
        /// 排序
        /// </summary>		
		public int  OrderNo { get; set; }=0;
       
        /// <summary>
        /// 备注
        /// </summary>		
		public string Remark{ get; set; }
       
        /// <summary>
        /// 导出/打印
        /// </summary>		
		public bool CanbeDerive{ get; set; }
       
        /// <summary>
        /// 是否冻结
        /// </summary>		
		public bool IsFreeze{ get; set; }
   } 
}
