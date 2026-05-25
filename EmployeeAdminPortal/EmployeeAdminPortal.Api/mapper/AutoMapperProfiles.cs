using AutoMapper;
using EmployeeAdminPortal.Api.Dtos;
using EmployeeAdminPortal.Api.Models.Entities;
using System.Runtime.InteropServices;

namespace EmployeeAdminPortal.Api.mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Employee, EmployeesDto>().ReverseMap();
            CreateMap<Employee, AddEmployeeDto>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeDto>().ReverseMap();
        }
    }
}
