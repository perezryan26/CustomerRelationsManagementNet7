using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Contracts;

namespace CustomerRelationsManagement.Web.Data
{
    public class Employee : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TaxId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateJoined { get; set; }

    }
}
