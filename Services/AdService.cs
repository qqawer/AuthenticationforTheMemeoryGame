using AuthenticationforTheMemeoryGame.Data;
using AuthenticationforTheMemeoryGame.DTOs.Ads;
using AuthenticationforTheMemeoryGame.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationforTheMemeoryGame.Services
{
    public class AdService : IAdService
    {
        private readonly AppDbContext _dbContext;
        public  AdService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<AdResponseDto>> GetActiveAdsAsync()
        {
            
            return await _dbContext.Ads
                .AsNoTracking() //improves performance for read-only queries
                .Where(ad => ad.IsActive) // filtering
                .Select(ad => new AdResponseDto //mapping
                {
                    Id = ad.Id,
                    AdTitle = ad.AdTitle,
                    AdImageUrl = ad.AdImageUrl,
                    IsActive = ad.IsActive
                })
                .ToListAsync(); //Async execution
        }
    }
}
