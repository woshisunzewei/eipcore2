using EIP.Common.Restful.Attribute;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Dtos.Reports;
using EIP.Common.Entities.Paging;
using EIP.Common.Restful;
using EIP.System.Business.Log;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Core.Extensions;
using EIP.System.Models.Dtos.Log;
using EIP.System.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Driver;

namespace EIP.Controllers
{
    /// <summary>
    ///     日志管理控制器
    /// </summary>
    [Authorize]
    public class LogController : BaseController
    {
        #region 构造函数

        private readonly ISystemExceptionLogLogic _exceptionLogLogic;
        private readonly ISystemLoginLogLogic _loginLogLogic;
        private readonly ISystemOperationLogLogic _operationLogLogic;
        private readonly ISystemDataLogLogic _dataLogLogic;
        private readonly ISystemSqlLogLogic _sqlLogLogic;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionLogLogic"></param>
        /// <param name="loginLogLogic"></param>
        /// <param name="operationLogLogic"></param>
        /// <param name="dataLogLogic"></param>
        /// <param name="sqlLogLogic"></param>
        public LogController(ISystemExceptionLogLogic exceptionLogLogic,
            ISystemLoginLogLogic loginLogLogic,
            ISystemOperationLogLogic operationLogLogic,
            ISystemDataLogLogic dataLogLogic, ISystemSqlLogLogic sqlLogLogic)
        {
            _operationLogLogic = operationLogLogic;
            _dataLogLogic = dataLogLogic;
            _sqlLogLogic = sqlLogLogic;
            _exceptionLogLogic = exceptionLogLogic;
            _loginLogLogic = loginLogLogic;
        }

        #endregion

        #region 数据日志

        /// <summary>
        ///     获取所有数据日志
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("数据日志-方法-列表-获取所有数据日志")]
        public async Task<JsonResult> GetPagingDataLog(SystemDataLogGetPagingInput paging)
        {
            var list = new List<FilterDefinition<SystemDataLog>>
            {
                Builders<SystemDataLog>.Filter.Lt("CreateTime", DateTime.Now)
            };
            if (!paging.Name.IsNullOrEmpty())
                list.Add(Builders<SystemDataLog>.Filter.Where(w => w.CreateUserName.Contains(paging.Name)));

            var filter = Builders<SystemDataLog>.Filter.And(list);
            return JsonForGridPaging(await _dataLogLogic.PagingQueryProcAsync(filter, paging));
        }

        /// <summary>
        ///     根据主键获取数据日志
        /// </summary>
        /// <param name="input">主键Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("数据日志-方法-列表-根据主键获取数据日志")]
        public async Task<JsonResult> GetDataLogById(IdInput input)
        {
            return Json(await _dataLogLogic.GetByIdAsync(input.Id));
        }
        #endregion

        #region 异常日志

        /// <summary>
        ///     获取所有异常信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("异常日志-方法-列表-获取所有异常信息")]
        public async Task<JsonResult> GetPagingExceptionLog(SystemExceptionLogGetPagingInput paging)
        {
            var list = new List<FilterDefinition<SystemExceptionLog>>
            {
                Builders<SystemExceptionLog>.Filter.Lt("CreateTime", DateTime.Now)
            };
            if (!paging.Name.IsNullOrEmpty())
                list.Add(Builders<SystemExceptionLog>.Filter.Where(w => w.CreateUserName.Contains(paging.Name)));
            if (!paging.Code.IsNullOrEmpty())
                list.Add(Builders<SystemExceptionLog>.Filter.Where(w => w.CreateUserCode.Contains(paging.Code)));
            if (!paging.CreateTime.IsNullOrEmpty())
                list.Add(Builders<SystemExceptionLog>.Filter.Where(w => w.CreateTime <= paging.EndCreateTime && w.CreateTime >= paging.BeginCreateTime));

            var filter = Builders<SystemExceptionLog>.Filter.And(list);
            var sort = Builders<SystemExceptionLog>.Sort.Descending(d => d.CreateTime);
            var datas = await _exceptionLogLogic.PagingQueryProcAsync(filter, paging, sort);
            foreach (var data in datas.Data)
            {
                data.CreateTime = TimeZone.CurrentTimeZone.ToLocalTime(data.CreateTime);
            }
            return JsonForGridPaging(datas);
        }

        /// <summary>
        ///     根据主键获取异常明细
        /// </summary>
        /// <param name="input">主键Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("异常日志-方法-列表-根据主键获取异常明细")]
        public async Task<JsonResult> GetExceptionLogById(IdInput<int> input)
        {
            return Json(await _exceptionLogLogic.GetByIdAsync(input.Id));
        }

        /// <summary>
        ///     根据主键删除异常信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("异常日志-方法-列表-根据主键删除异常信息")]
        public async Task<JsonResult> DeleteExceptionLogById(IdInput<string> input)
        {
            return Json(await _exceptionLogLogic.DeleteByIdsAsync(input.Id));
        }

        /// <summary>
        ///     根据主键删除异常信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("异常日志-方法-列表-根据主键删除异常信息")]
        public async Task<JsonResult> DeleteExceptionLogAll()
        {
            var list = new List<FilterDefinition<SystemExceptionLog>>
            {
                Builders<SystemExceptionLog>.Filter.Lt("CreateTime", DateTime.Now)
            };
            var filter = Builders<SystemExceptionLog>.Filter.And(list);
            return Json(await _exceptionLogLogic.DeleteAllAsync(filter));
        }

        /// <summary>
        ///     导出到Excel
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("异常日志-方法-列表-导出到Excel")]
        public async Task<FileResult> ExportExcelToExceptionLog(QueryParam paging)
        {
            ExcelReportDto excelReportDto = new ExcelReportDto()
            {
                //TemplatePath = Server.MapPath("/") + "DataUser/Templates/System/Log/异常日志.xlsx",
                DownTemplatePath = "异常日志" + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + ".xlsx",
                Title = "异常日志.xlsx"
            };
            await _exceptionLogLogic.ReportExcelExceptionLogQuery(paging, excelReportDto);
            //return File(new FileStream(excelReportDto.DownPath, FileMode.Open), "application/octet-stream", Server.UrlEncode(excelReportDto.Title));
            return null;
        }

        #endregion

        #region 登录日志

        /// <summary>
        ///     获取所有登录日志信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("登录日志-方法-列表-获取所有登录日志信息")]
        public async Task<JsonResult> GetPagingLoginLog(SystemLoginLogGetPagingInput paging)
        {
            var list = new List<FilterDefinition<SystemLoginLog>>
            {
                Builders<SystemLoginLog>.Filter.Lt("CreateTime", DateTime.Now)
            };
            if (!paging.Name.IsNullOrEmpty())
                list.Add(Builders<SystemLoginLog>.Filter.Where(w => w.CreateUserName.Contains(paging.Name)));
            if (!paging.Code.IsNullOrEmpty())
                list.Add(Builders<SystemLoginLog>.Filter.Where(w => w.CreateUserCode.Contains(paging.Code)));
            if (!paging.CreateTime.IsNullOrEmpty())
                list.Add(Builders<SystemLoginLog>.Filter.Where(w => w.CreateTime <= paging.EndCreateTime&& w.CreateTime >= paging.BeginCreateTime));

            var filter = Builders<SystemLoginLog>.Filter.And(list);
            var sort = Builders<SystemLoginLog>.Sort.Descending(d => d.CreateTime);
            var datas = await _loginLogLogic.PagingQueryProcAsync(filter, paging, sort);
            foreach (var data in datas.Data)
            {
                data.CreateTime = TimeZone.CurrentTimeZone.ToLocalTime(data.CreateTime);
            }
            return JsonForGridPaging(datas);
        }

        /// <summary>
        ///     根据主键删除登录日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("异常日志-方法-列表-根据主键删除登录日志")]
        public async Task<JsonResult> DeleteLoginLogById(IdInput<string> input)
        {
            return Json(await _loginLogLogic.DeleteByIdsAsync(input.Id));
        }

        /// <summary>
        ///     根据主键删除登录日志
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("异常日志-方法-列表-根据主键删除登录日志")]
        public async Task<JsonResult> DeleteLoginLogAll()
        {
            var list = new List<FilterDefinition<SystemLoginLog>>
            {
                Builders<SystemLoginLog>.Filter.Lt("CreateTime", DateTime.Now)
            };
            var filter = Builders<SystemLoginLog>.Filter.And(list);
            return Json(await _loginLogLogic.DeleteAllAsync(filter));
        }

        /// <summary>
        ///     导出到Excel
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("登录日志-方法-列表-导出到Excel")]
        public async Task<FileResult> ExportExcelToLoginLog(QueryParam paging)
        {
            //ExcelReportDto excelReportDto = new ExcelReportDto()
            //{
            //    TemplatePath = Server.MapPath("/") + "DataUser/Templates/System/Log/登录日志.xlsx",
            //    DownTemplatePath = "登录日志" + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + ".xlsx",
            //    Title = "登录日志.xlsx"
            //};
            //await _loginLogLogic.ReportExcelLoginLogQuery(paging, excelReportDto);
            //return File(new FileStream(excelReportDto.DownPath, FileMode.Open), "application/octet-stream", Server.UrlEncode(excelReportDto.Title));
            return null;
        }
        #endregion

        #region 操作日志

        /// <summary>
        ///     获取所有操作日志信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("操作日志-方法-列表-获取所有操作日志信息")]
        public async Task<JsonResult> GetPagingOperationLog(SystemOperationLogGetPagingInput paging)
        {
            var list = new List<FilterDefinition<SystemOperationLog>>
            {
                Builders<SystemOperationLog>.Filter.Lt("CreateTime", DateTime.Now)
            };
            if (!paging.Name.IsNullOrEmpty())
                list.Add(Builders<SystemOperationLog>.Filter.Where(w => w.CreateUserName.Contains(paging.Name)));
            if (!paging.Describe.IsNullOrEmpty())
                list.Add(Builders<SystemOperationLog>.Filter.Where(w => w.Describe.Contains(paging.Describe)));
            if (!paging.Code.IsNullOrEmpty())
                list.Add(Builders<SystemOperationLog>.Filter.Where(w => w.CreateUserCode.Contains(paging.Code)));
            if (!paging.CreateTime.IsNullOrEmpty())
                list.Add(Builders<SystemOperationLog>.Filter.Where(w => w.CreateTime <= paging.EndCreateTime && w.CreateTime >= paging.BeginCreateTime));

            var filter = Builders<SystemOperationLog>.Filter.And(list);
            var sort = Builders<SystemOperationLog>.Sort.Descending(d => d.CreateTime);
            var datas = await _operationLogLogic.PagingQueryProcAsync(filter, paging, sort);
            foreach (var data in datas.Data)
            {
                data.CreateTime = TimeZone.CurrentTimeZone.ToLocalTime(data.CreateTime);
            }
            return JsonForGridPaging(datas);
        }

        /// <summary>
        ///     根据主键获取操作日志信息明细
        /// </summary>
        /// <param name="input">主键Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("操作日志-方法-列表-根据主键获取操作日志信息明细")]
        public async Task<JsonResult> GetOperationLogById(IdInput<int> input)
        {
            return Json(await _operationLogLogic.GetByIdAsync(input.Id));
        }

        /// <summary>
        ///     根据主键删除操作日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("异常日志-方法-列表-根据主键删除操作日志")]
        public async Task<JsonResult> DeleteOperationLogById(IdInput<string> input)
        {
            return Json(await _operationLogLogic.DeleteByIdsAsync(input.Id));

        }

        /// <summary>
        ///     根据主键删除操作日志
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("异常日志-方法-列表-根据主键删除登录日志")]
        public async Task<JsonResult> DeleteOperationLogAll()
        {
            var list = new List<FilterDefinition<SystemOperationLog>>
            {
                Builders<SystemOperationLog>.Filter.Lt("CreateTime", DateTime.Now)
            };
            var filter = Builders<SystemOperationLog>.Filter.And(list);
            return Json(await _operationLogLogic.DeleteAllAsync(filter));
        }

        /// <summary>
        ///     导出到Excel
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("操作日志-方法-列表-导出到Excel")]
        public async Task<FileResult> ExportExcelToOperationLog(QueryParam paging)
        {
            //ExcelReportDto excelReportDto = new ExcelReportDto()
            //{
            //    TemplatePath = Server.MapPath("/") + "DataUser/Templates/System/Log/操作日志.xlsx",
            //    DownTemplatePath = "操作日志" + string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now) + ".xlsx",
            //    Title = "操作日志.xlsx"
            //};
            //await _operationLogLogic.ReportExcelOperationLogQuery(paging, excelReportDto);
            //return File(new FileStream(excelReportDto.DownPath, FileMode.Open), "application/octet-stream", Server.UrlEncode(excelReportDto.Title));
            return null;

        }
        #endregion

        #region Sql日志

        /// <summary>
        ///     获取所有数据日志
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("Sql日志-方法-列表-获取所有Sql日志")]
        public async Task<JsonResult> GetPagingSqlLog(QueryParam paging)
        {
            var list = new List<FilterDefinition<SystemSqlLog>>
            {
                Builders<SystemSqlLog>.Filter.Lt("CreateTime", DateTime.Now)
            };
            var filter = Builders<SystemSqlLog>.Filter.And(list);
            return JsonForGridPaging(await _sqlLogLogic.PagingQueryProcAsync(filter, paging));
        }

        /// <summary>
        ///     根据主键获取数据日志
        /// </summary>
        /// <param name="input">主键Id</param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("Sql日志-方法-列表-根据主键获取Sql日志")]
        public async Task<JsonResult> GetSqlLogById(IdInput input)
        {
            return Json(await _sqlLogLogic.GetByIdAsync(input.Id));
        }

        /// <summary>
        ///     根据主键删除Sql日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("异常日志-方法-列表-根据主键删除Sql日志")]
        public async Task<JsonResult> DeleteSqlLogById(IdInput<string> input)
        {
            return Json(await _sqlLogLogic.DeleteByIdsAsync(input.Id));
        }

        /// <summary>
        ///     根据主键删除Sql日志
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("异常日志-方法-列表-根据主键删除Sql日志")]
        public async Task<JsonResult> DeleteSqlLogAll()
        {
            var list = new List<FilterDefinition<SystemSqlLog>>
            {
                Builders<SystemSqlLog>.Filter.Lt("CreateTime", DateTime.Now)
            };
            var filter = Builders<SystemSqlLog>.Filter.And(list);
            return Json(await _sqlLogLogic.DeleteAllAsync(filter));
        }
        #endregion

        #region 日志分析

        #region 浏览器分析


        /// <summary>
        /// 浏览器分析
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("日志分析-方法-获取浏览器分析数据")]
        public async Task<JsonResult> GetAnalysisForBrowser()
        {
            return Json(await _loginLogLogic.GetBrowserAnalysis());
        }
        #endregion

        #endregion
    }
}