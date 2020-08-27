using AFORO255.MS.TEST.Notification.RabbitMQ.EventHandler;
using AFORO255.MS.TEST.Notification.RabbitMQ.Events;
using AFORO255.MS.TEST.Notification.Repository;
using AFORO255.MS.TEST.Notification.Service;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MS.AFORO255.Cross.RabbitMQ.Src;
using MS.AFORO255.Cross.RabbitMQ.Src.Bus;

namespace AFORO255.MS.TEST.Notification
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

            services.AddTransient<IRepositoryHistory, RepositoryHistory>();
            services.AddTransient<IHistoryService, HistoryService>();

            ///*Start - RabbitMQ */
            services.AddMediatR(typeof(Startup));
            services.AddRabbitMQ();

            services.AddTransient<PayEventHandler>();
            services.AddTransient<IEventHandler<PayCreatedEvent>, PayEventHandler>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<PayCreatedEvent, PayEventHandler>();            
        }
    }
}
