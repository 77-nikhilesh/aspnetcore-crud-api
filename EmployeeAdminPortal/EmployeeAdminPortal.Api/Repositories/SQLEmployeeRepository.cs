using AutoMapper;
using EmployeeAdminPortal.Api.Data;
using EmployeeAdminPortal.Api.Dtos;
using EmployeeAdminPortal.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Api.Repositories
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper mapper;

        public SQLEmployeeRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }


        //Get All Employees
        public async Task<List<Employee>> GetAllEmployeesAsync(string? filterOn=null, string? filterQuery=null,
            string? sortBy=null, bool isAscending=true)
        {
            var employees = _dbContext.Employees.AsQueryable();

            //filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrEmpty(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    employees=employees.Where(x=>x.Name.Contains(filterQuery));
                }
            }

            //sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Contains("Name", StringComparison.OrdinalIgnoreCase))
                {
                    employees= isAscending? employees.OrderBy(x=> x.Name) : employees.OrderByDescending(x => x.Name);
                }
            }

            return await employees.ToListAsync();
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
            //var employee = new Employee
            //{
            //    Name = addEmployeeDto.Name,
            //    Email = addEmployeeDto.Email,
            //    Phone = addEmployeeDto.Phone,
            //    Salary = addEmployeeDto.Salary
            //};

            //Auto Mapper
             var employee = mapper.Map<Employee>(addEmployeeDto);
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

            //Auto Mapper
            mapper.Map(updateEmployeeDto, existingEmployee);

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

    

      

        

       

   

      
    }
}