using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AuthServiceInfrastructure
{
    /// <summary>
    /// Generate Migration with this command
    /// dotnet ef migrations add InitPersistedGrantDbContext -c PersistedGrantDbContext -o IdentityServer/Migrations/PersistedGrant
    /// </summary>
    public class PersistedGrantDbContextDesignTime : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
            builder.UseNpgsql("Host=localhost;Database=is4s;Username=is4s_user;Password=P@ssw0rd", sql =>
            {
                sql.MigrationsAssembly("AuthServiceInfrastructure");
            });
            var storeOptionBuilder = new OperationalStoreOptions();

            return new PersistedGrantDbContext(builder.Options, storeOptionBuilder);
        }
    }
}
