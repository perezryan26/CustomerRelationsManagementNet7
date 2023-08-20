namespace CustomerRelationsManagement.Web.Data
{
    public class Project : BaseEntity
    {
        public bool IsComplete { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Employee> Employees { get; set; }    
    }
}
