using ManagementAssistanceForBusinessWeb_OnlyRole.Models;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.TaskViewModels;
using ManagementAssistanceForBusinessWeb_OnlyRole.Repository.TaskFolder;
using Microsoft.AspNetCore.Mvc;
using static ManagementAssistanceForBusinessWeb_OnlyRole.Models.TaskModel;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Controllers
{
    public class TaskController:Controller
    {
        private readonly ITaskRepository _taskRepository;
        public TaskController (ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public IActionResult GetTask()
        {
               return View();
        }
        // get list task by projectid
        [HttpGet]
        public async Task<IActionResult> GetAllTaskByProjectID(int projectID, string name ="My")
        {

            var tasks = await _taskRepository.GetAllTaskByProjectID(projectID);
            var todo_tasks = tasks.Where(p => p.Status == EStatus.ToDo);
            var done_tasks = tasks.Where(p => p.Status == EStatus.Done);
            var doing_tasks = tasks.Where(p => p.Status == EStatus.Doing);

            Console.WriteLine("Test");
            ViewBag.ProjectID = projectID;
            ViewBag.Name = name;
       
            ViewBag.Tasks = new
            {
                TodoTasks = todo_tasks,
                DoneTasks = done_tasks,
                DoingTasks = doing_tasks
            };

            return View(tasks);
        }

        // show form add new task
        [HttpGet]
        public IActionResult AddNewTask()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewTask(CreateNewTaskModel newTask)
        {
            if(ModelState.IsValid)
            {
                _taskRepository.AddNewTask(newTask);
                return RedirectToAction("GetAllTaskByProjectID", new { projectID = newTask.ProjectID });
            }
            ViewBag.ErrorMessage = "Kiểm tra lại thông tin đầu vào";
            return View();
            
        }
    }
}
