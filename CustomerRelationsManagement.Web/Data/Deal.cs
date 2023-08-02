using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace CustomerRelationsManagement.Web.Data
{
    public class Deal : BaseEntity
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DealStatus Status { get; set; }

        //Relationship with Client entity
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public int ClientId { get; set; }

        //Additional Properties
        public DateTime? DateClosed { get; set; }

        public Deal()
        {
            Status = DealStatus.Open;
            //DateCreated = DateTime.Now;
        }
    }
}
