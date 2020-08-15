using AuthServiceInfrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.Extensions.Logging;

namespace AuthService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            #region Migration Db

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var config = services.GetRequiredService<IConfiguration>();
                    if (bool.Parse(config.GetSection("Postgres:AutoMigrateDB").Value))
                    {
                        var AuthContext = services.GetRequiredService<AuthServiceIdentityDbContext>();
                        var persisedGrantContext = services.GetRequiredService<PersistedGrantDbContext>();
                        var configurationContext = services.GetRequiredService<ConfigurationDbContext>();
                        persisedGrantContext.Database.MigrateAsync().Wait();
                        configurationContext.Database.MigrateAsync().Wait();
                        AuthContext.Database.MigrateAsync().Wait();
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }

            #endregion

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("hosting.json", optional: true)
                        //.AddJsonFile("appsettings.json", optional: true)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
