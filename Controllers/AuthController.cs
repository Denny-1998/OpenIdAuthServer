using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using OpenIdAuthServer.Helper;
using OpenIdAuthServer.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Azure.Core;
using OpenIdAuthServer.DTO;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using OpenIdAuthServer.Repository;

[Route("auth")]
public class AuthController : Controller
{
    IUserRepository _userRepository;
    IClientRepository _clientRepository;

    public AuthController(IUserRepository userRepository, IClientRepository clientRepository)
    {
        _userRepository = userRepository;
        _clientRepository = clientRepository;
    }

    [HttpGet("authorize")]
    public async Task<IActionResult> Authorize([FromQuery] Dictionary<string, string> parameters)
    {
        // Store the parameters in TempData to use after login
        TempData["authParameters"] = parameters;

        // Redirect to login page if the user is not authenticated
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login");
        }



        UrlBuilder urlBuilder = new UrlBuilder("/auth/scopes", parameters);
        return Redirect(urlBuilder.UrlWithParams);
    }



    




    [HttpGet("login")]
    public IActionResult Login([FromQuery] Dictionary<string, string> parameters)
    {
        return View();
    }




    [HttpPost("login")]
    public async Task<IActionResult> Login(string username, string password, [FromQuery]string returnUrl = "/auth/authorize")
    {

        LoggedInUser user = new LoggedInUser(username, password, _userRepository, _clientRepository);
        if (!user.checkUsername())
            return Unauthorized("wrong username or password.");

        if (!user.checkPassword())
            return Unauthorized("wrong username or password.");




        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            RedirectUri = returnUrl
        };
        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties).Wait();

        UrlBuilder urlBuilder = new UrlBuilder(returnUrl, TempData["authParameters"] as Dictionary<string, string>);

        return Redirect(urlBuilder.UrlWithParams);




        //return RedirectToAction("authorize");
    }

    [HttpPost("newUser")]
    public async Task<IActionResult> NewUser(string email, string password)
    {
        User user = _userRepository.CreateNewUser(email, password);

        if (user == null)
            return BadRequest();
        

        return Ok();
    }

    [HttpGet("scopes")]
    [Authorize]
    public async Task<IActionResult> Scopes([FromQuery] string client_id, [FromQuery] string redirect_uri, [FromQuery] string response_type, [FromQuery] string scope, [FromQuery] string state)
    {
        if (scope == null)
            return BadRequest();

        var scopes = scope.Split(' ');
        ViewData["Scopes"] = scopes;
        ViewData["ClientId"] = client_id;
        ViewData["RedirectUri"] = redirect_uri;
        ViewData["ResponseType"] = response_type;
        ViewData["State"] = state;
        return View();
    }



    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("login");

    }


    [HttpPost("Consent")]
    public async Task<IActionResult> Consent(string client_id, string redirect_uri, string response_type, string scope, string state)
    {
        Client client = _clientRepository.GetClient(client_id);


        string redirectUrl = $"{redirect_uri}?code=authorization_code&state={state}";

        return Redirect(redirectUrl);
    }


}
