﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ConfiguringApps.Infrastructure;
using Microsoft.Extensions.Configuration;

//c Update Startup.cs by adding service UptimeService which starts Stopwatch when UptimeService class is instantiated and ends Stopwatch when the application is ended.
//c Update Startup.cs by adding one middleware component app.UseMiddleware<ContentMiddleware>() to examine how middleware works.
//c Update Startup.cs by adding one middleware component app.UseMiddleware<ShortCircuitMiddleware>() which intercepts Http request from the client, and inspects HttpContext object if it contains "edge" in their Http request header and this either hands it to next middleware component or send 404 error contained in HttpContext.Response.StatusCode.
//c Update Startup.cs by adding code to register app.UseMiddleware<BrowserTypeMiddleware>().
//c Update Startup.cs by adding code to register app.UseMiddleware<ErrorMiddleware>().
//c Update Startup.cs by adding code to register app.UseMvc() which sets the middleware components for MVC system, including routing system. To complete MVC system work well, not only are MVC middleware componenets needed, but also services for MVC system are needed. It can be resolved by adding services.AddMvc() in ConfigureService(IServiceCollection services).
//c Update Startup.cs by removing middleware components which were used to examine middleware and adding exception-handling middlewares.

namespace ConfiguringApps
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UptimeService>();
            services.AddMvc();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if ((Configuration.GetSection("ShortCircuitMiddleware")?.
                GetValue<bool>("EnableBrowserShortCircuit")).Value)
            {
                app.UseMiddleware<BrowserTypeMiddleware>();
                app.UseMiddleware<ShortCircuitMiddleware>();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}