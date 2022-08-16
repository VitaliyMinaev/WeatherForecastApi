using Microsoft.EntityFrameworkCore;
using WeatherForecastApi;
using WeatherForecastApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var server = builder.Configuration["DbServer"] ?? "localhost";
var port = builder.Configuration["Port"] ?? "1443";
var user = builder.Configuration["Dbuser"] ?? "SA";
var password = builder.Configuration["DbPassword"] ?? "BilliJin2000";
var database = builder.Configuration["Database"] ?? "WeatherForecast";

builder.Services.AddDbContext<WeatherForecastContext>(options =>
    options.UseSqlServer($"Server={server}:{port}; Initial Catalog={database}; " +
    $"User ID={user}; Password={password}"));

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

PrepareDatabase.PreparePopulation(app, app.Logger);

app.Run();
