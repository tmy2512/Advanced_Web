using static ManagementAssistanceForBusinessWeb_OnlyRole.Models.TaskModel;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.TaskViewModels;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Repository.TaskFolder
{
    public interface ITaskRepository
    {
        Task<IEnumerable<CreateNewTaskModel>> GetAllTaskByProjectID(int projectID);
        void AddNewTask(CreateNewTaskModel newTask);
    }
}
