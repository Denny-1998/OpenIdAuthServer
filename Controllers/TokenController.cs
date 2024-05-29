using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenIdAuthServer.DTO;
using OpenIdAuthServer.Helper;
using OpenIdAuthServer.Models;
using OpenIdAuthServer.Repository;

namespace OpenIdAuthServer.Controllers
{
    public class TokenController : Controller
    {


        IUserRepository _userRepository;
        IClientRepository _clientRepository;
        ITokenRepository _tokenRepository;

        public TokenController(IUserRepository userRepository, IClientRepository clientRepository, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _clientRepository = clientRepository;
            _tokenRepository = tokenRepository;
        }


        [HttpPost("token")]
        public async Task<IActionResult> Token([FromForm] TokenRequestDTO request)
        {
            Client client = _clientRepository.GetClient(request.ClientId);

            
            HashHelper hs = new HashHelper();
            
            //TODO
            //read client from DB
            //hash client secret
            
            //string requestSecretHash = hs.ComputeHash_SHA512(request.ClientSecret);

            if (client.AllowedGrantTypes.Contains(request.GrantType))
            {
                if (request.ClientSecret != client.ClientSecret)
                {
                    return Unauthorized(new { error = "invalid_client" });
                }

                var token = GenerateToken(request, new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, request.ClientId),
                });

                return Ok(new TokenResponseDTO
                {
                    AccessToken = token,
                    TokenType = "Bearer",
                    ExpiresIn = 3600
                });

                
            }
            return BadRequest();
        }

        private string GenerateToken(TokenRequestDTO tokenRequest, List<Claim> claims)
        {
            ConfigReader cr = new ConfigReader("./config.json"); 



            //TODO save token somewhere else
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cr.GetJwtKey()));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime expires = System.DateTime.Now.AddHours(2);

            var token = new JwtSecurityToken(
                issuer: cr.GetJwtIssuer(),
                audience: cr.GetJwtAudience(),
                claims: claims,
                expires: expires,
                signingCredentials: credentials);



            Token dbToken = new Token
            {
                Token1 = token.ToString(),
                ClientId = tokenRequest.ClientId,
                TokenType = "Baerer",
                Scope = "oidc",
                ExpiresAt = expires,
                CreatedAt = System.DateTime.Now

            };

            _tokenRepository.AddToken(dbToken);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       

        [HttpPost("newClient")]
        public async Task<IActionResult> NewClient([FromForm] string clientName, [FromForm] string redirectUri, [FromForm] string allowedGrantTypes, [FromForm] string allowedScopes)
        {
            ClientDTO client = _clientRepository.CreateNewClient(clientName, redirectUri, allowedGrantTypes, allowedScopes);
            
            return Ok(client);
        }



    }
}
