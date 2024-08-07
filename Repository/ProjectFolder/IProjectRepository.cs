using ManagementAssistanceForBusinessWeb_OnlyRole.Models;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.ProjectViewModels;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.TaskViewModels;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.ViewModels;
namespace ManagementAssistanceForBusinessWeb_OnlyRole.Repository.ProjectFolder
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectModel>> GetAllProject();
        void CreateNewProject(CreateNewProjectViewModel newProject);
        Task<ProjectModel> FindProjectByID(int id);
        Task UpdateProject(ProjectModel newProject);
        Task DeleteProject(int id);
        Task<IEnumerable<CreateNewTaskModel>> FindTaskByProjectID(int projectID);
        Task<IEnumerable<ProjectModel>> SearchProjectsByName(string name);

    }
}
