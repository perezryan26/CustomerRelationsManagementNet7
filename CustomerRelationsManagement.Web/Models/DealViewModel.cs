using CustomerRelationsManagement.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRelationsManagement.Web.Models
{
    public class DealViewModel : DealCreateViewModel
    {
        public int Id { get; set; }

        public DealStatus? Status { get; set; }

        public Client? Client { get; set; }

        //Additional Properties
        [Display(Name = "Date Closed")]
        public DateTime? DateClosed { get; set; }
    }
}
