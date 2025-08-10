using Task_TeamManagementSystem.Entities;
using Task_TeamManagementSystem.Entities.Models;

namespace Task_TeamManagementSystem.Services
{
    public interface IUserService
    {
        Task<string?> AdminUpdateUser(updateUser request);

        Task<IEnumerable<updateUser>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(Guid userId);

    }
}
