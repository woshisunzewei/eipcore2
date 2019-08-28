using System.Collections.Generic;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Enums;

namespace EIP.System.Models.Dtos.Config
{
    /// <summary>
    ///     代码生成器实体输入
    /// </summary>
    public class SystemCodeGenerationInput
    {
        /// <summary>
        ///     基础
        /// </summary>
        public SystemCodeGenerationBaseInput Base { get; set; }

        /// <summary>
        ///     列表
        /// </summary>
        public IEnumerable<SystemCodeGenerationListForListInput> List { get; set; }

        /// <summary>
        ///     编辑
        /// </summary>
        public IEnumerable<SystemCodeGenerationEditInput> Edit { get; set; }
    }

    /// <summary>
    ///     基础信息
    /// </summary>
    public class SystemCodeGenerationBaseInput : SystemDataBaseTableDoubleWay
    {
        /// <summary>
        ///     主键
        /// </summary>
        public string TableKey { get; set; }

        /// <summary>
        ///     实体类名
        /// </summary>
        public string Entity { get; set; }

        /// <summary>
        ///     DataAccess接口
        /// </summary>
        public string DataAccessInterface { get; set; }

        /// <summary>
        ///     DataAccess实现
        /// </summary>
        public string DataAccess { get; set; }

        /// <summary>
        ///     Business接口
        /// </summary>
        public string BusinessInterface { get; set; }

        /// <summary>
        ///     Business实现
        /// </summary>
        public string Business { get; set; }

        /// <summary>
        ///     控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        ///     列表页名
        /// </summary>
        public string List { get; set; }

        /// <summary>
        ///     表单页名
        /// </summary>
        public string Edit { get; set; }

        /// <summary>
        ///     表单详细
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        ///     js列表
        /// </summary>
        public string ListJs { get; set; }

        /// <summary>
        ///     js表单
        /// </summary>
        public string EditJs { get; set; }

        /// <summary>
        ///     实体路径
        /// </summary>
        public string EntityPath { get; set; }

        /// <summary>
        ///     DataAccess接口路径
        /// </summary>
        public string DataAccessInterfacePath { get; set; }

        /// <summary>
        ///     DataAccess实现路径
        /// </summary>
        public string DataAccessPath { get; set; }

        /// <summary>
        ///     Business接口路径
        /// </summary>
        public string BusinessInterfacePath { get; set; }

        /// <summary>
        ///     Business实现路径
        /// </summary>
        public string BusinessPath { get; set; }

        /// <summary>
        ///     控制器
        /// </summary>
        public string ControllerPath { get; set; }

        /// <summary>
        ///     js列表
        /// </summary>
        public string ListJsPath { get; set; }

        /// <summary>
        ///     js表单
        /// </summary>
        public string EditJsPath { get; set; }

        /// <summary>
        ///     是否分页
        /// </summary>
        public bool IsPaging { get; set; }

        /// <summary>
        /// 编辑框宽
        /// </summary>
        public int EditWidth { get; set; }

        /// <summary>
        /// 编辑框高
        /// </summary>
        public int EditHeight { get; set; }
    }

    /// <summary>
    ///     列表信息
    /// </summary>
    public class SystemCodeGenerationListForListInput : IInputDto
    {
        /// <summary>
        ///     字段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     显示名称
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        ///     排序名称
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        ///     显示列宽
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///     对齐方式
        /// </summary>
        public string Align { get; set; }

        /// <summary>
        ///     是否显示
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        ///     列宽是否重新计算
        /// </summary>
        public bool Fixed { get; set; }

        /// <summary>
        ///     自定义转换
        /// </summary>
        public string Formatter { get; set; }

        /// <summary>
        ///     排序类型
        /// </summary>
        public string Sorttype { get; set; }

        /// <summary>
        ///     排序
        /// </summary>
        public int OrderNo { get; set; } = 0;

        /// <summary>
        ///     是否排序
        /// </summary>
        public bool Sortable { get; set; }
    }

    /// <summary>
    ///     编辑界面信息
    /// </summary>
    public class SystemCodeGenerationEditInput : IInputDto
    {
        /// <summary>
        ///     控件Id/Name
        /// </summary>
        public string ControlId { get; set; }

        /// <summary>
        ///     控件名称
        /// </summary>
        public string ControlName { get; set; }

        /// <summary>
        ///     验证方式
        /// </summary>
        public EnumControlValidator ControlValidator { get; set; }

        /// <summary>
        ///     控件类型
        /// </summary>
        public EnumControlType ControlType { get; set; }

        /// <summary>
        ///     合并单元格
        /// </summary>
        public string ControlColspan { get; set; }

        /// <summary>
        ///     默认值
        /// </summary>
        public string ControlDefault { get; set; }
    }
}