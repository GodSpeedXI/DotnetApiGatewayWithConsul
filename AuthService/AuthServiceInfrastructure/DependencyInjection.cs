using System.Reflection;
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

            //services.AddDbContext<AuthServiceIdentityDbContext>(opt =>
            //{
            //    opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            //});
            //IdentityBuilder idenBuilder = services.AddIdentityCore<UserModel>(opt =>
            //{
            //    opt.Password.RequireDigit = true;
            //    opt.Password.RequiredLength = 6;
            //    opt.Password.RequireUppercase = false;
            //    opt.Password.RequireNonAlphanumeric = false;
            //});
            //idenBuilder = new IdentityBuilder(idenBuilder.UserType, typeof(RolesModel), idenBuilder.Services);
            //idenBuilder.AddEntityFrameworkStores<AuthServiceIdentityDbContext>();
            //idenBuilder.AddRoleValidator<RoleValidator<RolesModel>>();
            //idenBuilder.AddRoleManager<RoleManager<RolesModel>>();
            //idenBuilder.AddSignInManager<SignInManager<UserModel>>();

            var migrationsAssembly = Assembly.GetExecutingAssembly();
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options =>
                {
                    // this adds the config data from DB (clients, resources)
                    options.ConfigureDbContext = builder =>
                        builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), 
                            sql => sql.MigrationsAssembly(migrationsAssembly.FullName));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                            sql => sql.MigrationsAssembly(migrationsAssembly.FullName));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                });

            return services;
        }
    }
}
