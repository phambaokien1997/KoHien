using IdentityServer4.Models;
using System.Collections.Generic;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("api1", "My API")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "SpaClient",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = true,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                RedirectUris = { "https://localhost:5001/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },
                AllowedScopes = { "openid", "profile", "api1" },
                RequirePkce = true
            },
            new Client
            {
                ClientId = "mvc_client",
                ClientName = "MVC Client",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code, // Use the Authorization Code flow

                // URLs to redirect to after login/logout
                RedirectUris = { "https://localhost:5002/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                AllowedScopes = 
                {
                    IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                    "api1" // Add any API scopes the client needs
                },

                RequireConsent = false, // Optional: disables the consent screen
                RequirePkce = true, // Enforces PKCE for security

                // Enable offline access if you need refresh tokens
                AllowOfflineAccess = true
            }
        };
}
