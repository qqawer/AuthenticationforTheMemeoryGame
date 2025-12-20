using AuthenticationforTheMemeoryGame.Data;
using AuthenticationforTheMemeoryGame.DTOs.Auth;
using AuthenticationforTheMemeoryGame.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationforTheMemeoryGame.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        public AuthService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
            return new LoginResponseDto
            {
                IsSuccess = true,
                Message = "Login successful",
                UserId = user.Id,
                IsPaid = user.IsPaid
            };
        }
    }
}
