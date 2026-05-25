using EmployeeAdminPortal.Api.Dtos;
using EmployeeAdminPortal.Api.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAdminPortal.Api.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(Guid id);
        Task<Employee> AddEmployeeAsync(AddEmployeeDto addEmployeeDto);
        Task<Employee?> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateEmployeeDto);
        Task<Employee?> DeleteEmployeeAsync(Guid id);

        
    }
}