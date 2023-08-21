using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRelationsManagement.Common.Models
{
    public class ProjectTaskViewModel : ProjectTaskCreateViewModel
    {
        public int Id { get; set; }

        [ForeignKey("ProjectId")]
        public ProjectViewModel Project { get; set; }

        public string EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        [Display(Name = "Team Member")]
        public EmployeeListViewModel? Employee { get; set; }

        [Display(Name="Status")]
        public bool IsComplete { get; set; }
    }
}
