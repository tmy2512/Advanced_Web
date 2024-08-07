using ManagementAssistanceForBusinessWeb_OnlyRole.Context;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.TaskViewModels;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.ViewModels;
using ManagementAssistanceForBusinessWeb_OnlyRole.Repository.ProjectFolder;
using Microsoft.EntityFrameworkCore;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Repository.Project
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ModelDbContext _context;

        public ProjectRepository(ModelDbContext context)
        {
            _context = context;
        }

        public void CreateNewProject(CreateNewProjectViewModel newProject)
        {
            var project = new ProjectModel()
            {
                Name = newProject.Name,
                Description = newProject.Description,
                StartTime = newProject.StartTime
            };
            _context.Add(project);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<ProjectModel>> GetAllProject()
        {
            return await _context.Projects.ToListAsync();
        }
        public async Task<ProjectModel> FindProjectByID(int id)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task UpdateProject(ProjectModel projectModel)
        {
            var oldProject = _context.Projects.Find(projectModel.ID);
            if (oldProject != null)
            {
                oldProject.Name = projectModel.Name;
                oldProject.Description = projectModel.Description;
                oldProject.StartTime = projectModel.StartTime;
                _context.Projects.Update(oldProject);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProject(int id)
        {
            // find project by id
            var project = _context.Projects.Find(id);
            if (project != null)
            {
                // C1: cach xoa duoi day lien quan den cascade delete
                // phải cấu hình cascade trong dbcontext thì mới sử dụng được
                /*_context.Projects.Remove(project);
                _context.SaveChanges();*/


                //C2:  ----start
                // find and remove task before deleting project
                var tasks = await _context.Tasks.Where(t => t.ProjectID == id).ToListAsync();
                if (tasks != null)
                {
                    _context.Tasks.RemoveRange(tasks);

                    //remove project
                    _context.Projects.Remove(project);
                    await _context.SaveChangesAsync();
                }

            }
        }

        // find out list tasks by projectid to delete 
        public async Task<IEnumerable<CreateNewTaskModel>> FindTaskByProjectID(int projectID)
        {
            return await _context.Tasks.Where(t => t.ProjectID == projectID)
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

        public async Task<IEnumerable<ProjectModel>> SearchProjectsByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return await GetAllProject();
            }

            return _context.Projects.Where(m => m.Name.Contains(name)).ToList();
        }
    }
}
