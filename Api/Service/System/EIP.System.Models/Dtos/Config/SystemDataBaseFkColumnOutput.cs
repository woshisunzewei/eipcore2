using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Config
{
    public class SystemDataBaseFkColumnOutput : IOutputDto
    {
        public string ThisTable { get; set; }

        public string ThisColumn { get; set; }

        public string OtherTable { get; set; }

        public string OtherColumn { get; set; }

        public string ConstraintName { get; set; }

        public string Owner { get; set; }
    }
}