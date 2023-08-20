using CustomerRelationsManagement.Web.Data;

namespace CustomerRelationsManagement.Web.Contracts
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee> GetAsync(string? id);
        Task<List<Employee>> GetAllAsync();
        Task UpdateEmployeeAsync(Employee employee);
    }
}
