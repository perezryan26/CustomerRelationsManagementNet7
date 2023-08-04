using System.ComponentModel.DataAnnotations;

namespace CustomerRelationsManagement.Web.Models
{
    public class PositionViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Salary { get; set; }
    }
}
