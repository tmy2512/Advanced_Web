using ManagementAssistanceForBusinessWeb_OnlyRole.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Context
{
    public class ModelDbContext : DbContext
    {
        public ModelDbContext(DbContextOptions<ModelDbContext> options) : base(options) { }
        public DbSet<UserModel> Members { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<StatusModel> Statuses { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        
    }
}
