using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Config
{
    public class SystemDataBaseFkTableOuptut : IOutputDto
    {
        public string OtherClass;
        public string OtherColumn;
        public string OtherQueryable;
        public string OtherTable;
        public string ThisColumn;
        public string ThisTable; 
    }
}