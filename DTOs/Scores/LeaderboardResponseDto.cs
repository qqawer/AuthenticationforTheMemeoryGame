namespace AuthenticationforTheMemeoryGame.DTOs.Scores
{
    public class LeaderboardResponseDto
    {
        public string Username { get; set; }
        public int CompleteTimeSeconds { get; set; }
        public DateTime CompleteAt { get; set; }
    }
}
