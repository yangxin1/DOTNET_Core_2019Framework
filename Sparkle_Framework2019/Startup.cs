using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DTO.Model.DTOCommon;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;

namespace Sparkle_Framework2019
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 配置类
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            #region swagger
            //添加swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "通用框架2019",
                    Version = "V1 ",
                    Description = "Sparkle831143",
                    Contact = new Contact { Name = "Sparkle：", Url = "/views/test.html", Email = "a4200322@live.com" }
                });
                //添加说明文档
                var basepath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlpath = Path.Combine(basepath, "Sparkle_Framework2019.xml");
                c.IncludeXmlComments(xmlpath);
            });
            #endregion

            #region 跨域
            //添加跨域访问
            services.AddCors(options =>
            {
                options.AddPolicy("Cors", builder => builder.AllowAnyOrigin() //添加跨域访问规则
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials());
            });
            #endregion

            #region 实体映射
            //实体映射
            ColumnMapper.SetMapper();
            #endregion

            #region 依赖注入
            var IOCbuilder = new ContainerBuilder();//建立容器
            List<Assembly> programlist = new List<Assembly> {Assembly.Load("Common"), Assembly.Load("DAL") };//批量反射程序集
            foreach (var q in programlist)
            {
                IOCbuilder.RegisterAssemblyTypes(q).AsImplementedInterfaces(); //注册容器
            }
            IOCbuilder.Populate(services);//将service注入到容器
            var ApplicationContainner = IOCbuilder.Build();//登记创建容器

            return new AutofacServiceProvider(ApplicationContainner);   //IOC接管
            #endregion

        }

        /// <summary>
        /// 中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory">日志工厂</param>
        [Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //Nlog日志
            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");

            //重定向
            app.UseHttpsRedirection();
            //访问静态文件
            app.UseStaticFiles();

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "sparkle");
            });
            app.UseMvc();
        }
    }
}
