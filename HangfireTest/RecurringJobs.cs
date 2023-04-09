using System;
using Hangfire;

namespace HangfireTest
{
	public static class RecurringJobs
	{
		public static void GetHourlyWeatherReport()
		{
			WeatherReport weather = new();
			RecurringJob.RemoveIfExists(nameof(weather.ReportWeather));
			RecurringJob.RemoveIfExists(nameof(weather.ReportWeather2));

			RecurringJob.AddOrUpdate<IWeatherReport>(nameof(weather.ReportWeather), x => x.ReportWeather(), Cron.Minutely(), TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));


        }
	}
}

