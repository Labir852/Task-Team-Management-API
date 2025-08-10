namespace Task_TeamManagementSystem.Entities.Models
{
    public class TokenResponseDto
    {
        public required string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
