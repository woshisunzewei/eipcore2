using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Config
{
    public interface ISystemCalendarRepository : IAsyncRepository<SystemCalendar>
    {
        /// <summary>
        ///查询日历
        /// </summary>
        /// <param name="st">The st.</param>
        /// <param name="ed">The ed.</param>
        /// <param name="userId">The use id.</param>
        /// <returns></returns>
        Task<IEnumerable<SystemCalendar>> QueryCalendars(DateTime st, DateTime ed, Guid userId);
    }
}