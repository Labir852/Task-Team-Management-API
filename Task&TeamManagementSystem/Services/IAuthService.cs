using Task_TeamManagementSystem.Entities;
using Task_TeamManagementSystem.Entities.Models;

namespace Task_TeamManagementSystem.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(LoginDto request);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
    }
}
