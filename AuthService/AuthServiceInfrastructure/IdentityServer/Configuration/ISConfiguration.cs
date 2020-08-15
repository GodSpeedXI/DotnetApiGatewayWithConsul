using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthServiceInfrastructure.IdentityServer.Configuration
{
    public static class Configuration
    {
        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource>()
            {
                new ApiResource("ProductService")
            };
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>()
            {
                
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>()
            {
                new Client()
                {
                    ClientId = "client_id",
                    ClientSecrets = {new Secret("client_secret".ToSha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "ProductService" }
                }
            };

    }
}
