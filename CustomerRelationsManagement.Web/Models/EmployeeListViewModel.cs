using CustomerRelationsManagement.Web.Data;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace CustomerRelationsManagement.Web.Models
{
    public class EmployeeListViewModel
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [Display(Name = "Date Joined")]
        public string DateJoined { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }
        public string Department { get; set; }
        public Position? Position { get; set; }
    }
}
