using AuthServiceInfrastructure.IdentityServer;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AuthServiceInfrastructure
{
    public class AuthPersistedDbContextDesignTime : IDesignTimeDbContextFactory<AuthPersistedDbContext>
    {
        public AuthPersistedDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
            builder.UseNpgsql("Host=localhost;Database=auth;Username=auther;Password=P@ssw0rd");
            var storeOptionBuilder = new OperationalStoreOptions();

            return new AuthPersistedDbContext(builder.Options, storeOptionBuilder);
        }
    }
}
