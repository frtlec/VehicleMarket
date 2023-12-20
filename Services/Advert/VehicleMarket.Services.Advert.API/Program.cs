using VehicleMarket.Services.Advert.Infrastructure.ORM.Dapper;
using VehicleMarket.Services.Advert.Application.Helpers.Extensions;
using System.Data.SqlClient;
using System.Data;
using Npgsql;
using VehicleMarket.Services.Advert.Domain.SeedWork.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
IDbConnection dbConnection = new NpgsqlConnection("User ID=admin; Password=Password12*; Server=localhost; Port=5432;Database=advertdb;Pooling=true");

// IDbConnection baðýmlýlýðýný servis olarak kaydedin
builder.Services.AddSingleton<IDbConnection>(dbConnection);
builder.Services.AddSingleton<IAdvertRelationDatabaseBuilder,AdvertRelationDatabaseBuilder>();
builder.Services.AddSingleton<IAdvertRepository,AdvertRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Services.BuildPosgtreDB();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
