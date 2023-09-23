using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SanaWebShop.Api.Models;
using SanaWebShop.Api.Repository.Implements;
using SanaWebShop.Api.Repository.Interfaces;

namespace SanaWebShop.Api.Repository
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionStringName)
        {
            services.AddConnectionDb(configuration, connectionStringName);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}