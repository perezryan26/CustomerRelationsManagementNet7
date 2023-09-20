using System.ComponentModel.DataAnnotations;

namespace CustomerRelationsManagement.Common.Models
{
    public class PositionViewModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Salary { get; set; }

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

            if(Salary < 0)
            {
                yield return new ValidationResult("Salary cannot be negative", new[] { nameof(Salary) });
            }
        }
    }
}
