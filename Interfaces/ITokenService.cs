using AuthenticationforTheMemeoryGame.Models; 

namespace AuthenticationforTheMemeoryGame.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}