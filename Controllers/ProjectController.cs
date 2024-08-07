using AutoMapper;
using AutoMapper.Execution;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.ProjectViewModels;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.ViewModels;
using ManagementAssistanceForBusinessWeb_OnlyRole.Repository.ProjectFolder;
using Microsoft.AspNetCore.Mvc;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Controllers
{
    public class ProjectController:Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _autoMapper;

        public ProjectController(IProjectRepository projectRepository, IMapper autoMapper)
        {
            _projectRepository = projectRepository;
            _autoMapper = autoMapper;
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
        public async Task<IActionResult> GetAllProject(string searchName)
        {
            IEnumerable<ProjectModel> projectModels;
            IEnumerable<ProjectDTO> projectDTOs;
            if (string.IsNullOrEmpty(searchName))
            {
                 projectModels = await _projectRepository.GetAllProject();
                 projectDTOs = _autoMapper.Map<IEnumerable<ProjectDTO>>(projectModels);
            }
            else
            {
                projectModels = await _projectRepository.SearchProjectsByName(searchName);
                projectDTOs = _autoMapper.Map<IEnumerable<ProjectDTO>>(projectModels);
            }
            return View(projectDTOs);
        }
        [HttpGet]
        public async Task<IActionResult> ProjectDetail(int projectID)
        {
            var project = await _projectRepository.FindProjectByID(projectID);
            if(project == null)
            {
                return NotFound();
            }
            var projectDTO = _autoMapper.Map<ProjectDTO>(project);
            return new JsonResult(Ok(projectDTO));
        }
        [HttpPost]
        public IActionResult CreateNewProject(CreateNewProjectViewModel newProject) 
        {
            _projectRepository.CreateNewProject(newProject);
            return RedirectToAction("GetAllProject");
        }
        // update project
        [HttpPost]
        public async Task<IActionResult> UpdateProject(ProjectDTO projectDTO)
        {
            if(ModelState.IsValid)
            {
                var projectModel = _autoMapper.Map<ProjectModel>(projectDTO);
                await _projectRepository.UpdateProject(projectModel);
                return RedirectToAction("GetAllProject");
            }
            return await ProjectDetail(projectDTO.ID);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProject(int projectID)
        {
            await _projectRepository.DeleteProject(projectID);
            return NoContent();
        }
    }
}
