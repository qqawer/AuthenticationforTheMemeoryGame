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
                throw new ArgumentException("Username and Password cannot be empty");
            }
            //query user from database and validate credentials here
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == loginRequest.Username && u.Password == loginRequest.Password);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }
            var token=_tokenService.GenerateToken(user);
            return new LoginResponseDto
            {
                Token = token
            };
        }
    }
}
