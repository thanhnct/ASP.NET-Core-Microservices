using Microsoft.EntityFrameworkCore;

namespace Customer.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrationDatabase<TContext>(this IHost host) where TContext : DbContext
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<TContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
            return host;
        }
    }
}
