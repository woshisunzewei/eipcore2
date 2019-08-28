using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos.Reports;
using EIP.Common.Entities.Paging;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Log
{
    public interface ISystemLoginLogLogic : IAsyncMongoDbLogic<SystemLoginLog>
    {
        /// <summary>
        /// 获取日志分析报表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AnalysisBase>> GetBrowserAnalysis();

        /// <summary>
        ///     Excel导出方式
        /// </summary>
        /// <param name="paging">查询参数</param>
        /// <param name="excelReportDto"></param>
        /// <returns></returns>
        Task<OperateStatus> ReportExcelLoginLogQuery(QueryParam paging, ExcelReportDto excelReportDto);
    }
}