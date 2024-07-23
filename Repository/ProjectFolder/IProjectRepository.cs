using ManagementAssistanceForBusinessWeb_OnlyRole.Models;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.ViewModels;
namespace ManagementAssistanceForBusinessWeb_OnlyRole.Repository.ProjectFolder
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectModel>> GetAllProject();
        void CreateNewProject(CreateNewProjectViewModel newProject);
        Task<IEnumerable<ProjectModel>> FindProjectByID(int id);
        
    }
}
