using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_TeamManagementSystem.Entities;
using Task_TeamManagementSystem.Entities.Models;
using Task_TeamManagementSystem.Services;

namespace Task_TeamManagementSystem.Controllers
{
    [Route("api/[controller]")]
[ApiController]
public class TeamController(ITeamService teamService) : ControllerBase
{
        [Authorize(Roles = "Admin")]
        [HttpPost("AdminAddTeam")]
        public async Task<ActionResult<Team>> AdminAddNewTeam(Team request)
        {
            var response = await teamService.AdminAddNewTeam(request);
            if (response == null)
            {
                return BadRequest("Team already exists.");
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("AdminUpdateTeam")]
        public async Task<ActionResult<string>> AdminUpdateTeam(Team request)
        {
            var response = await teamService.AdminUpdateTeam(request);
            if (response == null)
            {
                return BadRequest("User couldn't be found");
            }
            return Ok(response);

        }

        [HttpGet("Teamlist")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetTeams()
        {
            var response = await teamService.GetAllTeamsAsync();
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only Admin can delete
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await teamService.DeletTeamAsync(id);
            if (!result)
                return NotFound(new { message = "Team not found" });

            return Ok(new { message = "Team deleted successfully" });
        }
    }
}
