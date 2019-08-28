using System.Collections.Generic;
using System.Text;
using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Config
{
    /// <summary>
    /// 存储过程
    /// </summary>
    public class SystemDataBaseSpOutput : IOutputDto
    {
        public string ClassName;
        public string CleanName;
        public string Name;
        public List<SystemDataBaseSpParamOutput> Parameters;

        public SystemDataBaseSpOutput()
        {
            Parameters = new List<SystemDataBaseSpParamOutput>();
        }

        public string ArgList
        {
            get
            {
                var sb = new StringBuilder();
                var loopCount = 1;
                foreach (var par in Parameters)
                {
                    sb.AppendFormat("{0} {1}", par.SysType, par.CleanName);
                    if (loopCount < Parameters.Count)
                        sb.Append(",");
                    loopCount++;
                }
                return sb.ToString();
            }
        }
    }
}