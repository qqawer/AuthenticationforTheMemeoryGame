
using AuthenticationforTheMemeoryGame.DTOs.Scores;
using AuthenticationforTheMemeoryGame.DTOs.Shared;

namespace AuthenticationforTheMemeoryGame.Interfaces
{
    public interface IScoreService
    {
        Task<bool> SubmitScoreAsync(int userId, ScoreSubmitRequestDto request);
        Task<PageResult<LeaderboardResponseDto>> GetLeaderboardAsync(int page,int size);
    }
}
