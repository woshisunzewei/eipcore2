using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Config
{
    public interface ISystemCalendarLogic : IAsyncLogic<SystemCalendar>
    {
        /// <summary>
        ///查询日历
        /// </summary>
        /// <param name="st">开始时间</param>
        /// <param name="ed">结束时间</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<IEnumerable<SystemCalendar>> QueryCalendars(DateTime st, DateTime ed, Guid userId);
    }
}