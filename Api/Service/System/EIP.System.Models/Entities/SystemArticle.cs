using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIP.System.Models.Entities
{
    /// <summary>
    /// 文章新闻表表实体类
    /// </summary>
    [Serializable]
    [Table("System_Article")]
    public class SystemArticle 
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public Guid ArticleId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 栏目
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 栏目
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 是否冻结
        /// </summary>
        public bool IsFreeze { get; set; }

        /// <summary>
        /// 是否在主页显示
        /// </summary>
        public bool ShowInHome { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ViewNums { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNo { get; set; }=0;

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
        public Guid UpdateOrganizationId { get; set; }

        /// <summary>
        /// 修改机构名称
        /// </summary>
        public string UpdateOrganizationName { get; set; }

        /// <summary>
        /// 修改用户编号
        /// </summary>
        public Guid? UpdateUserId { get; set; }

        /// <summary>
        /// 修改用户名称
        /// </summary>
        public string UpdateUserName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }


    }
}