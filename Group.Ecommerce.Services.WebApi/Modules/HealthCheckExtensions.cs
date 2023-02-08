using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Group.Ecommerce.Services.WebApi.Modules
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("NorthwindConnection"), tags: new[] {"database"});
            services.AddHealthChecksUI().AddInMemoryStorage();
            return services;
        }
    }
}
