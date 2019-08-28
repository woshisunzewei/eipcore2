using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using EIP.Common.Core.Utils;
using Quartz;

namespace EIP.Job.Business
{
    public class DatabaseBackupJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            StringBuilder logBuilder = new StringBuilder("开始执行数据库定时备份作业【" + DateTime.Now + "】</br>");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            JobKey jobKey = context.JobDetail.Key;
            // 获取传递过来的参数            
            JobDataMap data = context.JobDetail.JobDataMap;
            string dbConnection = data.GetString("BackupConnection_Solution");
            sw.Stop();
            return TaskUtil.CompletedTask;
        }
    }
}
