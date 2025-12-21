using AuthenticationforTheMemeoryGame.DTOs;
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
            
            var result = await _authService.LoginAsync(loginRequest);
            return Ok(new ApiResponse<LoginResponseDto>
            {
                Code= 200,
                Message ="Login successful.",
                Data=result
            });
           
        }
    }
}
