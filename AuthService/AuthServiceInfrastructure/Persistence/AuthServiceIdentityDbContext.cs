using AuthServiceDomain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuthServiceInfrastructure.Persistence
{
    public class AuthServiceIdentityDbContext : IdentityDbContext<
        UserModel, RolesModel, int, IdentityUserClaim<int>,
        UserRoleModel, IdentityUserLogin<int>, IdentityRoleClaim<int>,
        IdentityUserToken<int>>
    {
        public AuthServiceIdentityDbContext(DbContextOptions<AuthServiceIdentityDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Find IEntityTypeConfiguration to config db table
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
