using System;
using System.Collections.Generic;
using System.Data;
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
    public class SystemExceptionLogLogic : MongoDbLogic<SystemExceptionLog>, ISystemExceptionLogLogic
    {
        #region 构造函数

        private readonly ISystemExceptionLogRepository _exceptionLogRepository;

        public SystemExceptionLogLogic(ISystemExceptionLogRepository exceptionLogRepository)
            : base(exceptionLogRepository)
        {
            _exceptionLogRepository = exceptionLogRepository;
        }

        public async Task<OperateStatus> ReportExcelExceptionLogQuery(QueryParam paging,
            ExcelReportDto excelReportDto)
        {
            var operateStatus = new OperateStatus();
            try
            {
                //获取总共条数
                //paging.Rows =await Count();
                //组装数据
                //IList<SystemExceptionLog> dtos = (await _exceptionLogRepository.PagingQueryProcAsync(paging)).Data.ToList();
                var tables = new Dictionary<string, DataTable>(StringComparer.OrdinalIgnoreCase);
                //组装需要导出数据
                //var dt = new DataTable("ExceptionLog");
                //dt.Columns.Add("Num");
                //dt.Columns.Add("OperateTime");
                //dt.Columns.Add("Code");
                //dt.Columns.Add("Message");
                //dt.Columns.Add("RequestUrl");
                //dt.Columns.Add("ClientHost");

                //var num = 1;
                //if (dtos.Any())
                //{
                //    foreach (var dto in dtos)
                //    {
                //        var row = dt.NewRow();
                //        dt.Rows.Add(row);
                //        row[0] = num;
                //        row[1] = dto.CreateTime;
                //        row[2] = dto.CreateUserCode;
                //        row[3] = dto.Message;
                //        row[4] = dto.RequestUrl;
                //        row[5] = dto.ClientHost;
                //        num++;
                //    }
                //}
                //tables.Add(dt.TableName, dt);
                //OpenXmlExcel.ExportExcel(excelReportDto.TemplatePath, excelReportDto.DownPath, tables);
                operateStatus.ResultSign = ResultSign.Successful;
            }
            catch (Exception)
            {
                operateStatus.ResultSign = ResultSign.Error;
            }
            return operateStatus;
        }
        #endregion
    }
}