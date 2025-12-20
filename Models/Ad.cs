using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace AuthenticationforTheMemeoryGame.Models
{
    [Tags("Ads")]
    public class Ad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        [Column(TypeName = "varchar(256)")]
        public string AdImageUrl { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string ? AdTitle { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
    }
}
