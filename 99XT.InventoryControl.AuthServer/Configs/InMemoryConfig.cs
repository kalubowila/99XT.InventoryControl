using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace _99XT.InventoryControl.AuthServer.Configs
{
    public static class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
          new List<IdentityResource>
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Email(),
              new IdentityResource("roles", "User role(s)", new List<string> { "role" })
          };

        public static List<TestUser> GetUsers() =>
          new List<TestUser>
          {
              new TestUser
              {
                  SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
                  Username = "sa",
                  Password = "sa@123",
                  Claims = new List<Claim>
                  {
                      new Claim("given_name", "Super"),
                      new Claim("family_name", "Admin"),
                      new Claim("email", "sa@gmail.com"),
                      new Claim("role", "Admin")
                  }
              },
              new TestUser
              {
                  SubjectId = "c95ddb8c-79ec-488a-a485-fe57a1462340",
                  Username = "dev",
                  Password = "dev@123",
                  Claims = new List<Claim>
                  {
                      new Claim("given_name", "Dev"),
                      new Claim("family_name", "User"),
                      new Claim("email", "dev@gmail.com"),
                      new Claim("role", "Manager")
                  }
              }
          };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
               new Client
               {
                   ClientName = "99XT Inventory Control App",
                   ClientId = "99xt-inventorycontrolapp",
                   ClientSecrets = new [] { new Secret("99xt@Inventory".Sha512()) },
                   AllowedGrantTypes = GrantTypes.Hybrid,
                   RedirectUris = new List<string>{ "https://localhost:44379/signin-oidc" },
                   PostLogoutRedirectUris = new List<string> { "https://localhost:44379/signout-callback-oidc" },
                   RequirePkce = false,
                   //RequireConsent = true,
                   AllowedScopes = 
                   {    IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile, 
                        IdentityServerConstants.StandardScopes.Email,
                        "roles"
                   }
                }
            };
    }
}
