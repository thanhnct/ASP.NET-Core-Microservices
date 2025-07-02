using Microsoft.EntityFrameworkCore;
using Customer.API.Data;
using Customer.API.Middlewares;

namespace Customer.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseInfrastucture(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment() || env.IsStaging())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}    

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<ExceptionMiddleware>();

            //app.UseHttpsRedirection();
            app.UseAuthorization();

            try
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<CustomerContext>();
                    dbContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                var logger = app.ApplicationServices.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
    }
}
