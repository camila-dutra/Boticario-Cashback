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
using System.Text;
using System.Threading.Tasks;
using Cashback.Auth.Models;
using Cashback.Data.Context;
using Cashback.Data.Repository;
using Cashback.Domain.Entities;
using Cashback.Logger;
using Cashback.Service.AutoMapper;
using Cashback.Service.DependencyInjection;
using Cashback.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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


            //adding new connection to database
            string dbConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<CashbackContext>(opt => opt.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)).EnableSensitiveDataLogging());

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<LoggerRepository>>();
            services.AddSingleton(typeof(ILogger), logger);

            //services.AddControllersWithViews();
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling =
                                       Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            NativeInjector.RegisterServices(services);

            services.AddAutoMapper(typeof(AutoMapperSetup));

            services.AddHttpClient();

            services.AddSwaggerConfiguration();


            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
                                       {
                                           x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                           x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                       }).AddJwtBearer(x =>
                                                       {
                                                           x.RequireHttpsMetadata = false;
                                                           x.SaveToken = true;
                                                           x.Audience = "iqimplict";
                                                           x.TokenValidationParameters = new TokenValidationParameters
                                                               {
                                                                   ValidateIssuerSigningKey = true,
                                                                   IssuerSigningKey = new SymmetricSecurityKey(key),
                                                                   ValidateIssuer = false,
                                                                   ValidateAudience = false
                                                               };
                                                       });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddContext(LogLevel.Information); loggerFactory.AddContext(LogLevel.Information);

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


            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<CashbackContext>();
                context.Database.Migrate();
            }
        }

    }
}
