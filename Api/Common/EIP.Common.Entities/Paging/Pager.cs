using System.Collections.Generic;

namespace EIP.Common.Entities.Paging
{
    /// <summary>
    ///     说  明:分页信息
    ///     备  注:继承QueryParam,由于在界面上只需传入基础参数,页码,记录总数无须传入,所以此次使用继承来使用继承原始
    ///     编写人:孙泽伟-2015/03/25
    /// </summary>
    public class PagerInfo : QueryParam
    {
        /// <summary>
        ///     页码总数
        /// </summary>
        public long PageCount { get; set; }
    }

    /// <summary>
    ///     说  明:分页信息
    ///     备  注:
    ///     编写人:孙泽伟-2015/03/25
    /// </summary>
    public class PagedResults<T>
    {
        /// <summary>
        ///     分页信息
        /// </summary>
        public PagerInfo PagerInfo { get; set; }

        /// <summary>
        ///     查询出来数据
        /// </summary>
        public IList<T> Data { get; set; }
    }
}