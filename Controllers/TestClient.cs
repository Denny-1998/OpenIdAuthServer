using Microsoft.AspNetCore.Mvc;

namespace OpenIdAuthServer.Controllers
{
    public class TestClient : Controller
    {
        [HttpGet("callback")]
        public async Task<IActionResult> Callback()
        {
            return Ok();
        }
    }
}
