using System.ComponentModel.DataAnnotations;

namespace CustomerRelationsManagement.Web.Models
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Display(Name = "Company Name")]
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
