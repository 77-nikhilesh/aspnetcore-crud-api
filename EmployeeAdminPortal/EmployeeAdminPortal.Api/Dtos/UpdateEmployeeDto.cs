using System.ComponentModel.DataAnnotations;

namespace EmployeeAdminPortal.Api.Dtos
{
    public class UpdateEmployeeDto
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
        [StringLength(50)]
        public decimal Salary { get; set; }
    }
}
