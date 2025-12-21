using AuthenticationforTheMemeoryGame.DTOs.Ads;
using AuthenticationforTheMemeoryGame.DTOs.Auth;

namespace AuthenticationforTheMemeoryGame.Interfaces
{
    public interface IAdService
    {
        Task<List<AdResponseDto>> GetActiveAdsAsync();
    }
}
