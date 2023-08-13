using CustomerRelationsManagement.Web.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRelationsManagement.Web.Models
{
    public class ProjectTaskViewModel : ProjectTaskCreateViewModel
    {
        public int Id { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public string EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        [Display(Name = "Team Member")]
        public EmployeeListViewModel? Employee { get; set; }

        [Display(Name="Status")]
        public bool IsComplete { get; set; }
    }
}
