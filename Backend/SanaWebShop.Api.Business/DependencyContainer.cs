using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SanaWebShop.Api.Business.Implements;
using SanaWebShop.Api.Business.Interfaces;
using SanaWebShop.Api.Repository;

namespace SanaWebShop.Api.Business
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddBusinessRepos(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionStringName)
        {
            services.AddRepositories(configuration, connectionStringName);
            services.AddScoped<IActionsProductsBusiness, ActionsProductsBusiness>();
            services.AddScoped<IActionsCustomersBusiness, ActionsCustomersBusiness>();
            services.AddScoped<IActionsOrdersBusiness, ActionsOrdersBusiness>();
            return services;
        }
    }
}