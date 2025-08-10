using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Task_TeamManagementSystem.Entities.Models;
using Task_TeamManagementSystem.Services;

namespace Task_TeamManagementSystem.Controllers
{
    [Route("api/[controller]")]
[ApiController]
public class TaskController(ITaskService taskService) : ControllerBase
{
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("ManagerAddTask")]
        public async Task<ActionResult<Tasks>> ManagerAddTask(Tasks request)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized("Invalid user ID");
            }

            var response = await taskService.ManagerAddTask(request, userId);
            if (response == null)
            {
                return BadRequest("Failed to create task");
            }
            
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("ManagerUpdateTask")]
        public async Task<ActionResult<string>> ManagerUpdateTask(Tasks request)
        {
            var response = await taskService.ManagerUpdateTask(request);
            if (response == null)
            {
                return BadRequest("Task couldn't be found");
            }
            return Ok(response);

        }

        [HttpGet("Tasklist")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetTasks()
        {
            var response = await taskService.GetAllTaskAsync();
            return Ok(response);
        }

        [HttpGet("EmployeeTasks")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetEmployeeTasks()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized("Invalid user ID");
            }

            var response = await taskService.GetEmployeeAssignedTasksAsync(userId);
            return Ok(response);
        }

        [HttpPut("EmployeeUpdateTask")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<string>> EmployeeUpdateTask(Tasks request)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized("Invalid user ID");
            }

            var response = await taskService.EmployeeUpdateTask(request, userId);
            if (response == null)
            {
                return BadRequest("Task not found or not assigned to you");
            }
            return Ok(response);
        }

    }
}
