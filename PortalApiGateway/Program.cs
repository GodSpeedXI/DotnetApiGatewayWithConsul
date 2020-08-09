using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace PortalApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://*:7000");
                    webBuilder.UseKestrel();
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.ConfigureAppConfiguration((builder, config) =>
                    {
                        config.SetBasePath(builder.HostingEnvironment.ContentRootPath)
                            .AddJsonFile("appsettings.json", true, true)
                            .AddJsonFile($"appsettings.{builder.HostingEnvironment.EnvironmentName}.json", true,true)
                            .AddJsonFile("ocelot.json",false,true)
                            .AddEnvironmentVariables();
                    });
                    webBuilder.UseStartup<Startup>();
                });
        }
        
    }
}
