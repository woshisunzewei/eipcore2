using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIP.System.Models.Entities
{
    /// <summary>
    /// System_Menu表实体类
    /// </summary>
    [Serializable]
    [Table( "System_Menu")]
    public class SystemMenu 
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        public Guid MenuId { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>		
        public Guid ParentId { get; set; }
        
        /// <summary>
        /// 名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>		
        public string Icon { get; set; }

        /// <summary>
        /// 打开类型
        /// </summary>		
        public byte? OpenType { get; set; }

        /// <summary>
        /// 打开地址
        /// </summary>		
        public string OpenUrl { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 允许删除
        /// </summary>		
        public bool CanbeDelete { get; set; }

        /// <summary>
        /// 备注
        /// </summary>		
        public string Remark { get; set; }

        /// <summary>
        /// 排序
        /// </summary>		
        public int OrderNo { get; set; }=0;

        /// <summary>
        /// 是否具有菜单权限
        /// </summary>
        public bool HaveMenuPermission { get; set; }

        /// <summary>
        /// 是否具有数据权限
        /// </summary>
        public bool HaveDataPermission { get; set; }

        /// <summary>
        /// 是否具有字段权限
        /// </summary>
        public bool HaveFieldPermission { get; set; }

        /// <summary>
        /// 是否具有功能项权限
        /// </summary>
        public bool HaveFunctionPermission { get; set; }

        /// <summary>
        /// 冻结
        /// </summary>
        public bool IsFreeze { get; set; }

        /// <summary>
        /// 是否显示到菜单
        /// </summary>
        public bool IsShowMenu { get; set; }

        /// <summary>
        /// 所有父级id(查询时使用)
        /// </summary>
        public string ParentIds { get; set; }
    }
}
