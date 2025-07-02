using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Product.API.Data;
using Product.API.Services;
using Product.API.Services.Interfaces;

namespace Product.API.Extensions
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
            services.AddTransient<IProductService, ProductService>();

            return services;
        }

        private static IServiceCollection ConfigureProductContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            var builder = new MySqlConnectionStringBuilder(connectionString);
            services.AddDbContext<ProductContext>(options =>
            {
                options.UseMySql(builder.ConnectionString, ServerVersion.AutoDetect(builder.ConnectionString), e =>
                {
                    e.MigrationsAssembly("Product.API");
                    e.SchemaBehavior(MySqlSchemaBehavior.Ignore);
                });
            });
            return services;
        }
    }
}
