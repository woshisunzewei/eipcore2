using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Config
{
    /// <summary>
    /// 数据库空间占用输出
    /// </summary>
    public class SystemDataBaseSpaceOutput : IOutputDto
    {
        /// <summary>
        ///     表名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     记录数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        ///     保留空间
        /// </summary>
        public string Reserved { get; set; }

        /// <summary>
        ///     使用空间
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        ///     索引使用空间
        /// </summary>
        public string IndexSize { get; set; }

        /// <summary>
        ///     未用空间
        /// </summary>
        public string Unused { get; set; }
    }
}