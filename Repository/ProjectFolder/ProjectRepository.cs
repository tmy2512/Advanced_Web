using ManagementAssistanceForBusinessWeb_OnlyRole.Context;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models;
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
                Description = newProject.Description
            };
            _context.Add(project);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<ProjectModel>> GetAllProject()
        {
            return await _context.Projects.ToListAsync();   
        }
        public async Task<IEnumerable<ProjectModel>> FindProjectByID(int id)
        {

            return (IEnumerable<ProjectModel>)await _context.Projects.FirstOrDefaultAsync(p => id == p.ID);
        }

    }
}
