using CustomerRelationsManagement.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRelationsManagement.Web.Models
{
    public class ProjectTaskCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        [Required]
        [Display(Name = "Task Priority")]
        public TaskPriority TaskPriority { get; set; }

        [Display(Name = "Due Date")]
        public DateTime? DateDue { get; set; }
    }
}
