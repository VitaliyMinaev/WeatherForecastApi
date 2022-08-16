using Microsoft.EntityFrameworkCore;
using WeatherForecastApi.Data;
using WeatherForecastApi.Models;

namespace WeatherForecastApi
{
    public class PrepareDatabase
    {
        public static void PreparePopulation(IApplicationBuilder applicationBuilder, ILogger logger)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<WeatherForecastContext>();

                SeedData(context, logger);
            }
        }
        public static void SeedData(WeatherForecastContext context, ILogger logger)
        {
            logger.LogInformation("Applying migration ...");

            context.Database.Migrate();

            if(context.WeatherForecasts.Any() == false)
            {
                logger.LogInformation("Adding data");

                string[] Summaries = new[]
                {
                    "Freezing", "Bracing", "Chilly", "Cool", "Mild",
                    "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
                };

                context.WeatherForecasts.AddRange(
                    Enumerable.Range(1, 5).Select(index => new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    }));
            }
            else
            {
                logger.LogInformation("Already have data - not seeding");
            }
        }
    }
}
