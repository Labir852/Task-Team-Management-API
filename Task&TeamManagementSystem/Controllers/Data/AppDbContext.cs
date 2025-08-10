using Task_TeamManagementSystem.Controllers;
using Microsoft.EntityFrameworkCore;
using Task_TeamManagementSystem.Entities;
using Task_TeamManagementSystem.Entities.Models;

namespace Task_TeamManagementSystem.Controllers.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Tasks> Tasks { get; set; }

    }

}
