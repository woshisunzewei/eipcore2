using EIP.Common.Entities.Paging;

namespace EIP.System.Models.Dtos.Log
{
    public class SystemDataLogGetPagingInput: QueryParam
    {
        public string Name { get; set; }
    }
}