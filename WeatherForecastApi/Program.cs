using Microsoft.EntityFrameworkCore;
using WeatherForecastApi;
using WeatherForecastApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var server = builder.Configuration["DbServer"] ?? "ms-sql-server";
var port = builder.Configuration["Port"] ?? "1433";
var user = builder.Configuration["Dbuser"] ?? "SA";
var password = builder.Configuration["DbPassword"] ?? "BilliJin2000";
var database = builder.Configuration["Database"] ?? "WeatherForecast";

builder.Services.AddDbContext<WeatherForecastContext>(options =>
    options.UseSqlServer($"Data Source={server},{port};Persist Security Info=True;" +
    $"Initial Catalog={database};User ID={user};Password={password}"));

var app = builder.Build();

app.Logger.LogInformation("Start new project!!!\n\n\n\n");

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
