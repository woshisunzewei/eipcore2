using EIP.Common.Restful;
using EIP.Common.Restful.Attribute;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using EIP.Common.Entities.Dtos;

namespace EIP.Controllers
{
    /// <summary>
    /// 监控
    /// </summary>
    public class MonitorController : BaseController
    {
        /// <summary>
        /// 获取所有程序集
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("系统监控-方法-获取所有程序集")]
        public JsonResult GetAllAssemblies()
        {
            var list = new List<AssembliesOutput>();
            var deps = DependencyContext.Default;
            //var libs = deps.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package");//排除所有的系统程序集、Nuget下载包
            foreach (var lib in deps.CompileLibraries)
            {
                try
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                    list.Add(new AssembliesOutput
                    {
                        Name = assembly.GetName().Name,
                        Version = assembly.GetName().Version.ToString(),
                        ClrVersion = assembly.ImageRuntimeVersion,
                        Location = assembly.Location
                    });
                }
                catch (Exception)
                {
                }
            }
            return JsonForGridLoadOnce(list);
        }
    }
}