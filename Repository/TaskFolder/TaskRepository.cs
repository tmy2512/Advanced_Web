using AutoMapper;
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
                    ID = t.ID,
                    Name = t.Name,
                    Content = t.Content,
                    DueDate = t.DueDate,
                    VerifyKey = t.VerifyKey,
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
                VerifyKey = newTask.VerifyKey,
                ProjectID = newTask.ProjectID,
                UserID = newTask.UserID,
                Status = newTask.Status
            };
            _context.Add(task);
            _context.SaveChanges();
        }

        public async Task<TaskModel> GetTaskById(int Id)
        {
            return await _context.Tasks.FindAsync(Id);
        }

        public CreateNewTaskModel UpdateTaskByID(CreateNewTaskModel task)
        {
            var findTaskByID = _context.Tasks.Find(task.ID);

            findTaskByID.Name = task.Name;
            findTaskByID.Content = task.Content;
            findTaskByID.DueDate = task.DueDate;
            findTaskByID.Status = task.Status;
            findTaskByID.UpdatedAt = DateTime.Now;
            findTaskByID.ProjectID = task.ProjectID;
            findTaskByID.UserID = task.UserID;
            _context.Update(findTaskByID);
            _context.SaveChanges();
            return task;
        }

        public void DeleteTask(int taskId)
        {
            TaskModel task = _context.Tasks.Find(taskId);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }

        }

        public async Task<IEnumerable<CreateNewTaskModel>> SearchTasksByName(string name)
        {
            {
                return await _context.Tasks.Where(t => t.Name.Contains(name))
              .Select(t => new CreateNewTaskModel
              {
                  ID = t.ID,
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
        }
    }
}