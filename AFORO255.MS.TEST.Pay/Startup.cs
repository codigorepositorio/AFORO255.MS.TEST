using AFORO255.MS.TEST.Pay.RabbitMQ.CommandHandlers;
using AFORO255.MS.TEST.Pay.RabbitMQ.Commands;
using AFORO255.MS.TEST.Pay.Repository;
using AFORO255.MS.TEST.Pay.Repository.Data;
using AFORO255.MS.TEST.Pay.Service;
using Consul;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MS.AFORO255.Cross.Consul.Consul;
using MS.AFORO255.Cross.Consul.Mvc;
using MS.AFORO255.Cross.Proxy.Proxy;
using MS.AFORO255.Cross.RabbitMQ.Src;

namespace AFORO255.MS.TEST.Pay
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


            services.AddDbContext<ContextDataBase>(
                 opt =>
                 {
                     //opt.UseMySQL(Configuration["mysql:cn"]);
                     opt.UseMySQL(Configuration["cnmysql"]); 
                 });

            services.AddTransient<IContextDataBase, ContextDataBase>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IInvoiceService, InvoiceService>();

            services.AddControllers();

            //Start RabbitMQ
            services.AddMediatR(typeof(Startup));
            services.AddRabbitMQ();

            services.AddTransient<IRequestHandler<InvoiceCreatedCommand, bool>, InvoiceCommandHandler>();
            services.AddTransient<IRequestHandler<TransactionPayCreatedCommand, bool>, TransactionPayCommandHandler>();
            services.AddTransient<IRequestHandler<NotificationCreateCommand, bool>, NotificationCommandHandler>();

            services.AddProxyHttp();


            /*Start - Consul*/
            services.AddSingleton<IServiceId, ServiceId>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddConsul();
            /*End - Consul*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime, IConsulClient consulClient)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            //Genera el ID  de consult
            var serviceId = app.UseConsul();
            hostApplicationLifetime.ApplicationStopped.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(serviceId);
            });
        }
    }
}
