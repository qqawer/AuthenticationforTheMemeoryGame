namespace AuthenticationforTheMemeoryGame.DTOs.Auth
{
    public class LoginResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public int UserId { get; set; }
        public bool IsPaid { get; set; }
    }
}
