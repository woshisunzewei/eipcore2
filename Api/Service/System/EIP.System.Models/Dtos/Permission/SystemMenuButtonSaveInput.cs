using System;

namespace EIP.System.Models.Dtos.Permission
{
    public class SystemMenuButtonSaveInput
    {
        /// <summary>
        /// 
        /// </summary>	
        public Guid MenuButtonId { get; set; }

        /// <summary>
        /// 菜单id
        /// </summary>		
        public Guid MenuId { get; set; }

        /// <summary>
        /// 若是按钮则可为其添加图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 名称:新增、删除、编辑、读取数据....
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 脚本方法
        /// </summary>		
        public string Script { get; set; }

        /// <summary>
        /// 排序
        /// </summary>		
        public int OrderNo { get; set; } = 0;

        /// <summary>
        /// 冻结
        /// </summary>
        public bool IsFreeze { get; set; }

        /// <summary>
        /// 备注
        /// </summary>		
        public string Remark { get; set; }
    }
}