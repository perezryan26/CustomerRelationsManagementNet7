using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CustomerRelationsManagement.Common.Constants;

namespace CustomerRelationsManagement.Common.Models
{
    public class ProjectTaskCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Task Priority")]
        public TaskPriority TaskPriority { get; set; }

        [Display(Name = "Due Date")]
        public DateTime? DateDue { get; set; }

        [Required]
        [Display(Name = "Project")]
        public int ProjectId { get; set; }
    }
}
