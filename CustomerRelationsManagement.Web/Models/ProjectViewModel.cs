using CustomerRelationsManagement.Web.Data;
using System.ComponentModel.DataAnnotations;

namespace CustomerRelationsManagement.Web.Models
{
    public class ProjectViewModel : ProjectCreateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Status")]
        public bool IsComplete { get; set; }
        public DateTime DateCreated { get; set; }


        //add team members later on
    }
}
