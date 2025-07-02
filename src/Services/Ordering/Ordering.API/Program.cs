using Common.Logging;
using Contracts.Services;
using Infrastructure.Configurations;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;
using Serilog;
using Shared.Services.Email;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog(Serilogger.Configure);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddTransient<IEmailService<MailRequest>, SmtpEmailService>();

var smtpEmailSetting = builder.Configuration.GetSection("SMTPEmailSetting").Get<SMTPEmailSetting>();
if (smtpEmailSetting != null)
{
    builder.Services.AddSingleton(smtpEmailSetting);
}
else
{
    throw new InvalidOperationException("SMTPEmailSetting configuration is missing or invalid.");
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<OrderContext>();
        dbContext.Database.Migrate();
    }
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while migrating the database.");
}


app.UseAuthorization();

app.MapControllers();

app.Run();
