using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SanaWebShop.Api.Models.Contexts;

namespace SanaWebShop.Api.Models
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddConnectionDb(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionStringName)
        {
            services.AddDbContext<SanaWebShopContext>(options =>
            options.UseSqlServer(configuration
            .GetConnectionString(connectionStringName)));
            return services;
        }
    }
}