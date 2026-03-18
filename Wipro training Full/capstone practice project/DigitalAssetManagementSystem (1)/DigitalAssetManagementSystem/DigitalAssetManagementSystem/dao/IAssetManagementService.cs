using DigitalAssetManagementSystem.entity;

namespace DigitalAssetManagementSystem.dao
{
    public interface IAssetManagementService
    {
        Task<bool> AddAssetAsync(Asset asset);
        Task<bool> UpdateAssetAsync(Asset asset);
        Task<bool> DeleteAssetAsync(int assetId);

        Task<bool> AllocateAssetAsync(int assetId, int employeeId, DateTime allocationDate);
        Task<bool> DeallocateAssetAsync(int assetId, int employeeId, DateTime returnDate);

        Task<bool> PerformMaintenanceAsync(int assetId, DateTime maintenanceDate, string description, double cost);

        Task<bool> ReserveAssetAsync(int assetId, int employeeId, DateTime reservationDate, DateTime startDate, DateTime endDate);
        Task<bool> WithdrawReservationAsync(int reservationId);

        // Helpful for UI
        Task<List<Asset>> GetAssetsAsync();
        Task<Asset?> GetAssetByIdAsync(int assetId);
        Task<List<Employee>> GetEmployeesAsync();
        Task<List<MaintenanceRecord>> GetMaintenanceAsync(int assetId);
        Task<List<AssetAllocation>> GetAllocationsAsync();
        Task<List<Reservation>> GetReservationsAsync();
    }
}