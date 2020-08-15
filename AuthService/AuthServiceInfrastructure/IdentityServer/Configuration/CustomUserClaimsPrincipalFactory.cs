using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthServiceDomain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthServiceInfrastructure.IdentityServer.Configuration
{
    public class CustomUserClaimsPrincipalFactory : IUserClaimsPrincipalFactory<UserModel>
    {
        public async Task<ClaimsPrincipal> CreateAsync(UserModel user)
        {
            
            return await Task.Run(() => { 
                ClaimsPrincipal principal = new ClaimsPrincipal();
            ((ClaimsIdentity)principal.Identities).AddClaims(new []
            {
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            });
            return principal;
            });
            
        }
    }
}
