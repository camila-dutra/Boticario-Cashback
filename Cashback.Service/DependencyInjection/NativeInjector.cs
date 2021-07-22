using System;
using System.Collections.Generic;
using System.Text;
using Cashback.Data.Interfaces;
using Cashback.Data.Repository;
using Cashback.Service.Interfaces;
using Cashback.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cashback.Service.DependencyInjection
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services

            services.AddScoped<IResellerService, ResellerService>();

            #endregion

            #region Repositories

            services.AddScoped<IResellerRepository, ResellerRepository>();

            #endregion

        }
    }
}
