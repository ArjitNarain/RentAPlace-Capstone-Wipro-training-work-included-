using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalAssetManagementSystem.entity
{
    public class Asset
    {
        [Key]
        public int AssetId { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(60)]
        public string Type { get; set; } = string.Empty; // laptop/vehicle/equipment

        [Required, MaxLength(120)]
        public string SerialNumber { get; set; } = string.Empty;

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required, MaxLength(120)]
        public string Location { get; set; } = string.Empty;

        [Required, MaxLength(60)]
        public string Status { get; set; } = "in use"; // in use/decommissioned/under maintenance

        // FK: owner employee
        public int? OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public Employee? Owner { get; set; }
    }
}