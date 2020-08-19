using AFORO255.MS.TEST.Security.Repository;
using AFORO255.MS.TEST.Security.Repository.Data;
using AFORO255.MS.TEST.Security.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MS.AFORO255.Cross.Jwt.Src;

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
            services.Configure<JwtOptions>(Configuration.GetSection("jwt"));
            services.AddControllers();

            services.AddDbContext<ContextDatabase>(
           opt =>
           {
               opt.UseSqlServer(Configuration["sqlserver:cn"]);
           });

            services.AddScoped<IAccessService, AccessService>();
            services.AddScoped<IAccessRepository, AccessRepository>();

            services.AddScoped<IContextDatabase, ContextDatabase>();

            services.AddControllers();
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
        }
    }
}
