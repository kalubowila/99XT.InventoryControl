using _99XT.InventoryControl.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace _99XT.InventoryControl.App.Extensions
{
    public static class ConfigureAuthentication
    {
        public static void AddAuthenticationWithIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = "Cookies";
                opt.DefaultChallengeScheme = "oidc";
            }).AddCookie("Cookies")
            .AddOpenIdConnect("oidc", opt =>
            {
                opt.SignInScheme = "Cookies";
                opt.Authority = configuration["OpenIdConnect:Authority"];
                opt.ClientId = configuration["OpenIdConnect:ClientId"];
                opt.ResponseType = configuration["OpenIdConnect:ResponseType"];
                opt.SaveTokens = true;
                opt.ClientSecret = StringCipher.Decrypt(configuration["OpenIdConnect:ClientSecret"]);
                opt.GetClaimsFromUserInfoEndpoint = true;

                opt.ClaimActions.DeleteClaims(new string[] { "sid", "idp" });
                opt.Scope.Add("email");
                opt.Scope.Add("roles");
                opt.ClaimActions.MapUniqueJsonKey("role", "role");

                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    RoleClaimType = "role"
                };
            });
        }
    }
}
