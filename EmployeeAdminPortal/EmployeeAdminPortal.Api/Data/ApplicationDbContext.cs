using EmployeeAdminPortal.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Api.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
