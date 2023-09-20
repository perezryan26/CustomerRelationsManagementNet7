using System.ComponentModel.DataAnnotations;

namespace CustomerRelationsManagement.Common.Models
{
    public class ProjectCreateViewModel : IValidatableObject
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string Description { get; set; }

        public List<string> EmployeeIds { get; set; }

        [Display(Name = "Team Members")]
        public List<EmployeeViewModel>? Employees { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name?.Length > 40)
            {
                yield return new ValidationResult("Name exceeds the character limit", new[] { nameof(Name) });
            }
        }
    }
}
