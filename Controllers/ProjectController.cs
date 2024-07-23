using ManagementAssistanceForBusinessWeb_OnlyRole.Models;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.ViewModels;
using ManagementAssistanceForBusinessWeb_OnlyRole.Repository.ProjectFolder;
using Microsoft.AspNetCore.Mvc;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Controllers
{
    public class ProjectController:Controller
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        [HttpGet]
        public IActionResult CreateNewProject()
        {
            return View();
        }
        //[HttpGet]
        //public async Task<IActionResult> ProjectDetail(int id)
        //{
        //    ProjectModel project = await _projectRepository.FindProjectByID(id);
        //    if(project == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(project);
        //}
        [HttpGet]
        public async Task<IActionResult> GetAllProject()
        {
            IEnumerable<ProjectModel> projects;
            projects = await _projectRepository.GetAllProject();    
            return View(projects);
        }
        [HttpPost]
        public IActionResult CreateNewProject(CreateNewProjectViewModel newProject) 
        {
            _projectRepository.CreateNewProject(newProject);
            return RedirectToAction("GetAllProject");
        }
    }
}
