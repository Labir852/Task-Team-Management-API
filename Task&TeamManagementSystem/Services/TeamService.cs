using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task_TeamManagementSystem.Controllers.Data;
using Task_TeamManagementSystem.Entities;
using Task_TeamManagementSystem.Entities.Models;

namespace Task_TeamManagementSystem.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public TeamService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Team?> AdminAddNewTeam(Team request)
        {

            var team = new Team();
            team.Name = request.Name;
            team.Description = request.Description;

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<string?> AdminUpdateTeam(Team request)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(u => u.Id == request.Id);
            if (team is null)
            {
                return null; // or throw an exception
            }

            // Update only if provided
            if (!string.IsNullOrWhiteSpace(request.Name))
                team.Name = request.Name;

            if (!string.IsNullOrWhiteSpace(request.Description))
                team.Description = request.Description;


            await _context.SaveChangesAsync();
            return "Updated the Team Details";
        }


        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _context.Teams
                .Select(u => new Team
                {
                    Id = u.Id,
                    Name = u.Name,
                    Description = u.Description
                })
                .ToListAsync();
        }

        public async Task<bool> DeletTeamAsync(Guid teamId)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(u => u.Id == teamId);
            if (team == null)
                return false;

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}