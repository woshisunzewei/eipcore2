using EIP.System.Models.Entities;

namespace EIP.System.Models.Dtos.Config
{
    public class SystemDistrictGetByIdOutput:SystemDistrict
    {
        /// <summary>
        /// 父级名称
        /// </summary>
        public string ParentName { get; set; }
    }
}
