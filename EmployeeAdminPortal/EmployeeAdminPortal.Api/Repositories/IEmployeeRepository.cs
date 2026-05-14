using EmployeeAdminPortal.Api.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAdminPortal.Api.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(Guid id);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee?> UpdateEmployeeAsync(Guid id, Employee employee);
        Task<Employee> DeleteEmployeeAsync(Guid id);
    }
}