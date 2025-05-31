using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Infrastructure.Identities;

namespace Pizzeria.Infrastructure.Repos
{
    public static class AddDbContextExtension
    {

        public static IServiceCollection AddDbContextInfrastructure(this IServiceCollection services, IConfiguration config)
        {


            var connString = config.GetConnectionString("PizzeriaConn");

            services.AddDbContext<ApplicationUserContext>(options =>
                 options.UseSqlServer(connString)


            );

            services.AddDbContext<ApplicationUserContext>(options =>
                options.UseSqlServer(connString, x => x.MigrationsAssembly("Pizzeria.Infrastructure"))
);

            return services;
        }
    }
}
