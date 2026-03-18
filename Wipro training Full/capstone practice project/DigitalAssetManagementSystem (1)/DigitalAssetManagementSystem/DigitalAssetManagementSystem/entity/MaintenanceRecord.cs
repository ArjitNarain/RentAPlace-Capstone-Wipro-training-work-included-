using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalAssetManagementSystem.entity
{
    public class MaintenanceRecord
    {
        [Key]
        public int MaintenanceId { get; set; }

        [Required]
        public int AssetId { get; set; }

        [ForeignKey(nameof(AssetId))]
        public Asset? Asset { get; set; }

        [Required]
        public DateTime MaintenanceDate { get; set; }

        [Required, MaxLength(300)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public double Cost { get; set; }
    }
}