using AuthenticationApi.Models;
using AuthenticationApi.Services;
using JwtAuthenticationManager;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly JwtTokenHandler _jwtTokenHandler;
        private readonly IAuthenticationService _authService;

        public AccountController(JwtTokenHandler jwtTokenHandler,
            IAuthenticationService authService)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (await _authService.RegisterUser(model))
            {
                return Ok(true);
            }
            return BadRequest(false);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthenticationResponse?>> Login(LoginModel model)
        {
            if (await _authService.Login(model))
            {
                if(model.Id is null)
                 return Unauthorized();

                var authenticationResponse = _jwtTokenHandler.GenerateJwtToken(model.Email,model.Id);
                if (authenticationResponse == null)
                {
                    return Unauthorized();
                }

                return authenticationResponse;
            }

            return Unauthorized();

        }

    }
}
