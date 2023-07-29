using System.ComponentModel.DataAnnotations;

namespace CustomerRelationsManagement.Web.Models
{
    public class EmployeeAllocationViewModel : EmployeeListViewModel
    {
        public List<LeaveAllocationViewModel> LeaveAllocations { get; set; }
    }
}
