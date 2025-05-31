using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Core.Interfaces;
using Pizzeria.Core.Services;

namespace Pizzeria.Core.Extensions
{
    public static class PizzaExtension
    {
        public static IServiceCollection AddCoreDI(this IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAccountService, AccountService>();


            return services;
        }
    }
}
