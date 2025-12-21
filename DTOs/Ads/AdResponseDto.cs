namespace AuthenticationforTheMemeoryGame.DTOs.Ads
{
    public class AdResponseDto
    {
        public int Id { get; set; }
        public string AdTitle { get; set; } = string.Empty;

        public string AdImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
