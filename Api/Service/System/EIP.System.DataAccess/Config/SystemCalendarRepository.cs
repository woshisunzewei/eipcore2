using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Config
{
    /// <summary>
    ///     日历
    /// </summary>
    public class SystemCalendarRepository : DapperAsyncRepository<SystemCalendar>, ISystemCalendarRepository
    {
        /// <summary>
        /// 获取日历信息
        /// </summary>
        /// <param name="st"></param>
        /// <param name="ed"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<IEnumerable<SystemCalendar>> QueryCalendars(DateTime st, DateTime ed, Guid userId)
        {
            const string sql = "SELECT * FROM System_Calendar WHERE UPAccount=@userId AND ((StartTime >= @st AND StartTime < @ed)  OR (EndTime >= @st AND EndTime < @ed) OR(StartTime<@st AND EndTime >@ed))";
            return SqlMapperUtil.SqlWithParams<SystemCalendar>(sql, new
            {
                st,
                ed,
                userId
            });
        }
    }
}