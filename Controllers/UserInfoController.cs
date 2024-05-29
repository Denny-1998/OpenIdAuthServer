using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIdAuthServer.Models;
using OpenIdAuthServer.Repository;
using System.Security.Claims;
using System.Threading.Tasks;

[Route("userinfo")]
public class UserInfoController : Controller
{

    IUserRepository _userRepository;
    IClientRepository _clientRepository;
    ITokenRepository _tokenRepository;

    public UserInfoController(IUserRepository userRepository, IClientRepository clientRepository, ITokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _clientRepository = clientRepository;
        _tokenRepository = tokenRepository;
    }

   



    [HttpGet("UserInfo")]
    [Authorize]
    public IActionResult GetUserInfo(string accessToken)
    {
        


        Token token = _tokenRepository.GetToken(accessToken);
        if (token == null)
            return Unauthorized();

           

        User user = _userRepository.FindUserById(token.UserId);

        return Ok(new
        {
            ID = user.UserId,
            UserName = user.Username,
            UserEmail = user.Email
        });
    }
}
