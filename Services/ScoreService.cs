using AuthenticationforTheMemeoryGame.Data;
using AuthenticationforTheMemeoryGame.DTOs.Scores;
using AuthenticationforTheMemeoryGame.DTOs.Shared;
using AuthenticationforTheMemeoryGame.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationforTheMemeoryGame.Services
{
    public class ScoreService
    {
        private readonly AppDbContext _dbcontext;
        public ScoreService(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<bool> SubmitScoreAsync(int userId,ScoreSubmitRequestDto request)
        {
            var score = new Score
            {
                UserId = userId,
                CompletionTimeSeconds = request.CompletionTimeSeconds,
                CompleteAt = DateTime.Now
            };
            _dbcontext.Scores.Add(score);
            var result = await _dbcontext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<PagedResult<LeaderboardResponseDto>> GetLeaderboardAsync(int page, int size)
        {
            
            var query = _dbcontext.Scores.AsQueryable();

            var totalCount = await query.CountAsync();

            var skipAmount = (page - 1) * size;

            var items = await query
                .Include(s => s.User)
                .OrderBy(s => s.CompletionTimeSeconds) 
                .ThenBy(s => s.CompleteAt)             
                .Skip(skipAmount)                      
                .Take(size)                           
                .Select(s => new LeaderboardResponseDto
                {
                    Username = s.User.Username,
                    CompleteTimeSeconds = s.CompletionTimeSeconds,
                    CompleteAt = s.CompleteAt
                })
                .ToListAsync();

  
            return new PagedResult<LeaderboardResponseDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = size
            };
        }
        public async Task<List<LeaderboardResponseDto>> GetLeaderboardAsync(int topN)
        {
            var leaderboard = await _dbcontext.Scores
                .Include(s => s.User)
                .OrderBy(s => s.CompletionTimeSeconds)
                .ThenBy(s => s.CompleteAt)
                .Take(topN)
                .Select(s => new LeaderboardResponseDto
                {
                    Username = s.User.Username,
                    CompleteTimeSeconds = s.CompletionTimeSeconds,
                    CompleteAt = s.CompleteAt
                })
                .ToListAsync();

            return leaderboard;
        }
    }
}
