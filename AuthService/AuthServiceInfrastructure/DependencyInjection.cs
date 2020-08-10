using AuthServiceDomain.Entities;
using AuthServiceInfrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthServiceInfrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AuthServiceIdentityDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            IdentityBuilder idenBuilder = services.AddIdentityCore<UserModel>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            });
            idenBuilder = new IdentityBuilder(idenBuilder.UserType, typeof(RolesModel), idenBuilder.Services);
            idenBuilder.AddEntityFrameworkStores<AuthServiceIdentityDbContext>();
            idenBuilder.AddRoleValidator<RoleValidator<RolesModel>>();
            idenBuilder.AddRoleManager<RoleManager<RolesModel>>();
            idenBuilder.AddSignInManager<SignInManager<UserModel>>();

            return services;
        }
    }
}
