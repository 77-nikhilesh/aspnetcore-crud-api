using Microsoft.AspNetCore.Mvc;
using EmployeeAdminPortal.Api.Data;
using EmployeeAdminPortal.Api.Models.Entities;
using EmployeeAdminPortal.Api.Dtos;
using Microsoft.EntityFrameworkCore;
using EmployeeAdminPortal.Api.Repositories;


namespace EmployeeAdminPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IEmployeeRepository _employeeRepository;


        public EmployeesController(ApplicationDbContext dbcontext, IEmployeeRepository employeeRepository)
        {
            _dbContext = dbcontext;
            _employeeRepository = employeeRepository;
        }

        //Get method

        [HttpGet] //working good
        public async Task<IActionResult> GetAllEmployeesASync()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();

            var employeesdto=new List<EmployeesDto>();
           

            foreach(var employee in employees)
            {
                var employeeDto = new EmployeesDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Phone = employee.Phone
                };
                employeesdto.Add(employeeDto);
            }
            return Ok(employeesdto);

        }


        //Get Employee By Id
        [HttpGet("{id:guid}")] //working good
        public async Task<IActionResult> GetEmployeeByIdAsync(Guid id)
        {
            var employeeEntity = await _employeeRepository.GetEmployeeByIdAsync(id);
    
            if(employeeEntity == null)
            {
                return NotFound();
            }
            var employeeDto = new EmployeesDto
            {
                Id = employeeEntity.Id,
                Name = employeeEntity.Name,
                Email = employeeEntity.Email,
                Phone = employeeEntity.Phone
            };

            return Ok(employeeDto);
        }


        //Post Method
        [HttpPost]  //working good
        public async Task<IActionResult> AddEmployeeAsync(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = await _employeeRepository.AddEmployeeAsync(addEmployeeDto);
            var employeeDto = new EmployeesDto
            {
                Id = employeeEntity.Id,
                Name = employeeEntity.Name,
                Email = employeeEntity.Email,
                Phone = employeeEntity.Phone
            };

            return Ok(employeeDto);

         
        }



        //Update/Put Method
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateemployeedto)
        {
            var employeeEntity = await _employeeRepository.UpdateEmployeeAsync(id, updateemployeedto);


            if (employeeEntity == null)
            {
                return NotFound();

            }

            var employeeDto = new EmployeesDto
            {
                Id = employeeEntity.Id,
                Name = employeeEntity.Name,
                Email = employeeEntity.Email,
                Phone = employeeEntity.Phone
            };
            return Ok(employeeDto);
        }


        //Delete Method
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employeeEntity =await  _employeeRepository.DeleteEmployeeAsync(id);
            if (employeeEntity == null)
            {
                return NotFound();
            }
         
            return Ok("Employee deleted successfully.");
        }
    }
}
