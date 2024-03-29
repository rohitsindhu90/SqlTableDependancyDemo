﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Options;
using SignalRCore.Web;
using SignalRCore.Web.Hubs;
using WebApplication1.Hubs;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            
 //           services.AddLogging(config =>
 //           {
 //               config.AddDebug(); // Log to debug (debug window in Visual Studio or any debugger attached)
 //               config.AddConsole(); // Log to console (colored !)
 //           })
 //.Configure<LoggerFilterOptions>(options =>
 //{
 //    options.AddFilter<DebugLoggerProvider>(null /* category*/ , LogLevel.Information /* min level */);
 //    options.AddFilter<ConsoleLoggerProvider>(null  /* category*/ , LogLevel.Warning /* min level */);
 //});
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<ConfigurationSetting>(Configuration.GetSection("ConnectionStrings"));
            services.AddSingleton<InventoryDatabaseSubscription, InventoryDatabaseSubscription>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddFile(Configuration.GetSection("Logging"));
            loggerFactory.AddFile("Logs/myapp-{Date}.txt");

            if (env.IsDevelopment())
            {
                //logger.LogInformation("In Development environment");

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSqlTableDependency<InventoryDatabaseSubscription>(Configuration.GetConnectionString("DefaultConnection"));

        }
    }
}
