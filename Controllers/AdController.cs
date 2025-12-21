using AuthenticationforTheMemeoryGame.DTOs.Auth;
using AuthenticationforTheMemeoryGame.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AuthenticationforTheMemeoryGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdController : ControllerBase
    {
        private readonly IAdService _adService;

        public AdController(IAdService adService)
        {
            _adService = adService;
        }

        //Api to get active ads
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveAds()
        {
            var ads = await _adService.GetActiveAdsAsync();
            return Ok(ads);
        }
    }
}
