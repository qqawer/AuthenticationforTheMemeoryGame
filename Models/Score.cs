using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationforTheMemeoryGame.Models
{
    [Tags("Scores")]
    public class Score
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [Range(1,int.MaxValue)]
        public int CompletionTimeSeconds { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CompleteAt { get; set; }=DateTime.Now;

        public User User { get; set; } = null;
    }
}
