
using AuthenticationforTheMemeoryGame.DTOs.Scores;

namespace AuthenticationforTheMemeoryGame.Interfaces
{
    public interface IScoreService
    {
        Task<bool> SubmitScoreAsync(int userId, ScoreSubmitRequestDto request);
        Task<List<LeaderboardResponseDto>> GetLeaderboardAsync(int page,int size);
    }
}
