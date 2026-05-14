using Microsoft.AspNetCore.Mvc;
using EmployeeAdminPortal.Api.Data;
using EmployeeAdminPortal.Api.Models.Entities;
using EmployeeAdminPortal.Api.Dtos;



namespace EmployeeAdminPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;

        public EmployeesController(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        //GET All Employees Details

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employees = _dbContext.Employees.ToList();

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


        //GET Employee details By Id
        [HttpGet("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employeeEntity = _dbContext.Employees.Find(id);
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


        //POST New Employee
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {

            var employeeEntity = new Employee
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };

            _dbContext.Employees.Add(employeeEntity);
            _dbContext.SaveChanges();


            var employeeDto = new AddEmployeeDto
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };

            return CreatedAtAction(nameof(GetAllEmployees), new { id = employeeEntity.Id }, employeeDto);

         
        }



        //UPDATE Employee Details
        [HttpPut("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id,UpdateEmployeeDto updateemployeedto)
        {
            var employeeEntity = _dbContext.Employees.Find(id);
            if (employeeEntity == null)
            {
                return NotFound();

            }
            employeeEntity.Name = updateemployeedto.Name;
            employeeEntity.Email = updateemployeedto.Email;
            employeeEntity.Phone = updateemployeedto.Phone;
            employeeEntity.Salary = updateemployeedto.Salary;

            _dbContext.SaveChanges();

            var employeeDto = new EmployeesDto
            {
                Id = employeeEntity.Id,
                Name = updateemployeedto.Name,
                Email = updateemployeedto.Email,
                Phone = updateemployeedto.Phone
            };
            return Ok(employeeDto);
        }


        //DELETE Employee By Id
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employeeEntity = _dbContext.Employees.Find(id);
            if (employeeEntity == null)
            {
                return NotFound();
            }
            _dbContext.Employees.Remove(employeeEntity);
            _dbContext.SaveChanges();
            return Ok("Employee deleted successfully.");
        }
    }
}
