using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WDIPaladins.Application;
using Microsoft.Extensions.Configuration;

namespace WDIPaladins.Infrastructure.EFCore
{
    public static class WDIPaladinsInstaller
    {

        public static IServiceCollection AddEFServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PaladinsContext>(options =>
                options.UseSqlite(configuration.
                GetConnectionString
                ("WDIPaladinsConnectionStringEF")));


            services.AddScoped<IPaladinsRepository, PaladinsRepository>();

            return services;
        }
    }
}
