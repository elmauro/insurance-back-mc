using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace insurance_back_mc
{
    public static class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((hostingContext, config) =>
				{
					var env = hostingContext.HostingEnvironment;
					config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
					config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false);
				})
				.ConfigureLogging((_, builder) =>
				{
					builder.AddFile("logs/app-{Date}.json", isJson: true);
				})
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
