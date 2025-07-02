using Microsoft.EntityFrameworkCore;
using Product.API.Data;
using Product.API.Middlewares;

namespace Product.API.Extensions
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
                    var dbContext = scope.ServiceProvider.GetRequiredService<ProductContext>();
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
