using AuthenticationforTheMemeoryGame.DTOs;
using AuthenticationforTheMemeoryGame.DTOs.Scores;
using AuthenticationforTheMemeoryGame.DTOs.Shared;
using AuthenticationforTheMemeoryGame.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationforTheMemeoryGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly IScoreService _scoreService;
        public ScoresController(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }
        [HttpPost("submit")]
        [Authorize]
        public async Task<IActionResult> SubmitScore([FromBody] ScoreSubmitRequestDto request)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");//User.FindFirst("UserId");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                // Return 401 wrapped in ApiResponse
                return Unauthorized(new ApiResponse<object>
                {
                    Code = 401,
                    Message = "Invalid user token.",
                    Data = null
                });
            }
            await _scoreService.SubmitScoreAsync(userId, request);

            return Ok(new ApiResponse<object>
            {
                Code = 200,
                Message = "Score submitted successfully.",
                Data = null
            });
        }
        // GET: /api/scores/leaderboard?page=1&size=10
        [HttpGet("leaderboard")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLeaderboard([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            
            if (page < 1) page = 1;
            if (size < 1) size = 10;
            if (size > 100) size = 100; 

            var result = await _scoreService.GetLeaderboardAsync(page, size);

            return Ok(new ApiResponse<PageResult<LeaderboardResponseDto>>
            {
                Code = 200,
                Message = "Leaderboard retrieved successfully.",
                Data = result
            });
        }
    }
}
