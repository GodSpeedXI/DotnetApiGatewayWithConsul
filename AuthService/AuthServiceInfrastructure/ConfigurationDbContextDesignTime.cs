using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AuthServiceInfrastructure
{
    /// <summary>
    /// Generate Migration with this command
    /// dotnet ef migrations add InitConfigurationDbContext -c ConfigurationDbContext -o IdentityServer/Migrations/Configuration
    /// </summary>
    public class ConfigurationDbContextDesignTime : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            builder.UseNpgsql("Host=localhost;Database=is4s;Username=is4s;Password=P@ssw0rd", sql =>
            {
                sql.MigrationsAssembly("AuthServiceInfrastructure");
            });
            var storeOptionBuilder = new ConfigurationStoreOptions();

            return new ConfigurationDbContext(builder.Options, storeOptionBuilder);
        }
    }
}
