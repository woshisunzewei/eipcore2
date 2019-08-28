using System.IO;

namespace EIP.Common.Entities.Dtos.Reports
{
    /// <summary>
    /// 基础导出类
    /// </summary>
    public class BaseReportDto : IDto
    {
        /// <summary>
        /// 模版地址
        /// </summary>
        public string TemplatePath { get; set; }

        /// <summary>
        /// 导出地址
        /// </summary>
        private string ExportPath
        {
            get
            {
                string exportPath = "D://Export";
                if (!Directory.Exists(exportPath))
                {
                    Directory.CreateDirectory(exportPath);
                }
                return exportPath;
            }
        }

        /// <summary>
        /// 下载模版地址
        /// </summary>
        public string DownTemplatePath { get; set; }
        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownPath
        {
            get { return ExportPath + DownTemplatePath; }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
    }
}