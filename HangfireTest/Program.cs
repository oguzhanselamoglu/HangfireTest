using Hangfire;
using Hangfire.PostgreSql;
using HangfireBasicAuthenticationFilter;
using HangfireTest;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(x => x.UsePostgreSqlStorage("Server=localhost;Port=5432;Database=hangfiretest;User Id=veboni;Password=Xidok4096H!;"));
builder.Services.AddHangfireServer();


builder.Services.AddSingleton<IWeatherReport, WeatherReport>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHangfireDashboard();

app.UseHangfireDashboard("/job", new DashboardOptions
{
    Authorization = new[]
    {
        new HangfireCustomBasicAuthenticationFilter
        {
         User = builder.Configuration.GetSection("HangfireSettings:UserName").Value,
         Pass = builder.Configuration.GetSection("HangfireSettings:Password").Value
    }
    }
});





GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 3 });

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
  
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

//RecurringJobs.GetHourlyWeatherReport();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

