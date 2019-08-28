using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos.Reports;
using EIP.Common.Entities.Paging;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Log
{
    public interface ISystemOperationLogLogic : IAsyncMongoDbLogic<SystemOperationLog>
    {
        /// <summary>
        ///     Excel导出方式
        /// </summary>
        /// <param name="paging">查询参数</param>
        /// <param name="excelReportDto"></param>
        /// <returns></returns>
        Task<OperateStatus> ReportExcelOperationLogQuery(QueryParam paging, ExcelReportDto excelReportDto);
    }
}