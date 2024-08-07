using static ManagementAssistanceForBusinessWeb_OnlyRole.Models.TaskModel;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.TaskViewModels;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Repository.TaskFolder
{
    public interface ITaskRepository
    {
        Task<IEnumerable<CreateNewTaskModel>> GetAllTaskByProjectID(int projectID);
        void AddNewTask(CreateNewTaskModel newTask);
        Task<TaskModel> GetTaskById(int Id);
        CreateNewTaskModel UpdateTaskByID(CreateNewTaskModel task);
        void DeleteTask(int taskId);
        Task<IEnumerable<CreateNewTaskModel>> SearchTasksByName(string name);

    }
}
