using AuthenticationforTheMemeoryGame.DTOs.Auth;
using AuthenticationforTheMemeoryGame.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationforTheMemeoryGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //login api
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var response = await _authService.LoginAsync(loginRequest);
            if (response.IsSuccess)
            {
               return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
