using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OcelotAPIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHostBuilder builder = new WebHostBuilder();
            builder.ConfigureServices(s => { s.AddSingleton(builder); }).ConfigureAppConfiguration((host, config) =>
            {
                config.AddJsonFile(Path.Combine("appsettings.json"));
            });
            builder.UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>().ConfigureLogging((hostingContext, logging) =>
                {
                    //add your logging
                    logging.AddConsole();
                });

            var host = builder.Build();
            host.Run();

        }
    }
}
