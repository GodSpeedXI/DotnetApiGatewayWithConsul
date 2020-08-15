using System.Reflection;
using AuthServiceDomain.Entities;
using AuthServiceInfrastructure.IdentityServer.Configuration;
using AuthServiceInfrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
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
            #region Register AuthServiceIdentityDbContext

            //Register AuthServiceIdentityDbContext
            services.AddDbContext<AuthServiceIdentityDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            #endregion

            #region Build Identity

            //Build Identity
            services.AddIdentity<UserModel, RolesModel>(opt =>
                {
                    opt.Password.RequireDigit = true;
                    opt.Password.RequiredLength = 6;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<AuthServiceIdentityDbContext>()
                .AddDefaultTokenProviders();
                
            
            //idenBuilder = new IdentityBuilder(idenBuilder.UserType, typeof(RolesModel), idenBuilder.Services);
            //idenBuilder.AddEntityFrameworkStores<AuthServiceIdentityDbContext>();
            //idenBuilder.AddRoleValidator<RoleValidator<RolesModel>>();
            //idenBuilder.AddRoleManager<RoleManager<RolesModel>>();
            //idenBuilder.AddSignInManager<SignInManager<UserModel>>();
            //idenBuilder.AddDefaultTokenProviders();

            #endregion

            #region IdentityServer Init

            var migrationAss = Assembly.GetExecutingAssembly();
            var builder = services.AddIdentityServer(opt =>
                {
                    opt.Events.RaiseErrorEvents = true;
                    opt.Events.RaiseInformationEvents = true;
                    opt.Events.RaiseFailureEvents = true;
                    opt.Events.RaiseSuccessEvents = true;

                    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                    opt.EmitStaticAudienceClaim = true;
                })
                .AddConfigurationStore(options =>
                {
                    // this adds the config data from DB (clients, resources)
                    options.ConfigureDbContext = builder =>
                        builder.UseNpgsql(configuration.GetConnectionString("Is4s"),
                            sql => sql.MigrationsAssembly(migrationAss.FullName));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseNpgsql(configuration.GetConnectionString("Is4s"),
                            sql => sql.MigrationsAssembly(migrationAss.FullName));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 604800;
                })
                .AddAspNetIdentity<UserModel>();

            builder.AddDeveloperSigningCredential();

            #endregion

            return services;
        }
    }
}
