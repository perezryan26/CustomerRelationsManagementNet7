using System.ComponentModel.DataAnnotations;

namespace CustomerRelationsManagement.Common.Models
{
    public class ProjectCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public List<string> EmployeeIds { get; set; }

        [Display(Name = "Team Members")]
        public List<EmployeeViewModel>? Employees { get; set; }
    }
}
