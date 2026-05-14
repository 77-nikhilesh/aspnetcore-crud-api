using EmployeeAdminPortal.Api.Data;
using EmployeeAdminPortal.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Api.Repositories
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SQLEmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        //Get All Employees
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        //Get Employee By Id
        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            return await _dbContext.Employees.FindAsync(id);
        }

        //Add Employee
        public async Task<Employee> AddEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
            return await employee;
        }

        //Update employee
        public async Task<Employee> UpdateEmployee(Guid id, Employee employee)
        {
            var existingEmployee = await _dbContext.Employees.FindAsync(id);
            if (existingEmployee== null)
            {
                return NotFound();
            }
            existingEmployee.Name = employee.Name;
            existingEmployee.Email = employee.Email;
            existingEmployee.Phone = employee.Phone;

            _dbContext.Employees.Update(existingEmployee);
            await _dbContext.SaveChangesAsync();
            return existingEmployee;
        }

        //Delete Employee
        public async Task<Employee> DeleteEmployeeAsyn(Guid id)
        {
            var existingemployee = await _dbContext.Employees.FindAsync(id);
            if (existingemployee == null)
            {
                return NotFound();
            }
            await _dbContext.Employees.DeleteAsync(existingemployee);
            _dbContext.SaveChanges();
            return existingemployee;
        }
    }
}