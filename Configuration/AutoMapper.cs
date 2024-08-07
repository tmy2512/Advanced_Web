using AutoMapper;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.ProjectViewModels;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.UserViewModels;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Configuration
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<ProjectModel, ProjectDTO>();
            CreateMap<ProjectDTO, ProjectModel>();
            CreateMap<UserModel, UserDTOModel>();
            CreateMap<UserDTOModel, UserModel>();

        }
    }
}
