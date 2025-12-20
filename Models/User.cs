using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace AuthenticationforTheMemeoryGame.Models
{
    [Tags("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Username { get; set; }=string.Empty; 

        [Required]
        [MaxLength(256)]
        [Column(TypeName = "nvarchar(256)")]
        public string Password { get; set; }=string.Empty;

        [Required]
        [Column(TypeName = "bit")]
        public bool IsPaid { get; set; }

        public ICollection<Score> Scores { get; set; }=new List<Score>();
    }
}
