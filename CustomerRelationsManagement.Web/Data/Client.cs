namespace CustomerRelationsManagement.Web.Data
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        //public virtual ICollection<Deal>? Deals { get; set; }

        /*
        public Client()
        {
            DateCreated = DateTime.Now;
        }
        */
    }
}
   
