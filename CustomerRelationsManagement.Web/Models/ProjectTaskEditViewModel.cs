using CustomerRelationsManagement.Web.Constants;
using System.ComponentModel.DataAnnotations;

namespace CustomerRelationsManagement.Web.Models
{
    public class ProjectTaskEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Task Priority")]
        public TaskPriority TaskPriority { get; set; }

        [Display(Name = "Due Date")]
        public DateTime? DateDue { get; set; }

        [Display(Name = "Status")]
        public bool IsComplete { get; set; }

        [Required]
        [Display(Name = "Project")]
        public int ProjectId { get; set; }
    }
}
