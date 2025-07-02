using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Customer.API.Data;
using Customer.API.Services.Interfaces;
using Customer.API.Services;

namespace Customer.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.ConfigureProductContext(configuration);

            services.AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsync<,,>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            services.AddTransient<ICustomerService, CustomerService>();

            return services;
        }

        private static IServiceCollection ConfigureProductContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<CustomerContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            return services;
        }
    }
}
