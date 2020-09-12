using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.AFORO255.Cross.Jaeger.Jaeger;
using MS.AFORO255.Cross.Jwt.Src;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace AFORO255.MS.TEST.Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyPolicy = "_myPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*Start - Cors*/
            services.AddCors(o => o.AddPolicy(MyPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();

            }));
            services.AddRouting(r => r.SuppressCheckForUnhandledSecurityMetadata = true);
            /*End - Cors*/

            services.AddOcelot();
            services.AddJwtCustomized();

            /*Start Jaeger*/
            services.AddJaeger();
            services.AddOpenTracing();
            /*End Jaeger*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*Start - Cors*/
            app.UseCors(MyPolicy);
            app.Use((context, next) =>
            {
                context.Items["__CorsMiddlewareInvoked"] = true;
                return next();
            });
            /*End - Cors*/


            app.UseOcelot().Wait();
        }
    }
}
