namespace CustomerRelationsManagement.Web.Models
{
    public class LeaveAllocationEditViewModel : LeaveAllocationViewModel
    {
        public int LeaveTypeId { get; set; }
        public string EmployeeId { get; set; }
        public EmployeeListViewModel? Employee { get; set; }
    }
}
