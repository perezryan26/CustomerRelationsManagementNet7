using System.ComponentModel.DataAnnotations;

namespace CustomerRelationsManagement.Common.Models
{
    public class EmployeeAllocationViewModel : EmployeeListViewModel
    {
        public List<LeaveAllocationViewModel> LeaveAllocations { get; set; }
    }
}
