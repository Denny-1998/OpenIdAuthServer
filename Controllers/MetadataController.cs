using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route(".well-known")]
public class MetadataController : Controller
{
    [HttpGet("openid-configuration")]
    public async Task<IActionResult> OpenIdConfiguration()
    {
        // Return discovery document
        var config = new
        {
            issuer = "https://your-auth-server.com",
            authorization_endpoint = "https://your-auth-server.com/authorize",
            token_endpoint = "https://your-auth-server.com/token",
            userinfo_endpoint = "https://your-auth-server.com/userinfo",
            jwks_uri = "https://your-auth-server.com/jwks",
            response_types_supported = new[] { "code", "token", "id_token", "code id_token", "code token", "token id_token", "code token id_token" },
            subject_types_supported = new[] { "public", "pairwise" },
            id_token_signing_alg_values_supported = new[] { "RS256" },
            scopes_supported = new[] { "openid", "email" },
            token_endpoint_auth_methods_supported = new[] { "client_secret_basic", "client_secret_post" },
            claims_supported = new[] { "sub", "name", "preferred_username", "given_name", "family_name", "email", "picture" }
        };
        return Ok(config);
    }

    [HttpGet("jwks")]
    public async Task<IActionResult> JWKS()
    {
        // Return JWKS document
        var jwks = new { /* your JWKS data */ };
        return Ok(jwks);
    }
}
