using System.ComponentModel.DataAnnotations;

namespace CustomerRelationsManagement.Common.Models
{
    public class LeaveTypeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Default # Of Days")]
        [Required]
        [Range(1, 28, ErrorMessage = "Please enter a valid number")]
        public int DefaultDays { get; set; }
    }
}
