using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVC.ECommercePricing.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IShoppingCartService, ShoppingCartService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, InMemoryProductRepository>();
        }
    }
}
