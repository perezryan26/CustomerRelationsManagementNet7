using CustomerRelationsManagement.Common.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerRelationsManagement.Data
{
    public class ProjectTask : BaseEntity
    {
        public bool IsComplete { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        
        public string EmployeeId { get; set; }

        public TaskPriority TaskPriority { get; set; }

        public DateTime DateDue { get; set; }

    }
}
