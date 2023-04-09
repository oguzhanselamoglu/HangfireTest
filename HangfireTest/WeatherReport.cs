using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace HangfireTest
{
    public class WeatherReport : IWeatherReport
    {
        string[] summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public void ReportWeather()
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                             new WeatherForecast
                             (
                                 DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                                 Random.Shared.Next(-20, 55),
                                 summaries[Random.Shared.Next(summaries.Length)]
                             ))
                             .ToArray();

            foreach (var item in forecast)
            {
                Debug.Write(item.Date + " | ");
                Debug.Write(item.TemperatureC + " | ");
              
                
                Debug.WriteLine(item.Summary);
                Debug.WriteLine("".PadRight(40, '*'));
            }
        }

        public void ReportWeather2()
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                            new WeatherForecast
                            (
                                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                                Random.Shared.Next(-20, 55),
                                summaries[Random.Shared.Next(summaries.Length)]
                            ))
                            .ToArray();


            foreach (var item in forecast)
            {
                Debug.Write(item.Date + " | ");
                Debug.Write(item.TemperatureC + " | ");


                Debug.WriteLine(item.Summary);
                Debug.WriteLine("".PadRight(40, '*'));
            }
        }
    }
}

