using System.ComponentModel.DataAnnotations;

namespace CustomerRelationsManagement.Web.Models
{
    public class LeaveTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Default Number Of Days")]
        public int DefaultDays { get; set; }
    }
}
