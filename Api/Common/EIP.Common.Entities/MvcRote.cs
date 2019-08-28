using System;

namespace EIP.Common.Entities
{
    /// <summary>
    /// 路由实体
    /// </summary>
    public class MvcRote
    {
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
        /// 完整地址
        /// </summary>
        public string Url
        {
            get { return Area + "/" + Controller + "/" + Action; }
            set
            {
                Area = value.Split('/')[0];
                Controller = value.Split('/')[1];
                Action = value.Split('/')[2];
            }
        }

        /// <summary>
        /// 开发者代码
        /// </summary>
        public string ByDeveloperCode { get; set; }

        /// <summary>
        /// 开发者时间
        /// </summary>
        public string ByDeveloperTime { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否为界面
        /// </summary>
        public bool IsPage { get; set; }

        /// <summary>
        /// 应用系统代码
        /// </summary>
        public string AppCode { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }
    }
}