using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomerRelationsManagement.Common.Models
{
    public class LeaveRequestCreateViewModel : IValidatableObject
    {
        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Please select a valid leave type.")]
        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }

        public SelectList? LeaveTypes { get; set; }

        [Display(Name = "Request Comments")]
        public string? RequestComments { get; set; }

        //custom logic for validating data, like making sure start date is before end date
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(StartDate > EndDate)
            {
                yield return new ValidationResult("The start date must be before the end date", new[] { nameof(StartDate), nameof(EndDate) });
            }

            if(RequestComments?.Length > 250)
            {
                yield return new ValidationResult("Comments exceed the character limit", new[] { nameof(RequestComments) });
            }
        }
    }
}
