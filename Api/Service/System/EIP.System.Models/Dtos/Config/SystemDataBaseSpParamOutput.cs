using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Config
{
    /// <summary>
    ///     存储过程参数
    /// </summary>
    public class SystemDataBaseSpParamOutput : IOutputDto
    {
        public string Name;
        public string CleanName;
        public string SysType;
        public string DbType;
        /*
         * 修改说明：添加存储过程说明参数，用于判断该参数是否是返回值
        *********************************************/
        public string Direction;
    }
}