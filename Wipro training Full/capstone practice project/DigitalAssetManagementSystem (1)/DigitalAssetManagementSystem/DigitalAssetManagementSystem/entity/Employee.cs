using System.ComponentModel.DataAnnotations;

namespace DigitalAssetManagementSystem.entity
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Department { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        // Identity handles password securely. This column exists to match schema requirement.
        public string? Password { get; set; }
    }
}