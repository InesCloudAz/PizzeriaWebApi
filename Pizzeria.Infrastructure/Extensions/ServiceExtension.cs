using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Infrastructure.Interfaces;
using Pizzeria.Infrastructure.Repos;

namespace Pizzeria.Infrastructure.Extensions
{
    public static class ServiceExtension
    {

        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {

            services.AddScoped<IItemRepo, ItemRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IIngredientRepo, IngredientRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IUserRepo, UserRepo>();

            return services;
        }
    }
}

