using System.ComponentModel.DataAnnotations;

namespace AuthenticationforTheMemeoryGame.DTOs.Scores
{
    public class ScoreSubmitRequestDto
    {
        [Required]
        [Range(1, int.MaxValue,ErrorMessage ="Time must be positive")]
        public int CompletionTimeSeconds { get; set; }
    }
}
