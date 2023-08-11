using CustomerRelationsManagement.Web.Data.Migrations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace CustomerRelationsManagement.Web.Data
{
    public class Employee : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [ForeignKey("PositionId")]
        public Position Position { get; set; }
        public int PositionId { get; set; }

        public string? Department { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }

        public ICollection<Project>? Projects { get; set; }
    }
}
