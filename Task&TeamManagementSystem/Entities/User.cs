namespace Task_TeamManagementSystem.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; }
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty ;
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
