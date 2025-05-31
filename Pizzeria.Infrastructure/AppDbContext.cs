using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Infrastructure.Identities;

namespace Pizzeria.Infrastructure
{
    public static class AppDbContext
    {
        public static IServiceCollection AddExtendedContext(this IServiceCollection services,
                                                           IConfiguration configuration
                                                   )
        {
            //            var connString = configuration["pizzeriaconnstring"];

            //            services.AddDbContext<ApplicationUserContext>(options =>

            //                    options.UseSqlServer(connString)

            //                );
            //            services.AddDbContext<ApplicationUserContext>(options =>
            //                options.UseSqlServer(connString, x => x.MigrationsAssembly("Pizzeria.Infrastructure"))
            //);

            //            return services;

            var connString = configuration["pizzeriaconnstring"];

            if (string.IsNullOrWhiteSpace(connString))
            {
                throw new InvalidOperationException("Connection string saknas eller är tom.");
            }

            services.AddDbContext<ApplicationUserContext>(options =>
                options.UseSqlServer(connString, sqlOptions =>
                    sqlOptions.MigrationsAssembly("Pizzeria.Infrastructure"))
            );

            return services;

        }
    }


}
