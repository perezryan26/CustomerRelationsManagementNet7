using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRelationsManagement.Web.Data
{
    public class LeaveAllocation : BaseEntity
    {
        public int NumberOfDays { get; set; }

        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }

        public string EmployeeId { get; set; }

        public int Period { get; set; } //When an employee is getting a certain number of days for a certain leave type,
        // it describes a year or a start and end date
    }
}
