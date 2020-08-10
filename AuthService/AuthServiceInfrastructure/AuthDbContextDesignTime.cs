using AuthServiceInfrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AuthServiceInfrastructure
{
    /// <summary>
    /// This Class is only use when generating migration
    /// </summary>
    public class AuthDbContextDesignTime : IDesignTimeDbContextFactory<AuthServiceIdentityDbContext>
    {
        public AuthServiceIdentityDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AuthServiceIdentityDbContext>();
            builder.UseNpgsql("Host=localhost;Database=auth;Username=auther;Password=P@ssw0rd");

            return new AuthServiceIdentityDbContext(builder.Options);
        }
    }
}
