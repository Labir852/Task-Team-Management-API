using Task_TeamManagementSystem.Entities.Models;

namespace Task_TeamManagementSystem.Services
{
    public interface ITeamService
{
        Task<Team?> AdminAddNewTeam(Team request);
        Task<string?> AdminUpdateTeam(Team request);

        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task<bool> DeletTeamAsync(Guid teamId);
    }
}
