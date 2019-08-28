using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos.Reports;
using EIP.Common.Entities.Paging;
using EIP.System.DataAccess.Log;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Log
{
    public class SystemLoginLogLogic : MongoDbLogic<SystemLoginLog>, ISystemLoginLogLogic
    {
        #region 方法
        /// <summary>
        /// 获取日志分析报表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AnalysisBase>> GetBrowserAnalysis()
        {
            IList<SystemLoginLog> loginLogs = (await _loginLogRepository.GetBrowserAnalysis()).ToList();
            IList<AnalysisBase> analysisBases = new List<AnalysisBase>();
            return analysisBases;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="excelReportDto"></param>
        /// <returns></returns>
        public Task<OperateStatus> ReportExcelLoginLogQuery(QueryParam paging, ExcelReportDto excelReportDto)
        {
            throw new global::System.NotImplementedException();
        }

        #endregion

        #region 构造函数

        private readonly ISystemLoginLogRepository _loginLogRepository;

        public SystemLoginLogLogic(ISystemLoginLogRepository loginLogRepository)
            : base(loginLogRepository)
        {
            _loginLogRepository = loginLogRepository;
        }

        #endregion
    }
}