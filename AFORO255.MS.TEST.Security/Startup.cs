using AFORO255.MS.TEST.Security.Repository;
using AFORO255.MS.TEST.Security.Repository.Data;
using AFORO255.MS.TEST.Security.Service;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MS.AFORO255.Cross.Consul.Consul;
using MS.AFORO255.Cross.Consul.Mvc;
using MS.AFORO255.Cross.Jaeger.Jaeger;
using MS.AFORO255.Cross.Jwt.Src;
using MS.AFORO255.Cross.Metrics.Registry;

namespace AFORO255.MS.TEST.Security
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
            services.AddControllers();
            services.Configure<JwtOptions>(Configuration.GetSection("jwt"));
         

            services.AddDbContext<ContextDatabase>(
           opt =>
           {
               //opt.UseSqlServer(Configuration["sqlserver:cn"]);
               opt.UseSqlServer(Configuration["cnsql"]);

           });

            services.AddScoped<IAccessService, AccessService>();
            services.AddScoped<IAccessRepository, AccessRepository>();
            services.AddScoped<IContextDatabase, ContextDatabase>();
            services.AddControllers();

            /*Start - Consul*/
            services.AddSingleton<IServiceId, ServiceId>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddConsul();
            /*End - Consul*/

            /*Start Jaeger*/
            services.AddJaeger();
            services.AddOpenTracing();
            /*End Jaeger*/


            /*Metricas - Prometheus*/
            /*Start - Metrics*/
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddTransient<IMetricsRegistry, MetricsRegistry>();
            /*End - Metrics*/

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime, IConsulClient consulClient, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            /*Log Centralizado - Seq*/
            if (bool.Parse(Configuration["seq:enabled"]) == true)
            {
                loggerFactory.AddSeq(Configuration["seq:url"], apiKey: Configuration["seq:token"]);
            }


            //Genera el ID  de consult
            var serviceId = app.UseConsul();
            hostApplicationLifetime.ApplicationStopped.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(serviceId);
            });
        }
    }
}
