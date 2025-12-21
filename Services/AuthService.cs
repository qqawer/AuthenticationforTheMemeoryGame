using AuthenticationforTheMemeoryGame.Data;
using AuthenticationforTheMemeoryGame.DTOs.Auth;
using AuthenticationforTheMemeoryGame.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationforTheMemeoryGame.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public AuthService(AppDbContext dbContext,ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;

        }
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest)
        {

            if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    Message = "Username and Password cannot be empty"
                };
            }
            //query user from database and validate credentials here
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == loginRequest.Username && u.Password == loginRequest.Password);
            if (user == null)
            {
                return new LoginResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid username or password"
                };
            }
            var token=_tokenService.GenerateToken(user);
            return new LoginResponseDto
            {
                IsSuccess = true,
                Message = "Login successful",
                Token = token
            };
        }
    }
}
