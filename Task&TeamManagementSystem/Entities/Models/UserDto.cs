namespace Task_TeamManagementSystem.Entities.Models
{
    public class UserDto
    {
        public string FullName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; }
    }
}
