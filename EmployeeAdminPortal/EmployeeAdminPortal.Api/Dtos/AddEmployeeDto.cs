using System.ComponentModel.DataAnnotations;

namespace EmployeeAdminPortal.Api.Dtos
{
    public class AddEmployeeDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public decimal Salary { get; set; }
    }
}
