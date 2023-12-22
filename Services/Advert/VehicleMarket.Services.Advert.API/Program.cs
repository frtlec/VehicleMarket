using VehicleMarket.Services.Advert.Infrastructure.ORM.Dapper;
using System.Data.SqlClient;
using System.Data;
using Npgsql;
using VehicleMarket.Services.Advert.Domain.SeedWork.Repository;
using VehicleMarket.Services.Advert.Application.Handlers;
using MediatR;
using System.Reflection;
using VehicleMarket.Services.Advert.Application.Helpers.Mappers.AutoMapper;
using MassTransit;
using VehicleMarket.Services.Advert.Application.Consumers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IDbConnection>(_=>new NpgsqlConnection(builder.Configuration.GetConnectionString("AdvertPostgreDB")));
builder.Services.AddTransient<IAdvertRepository,AdvertRepository>();
builder.Services.AddTransient<IAdvertVisitsRepository, AdvertVisitsRepository>();
builder.Services.AddMassTransitHostedService();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<AdvertVisitEventConsumer>();
    // Default Port : 5672
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
        cfg.ReceiveEndpoint("advert-visit-event", e =>
        {
            e.ConfigureConsumer<AdvertVisitEventConsumer>(context);
        });
    });
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllAdvertsByFilterQueryHandler).Assembly));
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
