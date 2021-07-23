using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cashback.Data.Context;
using Cashback.Service.AutoMapper;
using Cashback.Service.DependencyInjection;
using Cashback.Swagger;
using Microsoft.EntityFrameworkCore;

namespace Cashback
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
            //services.AddControllersWithViews();
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling =
                                       Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //adding new connection to database
            string dbConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<CashbackContext>(opt => opt.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)).EnableSensitiveDataLogging());

            NativeInjector.RegisterServices(services);

            services.AddAutoMapper(typeof(AutoMapperSetup));

            services.AddHttpClient();

            services.AddSwaggerConfiguration();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerConfiguration();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
