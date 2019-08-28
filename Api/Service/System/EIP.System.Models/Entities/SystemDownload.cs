using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EIP.Common.Entities.CustomAttributes;

namespace EIP.System.Models.Entities
{
    /// <summary>
    /// 文章下载记录表表实体类
    /// </summary>
    [Serializable]
    [Table( "System_Download")]
    [Db("EIP")]
    public class SystemDownload 
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public Guid DownloadId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 归属类别
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 是否冻结
        /// </summary>
        public bool IsFreeze { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 是否在主页显示
        /// </summary>
        public bool ShowInHome { get; set; }

        /// <summary>
        /// 下载次数
        /// </summary>
        public int DownloadNums { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNo { get; set; }=0;

        /// <summary>
        /// 类别
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 发布机构编号
        /// </summary>
        public Guid CreateOrganizationId { get; set; }

        /// <summary>
        /// 发布机构名称
        /// </summary>
        public string CreateOrganizationName { get; set; }

        /// <summary>
        /// 发布用户编号
        /// </summary>
        public Guid CreateUserId { get; set; }

        /// <summary>
        /// 发布用户姓名
        /// </summary>
        public string CreateUserName { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改机构编号
        /// </summary>
        public Guid UpdateTimeOrganizationId { get; set; }

        /// <summary>
        /// 修改机构名称
        /// </summary>
        public string UpdateTimeOrganizationName { get; set; }

        /// <summary>
        /// 修改用户编号
        /// </summary>
        public Guid UpdateUserId { get; set; }

        /// <summary>
        /// 修改用户名称
        /// </summary>
        public string UpdateUserName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }


    }
}