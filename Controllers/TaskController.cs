using ManagementAssistanceForBusinessWeb_OnlyRole.Models;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.TaskViewModels;
using ManagementAssistanceForBusinessWeb_OnlyRole.Repository.TaskFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System.Threading.Tasks;
using static ManagementAssistanceForBusinessWeb_OnlyRole.Models.TaskModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Controllers
{
    public class TaskController:Controller
    {
        private readonly ITaskRepository _taskRepository;
        public TaskController (ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
     
        // get list task by projectid
        [HttpGet]
        public async Task<IActionResult> GetAllTaskByProjectID(int projectID, string error ="", string searchName="")
        {
            IEnumerable<CreateNewTaskModel> tasks;
			if (!string.IsNullOrEmpty(searchName))
            {
                tasks = await _taskRepository.SearchTasksByName(searchName);
            }
            else
            {
				tasks = await _taskRepository.GetAllTaskByProjectID(projectID);
			}
			var todo_tasks = tasks.Where(p => p.Status == EStatus.ToDo);
			var done_tasks = tasks.Where(p => p.Status == EStatus.Done);
			var doing_tasks = tasks.Where(p => p.Status == EStatus.Doing);

			Console.WriteLine("Test");
			ViewBag.ProjectID = projectID;
			ViewBag.Error = error;

			ViewBag.Tasks = new
			{
				TodoTasks = todo_tasks,
				DoneTasks = done_tasks,
				DoingTasks = doing_tasks
			};


			return View(tasks);
        }

		
		// show form add new task or edit task
		[HttpGet]
        public async Task<IActionResult> AddNewTask(int taskID)
        {  
            if (taskID != 0)
            {   
                var result = await EditTaskModalPartialView(taskID);
                return result;
            }
            return View();
        }

        // api xoa task theo id
        [HttpDelete]
        public IActionResult DeleteTaskByID(int taskID)
        {
            _taskRepository.DeleteTask(taskID);
            return Ok();
        }

       
        //show  Edit task form
        [HttpGet]
        public async Task<IActionResult> EditTaskModalPartialView(int taskID)
        {
            var foundTask = await _taskRepository.GetTaskById(taskID);
            var task = new 
            {
                id = foundTask.ID,
                name = foundTask.Name,
                content = foundTask.Content,
                due_date = foundTask.DueDate,
                project_id = foundTask.ProjectID,
                user_id = foundTask.UserID,
                update_date = foundTask.UpdatedAt,
                status = foundTask.Status
            };
            
            return new JsonResult(Ok(task));
        }
        //[HttpGet]
        //public IActionResult AddNewTask()
        //{
        //    return View();
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Update_Task(CreateNewTaskModel newTask)
        {
            var ErrorMessage = "";

            if (ModelState.IsValid)
            {
                    if (newTask.ID != null && newTask.ID != 0)
                    {
                        return await UpdateTask(newTask);

                    }
                    else
                    {
                        _taskRepository.AddNewTask(newTask);
                    }

            }
            else
            {
                foreach (var state in ModelState.Values)
                {
                    foreach (var error in state.Errors)
                    {
                        ErrorMessage += error.ErrorMessage + "\n";
                        //ViewBag.Error = err;
                    }
                }
            }
            
            return RedirectToAction("GetAllTaskByProjectID", new { projectID = newTask.ProjectID, error = ErrorMessage });

        }
        [HttpPost]
        public async Task<IActionResult> UpdateTask(CreateNewTaskModel updatedTask)
        {
            var ErrorMessage = "";
            if (ModelState.IsValid)
            {
                if(updatedTask == null)
                {
                    ErrorMessage = "abc";
                }
                else
                {
                    _taskRepository.UpdateTaskByID(updatedTask);
                }
            }
            else
            {
                foreach (var state in ModelState.Values)
                {
                    foreach (var error in state.Errors)
                    {
                        ErrorMessage += error.ErrorMessage + "\n";
                    }
                }

                //ErrorMessage += "Kiểm tra lại thông tin đầu vào";
            }

            return RedirectToAction("GetAllTaskByProjectID", new { projectID = updatedTask.ProjectID, error = ErrorMessage });
        }
    }
}
