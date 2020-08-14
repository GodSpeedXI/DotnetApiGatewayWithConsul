using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceInfrastructure.IdentityServer
{
    public class AuthPersistedDbContext : PersistedGrantDbContext
    {
        public AuthPersistedDbContext(DbContextOptions<PersistedGrantDbContext> options, OperationalStoreOptions storeOptions) 
            : base(options, storeOptions) { }
    }
}
