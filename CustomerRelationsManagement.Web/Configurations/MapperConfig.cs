using AutoMapper;
using CustomerRelationsManagement.Web.Data;
using CustomerRelationsManagement.Web.Models;

namespace CustomerRelationsManagement.Web.Configurations
{
    public class MapperConfig : Profile
    {
        /* Configuration that tells automapper that it is legal to convert from
         type A to type B. Reverse map basically says that the reverse of this
        is also legal */
        public MapperConfig()
        {
            
            CreateMap<Employee, EmployeeListViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeAllocationViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();

            CreateMap<LeaveType, LeaveTypeViewModel>().ReverseMap();

            CreateMap<LeaveAllocation, LeaveAllocationViewModel>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationEditViewModel>().ReverseMap();

            CreateMap<LeaveRequest, LeaveRequestCreateViewModel>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestViewModel>().ReverseMap();

            CreateMap<Position, PositionViewModel>().ReverseMap();

            CreateMap<Announcement, AnnouncementViewModel>().ReverseMap();

            CreateMap<Project, ProjectViewModel>().ReverseMap();
            CreateMap<Project, ProjectCreateViewModel>().ReverseMap();

            CreateMap<ProjectTask, ProjectTaskViewModel>().ReverseMap();
            CreateMap<ProjectTask, ProjectTaskCreateViewModel>().ReverseMap();
            CreateMap<ProjectTask, ProjectTaskEditViewModel>().ReverseMap();
        }
    }
}
