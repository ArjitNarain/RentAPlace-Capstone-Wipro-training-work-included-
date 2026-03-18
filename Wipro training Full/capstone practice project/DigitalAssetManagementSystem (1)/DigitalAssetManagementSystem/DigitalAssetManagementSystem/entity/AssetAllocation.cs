using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalAssetManagementSystem.entity
{
    public class AssetAllocation
    {
        [Key]
        public int AllocationId { get; set; }

        [Required]
        public int AssetId { get; set; }

        [ForeignKey(nameof(AssetId))]
        public Asset? Asset { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee { get; set; }

        [Required]
        public DateTime AllocationDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}