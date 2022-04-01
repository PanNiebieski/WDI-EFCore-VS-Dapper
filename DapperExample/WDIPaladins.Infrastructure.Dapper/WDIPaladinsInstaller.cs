using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WDIPaladins.Application;

namespace WDIPaladins.Infrastructure.Dapper
{
    public static class WDIPaladinsInstaller
    {
        public static IServiceCollection AddDapperServices(this IServiceCollection services,
           IConfiguration configuration)
        {
            var connection = configuration.
                GetConnectionString("WDIPaladinsConnectionString");

            services.AddScoped<IDBContext, DBContext>
                (
                    (services) =>
                    {
                        var c =
                        new DBContext(connection);
                        return c;
                    }
                );


            services.AddScoped<IPaladinsRepository, PaladinsRepository>();


            SqlMapper.RemoveTypeMap(typeof(DateTimeOffset));
            SqlMapper.AddTypeHandler(DateTimeHandler.Default);

            SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));

            return services;
        }
    }
}
