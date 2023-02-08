using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Group.Ecommerce.Application.Interface;
using Group.Ecommerce.Application.Main;
using Group.Ecommerce.Domain.Core;
using Group.Ecommerce.Domain.Interface;
using Group.Ecommerce.Infraestructure.Data;
using Group.Ecommerce.Infraestructure.Interface;
using Group.Ecommerce.Infraestructure.Repository;
using Group.Ecommerce.Transversal.Common;
using Group.Ecommerce.Transversal.Logging;

namespace Group.Ecommerce.Services.WebApi.Modules.Injection
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            //services.AddSingleton<DapperContext>();
            services.AddScoped<ICustomersApplication, CustomersApplication>();
            services.AddScoped<ICustomersDomain, CustomersDomain>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<IUsersDomain, UsersDomain>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
