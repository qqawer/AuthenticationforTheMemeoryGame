using AuthenticationforTheMemeoryGame.Data;
using AuthenticationforTheMemeoryGame.DTOs.Scores;
using AuthenticationforTheMemeoryGame.DTOs.Shared;
using AuthenticationforTheMemeoryGame.Interfaces;
using AuthenticationforTheMemeoryGame.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationforTheMemeoryGame.Services
{
    public class ScoreService : IScoreService
    {
        private readonly AppDbContext _dbcontext;
        public ScoreService(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task SubmitScoreAsync(int userId, ScoreSubmitRequestDto request)
        {
            var score = new Score
            {
                UserId = userId,
                CompletionTimeSeconds = request.CompletionTimeSeconds,
                CompleteAt = DateTime.Now
            };
            _dbcontext.Scores.Add(score);
            await _dbcontext.SaveChangesAsync();

        }

        public async Task<PageResult<LeaderboardResponseDto>> GetLeaderboardAsync(int page, int size)
        {
            //Groupby UserId
            var query = _dbcontext.Scores
            .GroupBy(s => new { s.UserId, s.User.Username })
            .Select(g => new LeaderboardResponseDto
            {
                  Username = g.Key.Username,
                // get best completion time per user
                    CompleteTimeSeconds = g.Min(s => s.CompletionTimeSeconds),
              // use the earliest completion time for tie-breaking
                  CompleteAt = g.Min(s => s.CompleteAt)
            });

            var totalCount = await query.CountAsync();

            var skipAmount = (page - 1) * size;

            var items = await query
                .OrderBy(s => s.CompleteTimeSeconds)
                .ThenBy(s => s.CompleteAt)
                .Skip(skipAmount)
                .Take(size)
                .ToListAsync();


            return new PageResult<LeaderboardResponseDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = size
            };
        }
    }
}
