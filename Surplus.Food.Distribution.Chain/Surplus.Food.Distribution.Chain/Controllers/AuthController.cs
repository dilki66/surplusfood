using Microsoft.AspNetCore.Mvc;
using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Models.ResponseModels;
using Surplus.Food.Distribution.Chain.Services.Interface;

namespace Surplus.Food.Distribution.Chain.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authenticationService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponse>> RegisterAsync([FromBody] RegisterRequest request)
        {
            var response = await authenticationService.RegisterAsync(request);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> LoginAsync([FromBody] AuthenticationRequest request)
        {
            var response = await authenticationService.LoginAsync(request);

            return Ok(response);
        }
    }
}
