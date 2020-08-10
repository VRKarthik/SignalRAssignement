using FileMonitor.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace SignalRConsoleHost
{
	public class Startup
	{
		#region Public methods

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:5003")
							 .AllowAnyHeader()
							 .AllowAnyMethod()
							 .AllowCredentials()
							 .SetIsOriginAllowed((host) => true));
			});

			services.AddSingleton<IFileMonitorService>(fileMonitor => new FileMonitorService(@"C:\Users\rajendra-2\Desktop\ServerDirectory"));
			services.AddSingleton<FileEventService>();

			services.AddSignalR();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseCors("CorsPolicy");
			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<FileMonitorHub>("/filemonitorhub");
			});
		}

		#endregion
	}
}
