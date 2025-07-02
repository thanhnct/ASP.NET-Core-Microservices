using Common.Logging;
using Product.API.Data;
using Product.API.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting Product API up");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.AddAppConfigConfigurations();
    builder.Host.UseSerilog(Serilogger.Configure);
    ConfigureService();
    var app = builder.Build();
    Configure();
    app.Run();

    void ConfigureService()
    {
        builder.Services.AddInfrastructure(builder.Configuration);
    }

    void Configure()
    {
        app.UseInfrastucture(builder.Environment);

        app.MapControllers();

        app.MigrationDatabase<ProductContext>().Run();
    }
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("HostAbortedException", StringComparison.Ordinal))
    {
        throw;
    }
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.Information("Shutting down Product API");
    Log.CloseAndFlush();
}
