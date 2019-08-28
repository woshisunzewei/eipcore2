using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos.Reports;
using EIP.Common.Entities.Paging;
using EIP.System.DataAccess.Log;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Log
{
    public class SystemOperationLogLogic : MongoDbLogic<SystemOperationLog>, ISystemOperationLogLogic
    {
        #region ¹¹Ôìº¯Êý

        private readonly ISystemOperationLogRepository _repository;

        public SystemOperationLogLogic(ISystemOperationLogRepository operationLogRepository)
            : base(operationLogRepository)
        {
            _repository = operationLogRepository;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="excelReportDto"></param>
        /// <returns></returns>
        public Task<OperateStatus> ReportExcelOperationLogQuery(QueryParam paging, ExcelReportDto excelReportDto)
        {
            throw new global::System.NotImplementedException();
        }
    }
}