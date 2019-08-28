using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using EIP.Common.Entities.Dtos;
using EIP.Common.Restful.Jwt;
using EIP.Common.Restful.Middlewares;
using EIP.System.Models.Dtos.Config;
using EIP.System.Models.Dtos.Identity;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using EIP.Common.Quartz;
using EIP.Common.Restful.Filter;
using EIP.Common.Restful.Provider;

namespace EIP
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        bool _isDevelopment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
                options =>
                {
                    options.Filters.Add<ExceptionFilter>();
                    options.Filters.Add<ResultFilter>();
                    options.Filters.Add<ModelStateFilter>();
                    options.Filters.Add<ActionFilter>();
                    options.ModelMetadataDetailsProviders.Add(new RequiredBindingMetadataProvider());
                }).AddJsonOptions(
                options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region Swagger基础配置

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Configuration["Swagger:Version"], new Info
                {
                    Version = Configuration["Swagger:Version"],
                    Title = Configuration["Swagger:Title"],
                    Description = Configuration["Swagger:Description"],
                    TermsOfService = Configuration["Swagger:TermsOfService"],
                    Contact = new Contact
                    {
                        Name = Configuration["Swagger:Contact:Name"],
                        Email = Configuration["Swagger:Contact:Email"],
                        Url = Configuration["Swagger:Contact:Url"]
                    }
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "EIP.xml");
                c.IncludeXmlComments(xmlPath);
                c.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
            });
            #endregion

            #region 跨域
            services.AddCors(options => options.AddPolicy("EIPCors", p => p.WithOrigins(Configuration["Cors:Origins"]).AllowAnyMethod().AllowAnyHeader()));
            #endregion

            #region Jwt
            //获取配置文件
            services.Configure<JwtConfiguration>(jwtConfig =>
            {
                jwtConfig.Issuer = Configuration.GetSection("Jwt:Issuer").Value;
                jwtConfig.Secret = Configuration.GetSection("Jwt:Secret").Value;
            });

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(ConfigureJwtBearer);
            #endregion

            #region 注册Mapper
            RegisterMapper();
            #endregion

            #region  EIP基础配置
            services.Configure<EIPConfig>(Configuration.GetSection("EIP"));
            #endregion

            #region Autofac注入
            return RegisterAutofac(services);
            #endregion

        }

        /// <summary>
        /// JwtBearer配置
        /// </summary>
        /// <param name="config"></param>
        private void ConfigureJwtBearer(JwtBearerOptions config)
        {
            if (_isDevelopment)
                config.RequireHttpsMetadata = false;

            var issuer = Configuration.GetSection("Jwt:Issuer").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Jwt:Secret").Value));

            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = issuer,
                IssuerSigningKey = key,
                ValidateAudience = false,
            };
        }

        /// <summary>
        /// Mapper注册
        /// </summary>
        private void RegisterMapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<SystemDictionary, SystemDictionaryEditOutput>();
                config.CreateMap<SystemUserInfo, SystemUserOutput>();
                config.CreateMap<SystemUserInfo, SystemUserSaveInput>();
                config.CreateMap<SystemUserSaveInput, SystemUserInfo > ();
                config.CreateMap<SystemUserOutput, SystemUserDetailOutput>();
                config.CreateMap<SystemMenu, SystemMenuEditOutput>();
                config.CreateMap<SystemMenuButton, SystemMenuButtonOutput>();
                config.CreateMap<SystemMenuButton, SystemMenuButtonSaveInput>();
                config.CreateMap<SystemOrganization, SystemOrganizationOutput>();
                config.CreateMap<SystemRole, SystemRoleOutput>();
                config.CreateMap<SystemPost, SystemPostOutput>();
                config.CreateMap<SystemGroup, SystemGroupOutput>();
                config.CreateMap<SystemDistrict, SystemDistrictGetByIdOutput>();
            });
        }

        /// <summary>
        /// Autofac配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceProvider RegisterAutofac(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            var assemblys = GetAllAssemblies();
            builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => t.Name.EndsWith("Logic")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();
            return new AutofacServiceProvider(builder.Build());
        }

        /// <summary>
        /// 获取项目程序集，排除所有的系统程序集(Microsoft.***、System.***等)、Nuget下载包
        /// </summary>
        /// <returns></returns>
        public static IList<Assembly> GetAllAssemblies()
        {
            var list = new List<Assembly>();
            var deps = DependencyContext.Default;
            var libs = deps.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package");//排除所有的系统程序集、Nuget下载包
            foreach (var lib in libs)
            {
                try
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                    list.Add(assembly);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", Configuration["Swagger:Description"]);
                c.ShowRequestHeaders();
            });
            #endregion

            #region 错误中间件
            app.UseErrorHandlingMiddleware();
            #endregion

            #region 请求中间件
            app.UseRequestProviderMiddleware();
            #endregion

            #region 作业
            Job();
            #endregion

            if (env.IsDevelopment())
            {
                _isDevelopment = true;
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
            app.UseCors("EIPCors");//注册中间件
        }

        /// <summary>
        /// 启动作业
        /// </summary>
        private void Job()
        {
            StdSchedulerManager.Init();
            StdSchedulerManager.Start();
        }

        /// <summary>
        /// 请求头
        /// </summary>
        public class HttpHeaderOperation : IOperationFilter
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="operation"></param>
            /// <param name="context"></param>
            public void Apply(Operation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null)
                {
                    operation.Parameters = new List<IParameter>();
                }
                var actionAttrs = context.ApiDescription.ActionAttributes().ToList();
                var isAuthorized = actionAttrs.Any(a => a.GetType() == typeof(AuthorizeAttribute));
                if (isAuthorized == false)
                {
                    var controllerAttrs = context.ApiDescription.ControllerAttributes();
                    isAuthorized = controllerAttrs.Any(a => a.GetType() == typeof(AuthorizeAttribute));
                }
                var isAllowAnonymous = actionAttrs.Any(a => a.GetType() == typeof(AllowAnonymousAttribute));
                if (isAuthorized && isAllowAnonymous == false)
                {
                    operation.Parameters.Add(new NonBodyParameter
                    {
                        Name = "Authorization",
                        In = "header",
                        Type = "string",
                        Required = false
                    });
                }
            }
        }
    }
}
