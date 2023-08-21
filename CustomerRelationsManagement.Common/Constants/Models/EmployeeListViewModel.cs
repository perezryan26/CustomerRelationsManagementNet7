using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace CustomerRelationsManagement.Common.Models
{
    public class EmployeeListViewModel
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [Display(Name = "Date Joined")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DateJoined { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }
        public string Department { get; set; }
        public PositionViewModel? Position { get; set; }
    }
}
