using Basket.API.Middlewares;
using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Common.Logging;
using EventBus.Messages.IntegrationEvents.Interfaces;
using MassTransit;
using Serilog;
using Shared.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog(Serilogger.Configure);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();
//redis
var redisConnectionString = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnectionString;
});

//rabbitmq
var evenBusSetting = builder.Configuration.GetSection(nameof(EventBusSettings)).Get<EventBusSettings>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(evenBusSetting?.HostAddress);
    });

    x.AddRequestClient<IBacketCheckoutEvent>();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IBasketRepository, BasketRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
