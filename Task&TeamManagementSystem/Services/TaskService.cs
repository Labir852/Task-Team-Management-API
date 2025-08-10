using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Task_TeamManagementSystem.Controllers.Data;
using Task_TeamManagementSystem.Entities;
using Task_TeamManagementSystem.Entities.Models;

namespace Task_TeamManagementSystem.Services
{
    public class TaskService:ITaskService
{
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public TaskService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Tasks?> ManagerAddTask(Tasks request, Guid createdByUserId)
        {
            if (createdByUserId == Guid.Empty)
            {
                return null; // Invalid user ID
            }

            var task = new Tasks();
            task.Title = request.Title;
            task.Description = request.Description;
            task.Status = request.Status;
            task.DueDate = request.DueDate;
            task.AssignedToUserId = request.AssignedToUserId;
            task.CreatedByUserId = createdByUserId;

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<string?> ManagerUpdateTask(Tasks request)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(u => u.Id == request.Id);
            if (task is null)
            {
                return null; // or throw an exception
            }

            // Update only if provided
            if (!string.IsNullOrWhiteSpace(request.Title))
                task.Title = request.Title;

            if (!string.IsNullOrWhiteSpace(request.Description))
                task.Description = request.Description;

            if (request.Status != null)
                task.Status = request.Status;

            if (request.DueDate != null)
                task.DueDate = request.DueDate;

            if (request.AssignedToUserId != Guid.Empty)
                task.AssignedToUserId = request.AssignedToUserId;

            await _context.SaveChangesAsync();
            return "Updated the Task Details";
        }

        public async Task<IEnumerable<Tasks>> GetAllTaskAsync()
        {
            return await _context.Tasks
                .Select(t => new Tasks
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    DueDate = t.DueDate,
                    AssignedToUserId = t.AssignedToUserId,
                    CreatedByUserId = t.CreatedByUserId
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> GetEmployeeAssignedTasksAsync(Guid employeeId)
        {
            return await _context.Tasks
                .Where(t => t.AssignedToUserId == employeeId)
                .Select(t => new Tasks
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    DueDate = t.DueDate,
                    AssignedToUserId = t.AssignedToUserId,
                    CreatedByUserId = t.CreatedByUserId
                })
                .ToListAsync();
        }

        public async Task<string?> EmployeeUpdateTask(Tasks request, Guid employeeId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id && t.AssignedToUserId == employeeId);
            if (task is null)
            {
                return null; // Task not found or not assigned to this employee
            }

            // Employee can only update certain fields
            if (!string.IsNullOrWhiteSpace(request.Title))
                task.Title = request.Title;

            if (!string.IsNullOrWhiteSpace(request.Description))
                task.Description = request.Description;

            if (request.Status != null)
                task.Status = request.Status;

            if (request.DueDate != null)
                task.DueDate = request.DueDate;

            await _context.SaveChangesAsync();
            return "Task updated successfully";
        }
    }
}
