using AuthenticationforTheMemeoryGame.Data;
using AuthenticationforTheMemeoryGame.DTOs.Ads;
using AuthenticationforTheMemeoryGame.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationforTheMemeoryGame.Services
{
    public class AdService : IAdService
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        public  AdService(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<AdResponseDto>> GetActiveAdsAsync()
        {
            // Get the domain name of the current request (e.g. https://localhost:5011)
            var request = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{request?.Scheme}://{request?.Host}";

            // Retrieve raw data from the database first
            // Note: Do not directly concatenate the URL in Select here, because EF Core cannot translate baseUrl into SQL
            var ads = await _dbContext.Ads
                .AsNoTracking()
                .Where(ad => ad.IsActive)
                .ToListAsync();

            // Transform data and concatenate full URL in memory
            return ads.Select(ad => new AdResponseDto
            {
                Id = ad.Id,
                AdTitle = ad.AdTitle,
                // Concatenation logic: https://localhost:5011 + /images/ads/ad1.png
                AdImageUrl = $"{baseUrl}{ad.AdImageUrl}",
                IsActive = ad.IsActive
            }).ToList();
        }
    }
}
