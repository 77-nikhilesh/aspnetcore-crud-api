using EmployeeAdminPortal.Api.Data;
using EmployeeAdminPortal.Api.Dtos;
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
            var employees = await _dbContext.Employees.ToListAsync();
            return employees;
        }

        //Get Employee By Id
        public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if(employee == null) {
                return null;
            }
            return employee;
        }

        //Add Employee
        public async Task<Employee> AddEmployeeAsync(AddEmployeeDto addEmployeeDto)
        {
            var employee = new Employee
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }

        //Update employee
        public async Task<Employee?> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var existingEmployee = await _dbContext.Employees.FindAsync(id);
            if (existingEmployee== null)
            {
                return null;
            }
            existingEmployee.Name = updateEmployeeDto.Name;
            existingEmployee.Email = updateEmployeeDto.Email;
            existingEmployee.Phone = updateEmployeeDto.Phone;
            existingEmployee.Salary = updateEmployeeDto.Salary;

            _dbContext.Employees.Update(existingEmployee);
            await _dbContext.SaveChangesAsync();
            return existingEmployee;
        }

  

        //Delete Employee
        public async Task<Employee?> DeleteEmployeeAsync(Guid id)
        {
            var existingemployee = await _dbContext.Employees.FindAsync(id);
            if (existingemployee == null)
            {
                return null;
            }
            _dbContext.Employees.Remove(existingemployee);
            await _dbContext.SaveChangesAsync();
            return existingemployee;
        }

    

        public Task<Employee?> UpdateEmployeeAsync(Guid id, Employee employee)
        {
            throw new NotImplementedException();
        }

        

       

   

      
    }
}