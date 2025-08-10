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
    public class UserController(IUserService _userService, IAuthService _authService) : ControllerBase
    {
        


        [Authorize(Roles = "Admin")]
        [HttpPost("AdminAddUser")]
        public async Task<ActionResult<User>> AdminAddUser(UserDto request)
        {
            var user = await _authService.RegisterAsync(request);
            if (user == null)
            {
                return BadRequest("Username already exists.");
            }
            return Ok(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("AdminUpdateUser")]
        public async Task<ActionResult<string>> AdminUpdateUser(updateUser request)
        {
            var user = await _userService.AdminUpdateUser(request);
            if (user == null)
            {
                return BadRequest("User couldn't be found");
            }
            return Ok(user);

        }

        [HttpGet("Userlist")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only Admin can delete
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
                return NotFound(new { message = "User not found" });

            return Ok(new { message = "User deleted successfully" });
        }

    }
}