using Microsoft.AspNetCore.Mvc;
using EmployeeAdminPortal.Api.Data;
using EmployeeAdminPortal.Api.Models.Entities;
using EmployeeAdminPortal.Api.Dtos;
using Microsoft.EntityFrameworkCore;
using EmployeeAdminPortal.Api.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;


namespace EmployeeAdminPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeesController : ControllerBase
    {

        //private readonly ApplicationDbContext _dbContext;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper mapper;


        public EmployeesController(IEmployeeRepository employeeRepository,IMapper mapper)
        {
            //_dbContext = dbcontext;
            _employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        //Get method
        [HttpGet] 
        public async Task<IActionResult> GetAllEmployeesASync([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize=10)
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);

            var employeesdto=new List<EmployeesDto>();


            //foreach(var employee in employees)
            //{
            //    var employeeDto = new EmployeesDto
            //    {
            //        Id = employee.Id,
            //        Name = employee.Name,
            //        Email = employee.Email,
            //        Phone = employee.Phone
            //    };
            //    employeesdto.Add(employeeDto);
            //}

            //Auto mapping
            employeesdto = mapper.Map<List<EmployeesDto>>(employees);
            return Ok(employeesdto);

        }


        //Get Employee By Id
        [HttpGet("{id:guid}")] 
        public async Task<IActionResult> GetEmployeeByIdAsync(Guid id)
        {
            var employeeEntity = await _employeeRepository.GetEmployeeByIdAsync(id);
    
            if(employeeEntity == null)
            {
                return NotFound();
            }

            //var employeeDto = new EmployeesDto
            //{
            //    Id = employeeEntity.Id,
            //    Name = employeeEntity.Name,
            //    Email = employeeEntity.Email,
            //    Phone = employeeEntity.Phone
            //};

            //Auto mapping
            var employeeDto = mapper.Map<EmployeesDto>(employeeEntity);

            return Ok(employeeDto);
        }


        //Post Method
        [HttpPost] 
        public async Task<IActionResult> AddEmployeeAsync(AddEmployeeDto addEmployeeDto)
        {
            //Check if the model state is valid
            if (ModelState.IsValid)
            {
                var employeeEntity = await _employeeRepository.AddEmployeeAsync(addEmployeeDto);


                //var employeeDto = new EmployeesDto
                //{
                //    Id = employeeEntity.Id,
                //    Name = employeeEntity.Name,
                //    Email = employeeEntity.Email,
                //    Phone = employeeEntity.Phone
                //};

                //Auto mapping
                var employeeDto = mapper.Map<EmployeesDto>(employeeEntity);

                return Ok(employeeDto);
            }
            return BadRequest(ModelState);



        }



        //Update/Put Method
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateemployeedto)
        {
            //Check if the model state is valid
            if (ModelState.IsValid) {
                var employeeEntity = await _employeeRepository.UpdateEmployeeAsync(id, updateemployeedto);

                if (employeeEntity == null)
                {
                    return NotFound();

                }

                //var employeeDto = new EmployeesDto
                //{
                //    Id = employeeEntity.Id,
                //    Name = employeeEntity.Name,
                //    Email = employeeEntity.Email,
                //    Phone = employeeEntity.Phone
                //};

                //Auto mapping
                var employeedto = mapper.Map<EmployeesDto>(employeeEntity);
                return Ok(employeedto);
            }
            return BadRequest(ModelState);

        }


        //Delete Method
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid id)
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
