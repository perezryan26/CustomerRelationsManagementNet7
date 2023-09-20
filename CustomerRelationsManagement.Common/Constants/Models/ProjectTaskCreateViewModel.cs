using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CustomerRelationsManagement.Common.Constants;

namespace CustomerRelationsManagement.Common.Models
{
    public class ProjectTaskCreateViewModel : IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name?.Length > 20)
            {
                yield return new ValidationResult("Name exceed the character limit", new[] { nameof(Name) });
            }

            if (Description?.Length > 300)
            {
                yield return new ValidationResult("Description exceed the character limit", new[] { nameof(Description) });
            }
        }
    }
}
