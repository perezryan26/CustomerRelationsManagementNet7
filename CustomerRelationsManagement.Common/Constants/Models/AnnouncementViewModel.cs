using System.ComponentModel.DataAnnotations;

namespace CustomerRelationsManagement.Common.Models
{
    public class AnnouncementViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
