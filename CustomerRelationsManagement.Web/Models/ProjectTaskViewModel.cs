using CustomerRelationsManagement.Web.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRelationsManagement.Web.Models
{
    public class ProjectTaskViewModel : ProjectTaskCreateViewModel
    {
        public int Id { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public string EmployeeId { get; set; }

        public bool IsComplete { get; set; }
    }
}
