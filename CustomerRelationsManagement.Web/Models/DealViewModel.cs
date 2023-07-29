using CustomerRelationsManagement.Web.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRelationsManagement.Web.Models
{
    public class DealViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DealStatus? Status { get; set; }

        //Relationship with Client entity
        [Required]
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client? Client { get; set; }
       
        //Additional Properties
        [Display(Name = "Date Closed")]
        public DateTime? DateClosed { get; set; }
    }
}
