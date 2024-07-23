using ManagementAssistanceForBusinessWeb_OnlyRole.Context;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.TaskViewModels;
using Microsoft.EntityFrameworkCore;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Repository.TaskFolder
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ModelDbContext _context;
        public TaskRepository(ModelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CreateNewTaskModel>> GetAllTaskByProjectID(int projectID)
        {
            return await _context.Tasks.Where(t => t.ProjectID == projectID)
                .Select(t => new CreateNewTaskModel
                {
                    Name = t.Name,
                    Content = t.Content,
                    DueDate = t.DueDate,
                    ProjectID = t.ProjectID,
                    UserID = t.UserID,
                    UpdatedAt = t.UpdatedAt,
                    Status = t.Status
                })
                .ToListAsync();
        }
        public void AddNewTask(CreateNewTaskModel newTask)
        {
            var task = new TaskModel()
            {
                Name = newTask.Name,
                Content = newTask.Content,
                DueDate = newTask.DueDate,
                ProjectID = newTask.ProjectID,
                UserID = newTask.UserID,
                Status = newTask.Status
            };
            _context.Add(task);
            _context.SaveChanges();
        }
    }
}
