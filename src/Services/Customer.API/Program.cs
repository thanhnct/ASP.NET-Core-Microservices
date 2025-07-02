using Common.Logging;
using Customer.API.Data;
using Customer.API.Extensions;
using Customer.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using Shared.DTOs.Customer;

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

        app.MapGet("/api/customers", async ([FromQuery] int page, [FromServices] ICustomerService customerService) =>
        {
            if (page < 1)
            {
                page = 1;
            }
            var customers = await customerService.GetCustomers(page);
            return Results.Ok(customers);
        });

        app.MapGet("/api/customers/{id:int}", async ([FromRoute] int id, [FromServices] ICustomerService customerService) =>
        {
            if (id <= 0)
            {
                return Results.BadRequest("Invalid customer ID.");
            }

            var customer = await customerService.GetCustomerById(id);
            return customer == null ? Results.NotFound() : Results.Ok(customer);
        });

        app.MapPost("/api/customers", async ([FromBody] CustomerCreateDto dto, [FromServices] ICustomerService customerService) =>
        {
            if (dto == null)
            {
                return Results.BadRequest("Customer data is required.");
            }

            var id = await customerService.CreateCustomer(dto);
            return Results.Created($"/api/customers/{id}", id);
        });

        app.MapPut("/api/customers/{id:int}", async ([FromRoute] int id, [FromBody] CustomerUpdateDto dto, [FromServices] ICustomerService customerService) =>
        {
            if (dto == null)
            {
                return Results.BadRequest();
            }

            await customerService.UpdateCustomer(id, dto);
            return Results.NoContent();
        });

        app.MapDelete("/api/customers/{id:int}", async ([FromRoute] int id, [FromServices] ICustomerService customerService) =>
        {
            if (id <= 0)
            {
                return Results.BadRequest();
            }

            await customerService.DeleteCustomer(id);
            return Results.NoContent();
        });

        app.MapControllers();

        app.MigrationDatabase<CustomerContext>().Run();
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
