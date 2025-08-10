using Task_TeamManagementSystem.Entities.Models;

namespace Task_TeamManagementSystem.Services
{
    public interface ITaskService
{

        Task<Tasks?> ManagerAddTask(Tasks request, Guid createdByUserId);
        Task<string?> ManagerUpdateTask(Tasks request);

        Task<IEnumerable<Tasks>> GetAllTaskAsync();
        
        Task<IEnumerable<Tasks>> GetEmployeeAssignedTasksAsync(Guid employeeId);
        Task<string?> EmployeeUpdateTask(Tasks request, Guid employeeId);

    }
}
