using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleMarket.Services.Advert.Infrastructure.ORM.Dapper;

namespace VehicleMarket.Services.Advert.Application.Helpers.Extensions
{
    public static class IServiceProviderExtensions
    {
        public static IServiceProvider BuildPosgtreDB(this IServiceProvider provider)
        {
            var service = provider.GetService<IAdvertRelationDatabaseBuilder>();

            service.Run();

            return provider;
        }
    }
}
