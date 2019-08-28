using System.Threading.Tasks;

namespace EIP.Common.Core.Utils
{
    public static class TaskUtil
    {
        public static readonly Task CompletedTask = Task.FromResult(true);
    }
}