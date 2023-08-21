using CustomerRelationsManagement.Data;
using CustomerRelationsManagement.Application.Contracts;

namespace CustomerRelationsManagement.Application.Contracts
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee> GetAsync(string? id);
        Task<List<Employee>> GetAllAsync();
        Task UpdateEmployeeAsync(Employee employee);
    }
}
