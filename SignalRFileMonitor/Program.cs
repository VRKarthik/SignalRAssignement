using FileMonitor.DataContracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace SignalRConsoleHost
{
	public class Program
	{
		#region Public methods

		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			using (var serviceScope = host.Services.CreateScope())
			{
				var services = serviceScope.ServiceProvider;

				try
				{
					var serviceContext = services.GetRequiredService<FileEventService>();
					serviceContext.StartWatcher();
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Exception occured, message: {ex.Message}, stacktrace: {ex.StackTrace}");
				}
			}

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
					.ConfigureWebHostDefaults(webBuilder =>
					{
						webBuilder.UseUrls("http://localhost:5003");
						webBuilder.UseStartup<Startup>();
					});

		#endregion
	}
}
