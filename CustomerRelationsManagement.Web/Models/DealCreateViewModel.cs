using CustomerRelationsManagement.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CustomerRelationsManagement.Web.Models
{
    public class DealCreateViewModel
    {
        [Required(ErrorMessage = "You must enter a Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "You must enter a Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "You must enter a valid Amount")]
        [Range(1, 10000000, ErrorMessage = "You must enter a valid Amount")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "You must include a Client")]
        [Display(Name = "Client")]
        public int ClientId { get; set; }

        public SelectList? Clients { get; set; }
    }
}
