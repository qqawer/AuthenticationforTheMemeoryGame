using AuthenticationforTheMemeoryGame.DTOs.Auth;

namespace AuthenticationforTheMemeoryGame.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest); 
    }
}
