using DigitalAssetManagementSystem.Data;
using DigitalAssetManagementSystem.entity;
using DigitalAssetManagementSystem.exception;
using Microsoft.EntityFrameworkCore;

namespace DigitalAssetManagementSystem.dao
{
    public class AssetManagementServiceImpl : IAssetManagementService
    {
        private readonly ApplicationDbContext _db;

        public AssetManagementServiceImpl(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddAssetAsync(Asset asset)
        {
            _db.Assets.Add(asset);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAssetAsync(Asset asset)
        {
            var existing = await _db.Assets.FindAsync(asset.AssetId);
            if (existing == null)
                throw new AssetNotFoundException($"Asset with ID {asset.AssetId} not found.");

            existing.Name = asset.Name;
            existing.Type = asset.Type;
            existing.SerialNumber = asset.SerialNumber;
            existing.PurchaseDate = asset.PurchaseDate;
            existing.Location = asset.Location;
            existing.Status = asset.Status;
            existing.OwnerId = asset.OwnerId;

            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAssetAsync(int assetId)
        {
            var asset = await _db.Assets.FindAsync(assetId);
            if (asset == null)
                throw new AssetNotFoundException($"Asset with ID {assetId} not found.");

            _db.Assets.Remove(asset);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> AllocateAssetAsync(int assetId, int employeeId, DateTime allocationDate)
        {
            var asset = await _db.Assets.FindAsync(assetId);
            if (asset == null)
                throw new AssetNotFoundException($"Asset with ID {assetId} not found.");

            var emp = await _db.Employees.FindAsync(employeeId);
            if (emp == null)
                throw new Exception($"Employee with ID {employeeId} not found.");

            // Business: can't allocate if under maintenance
            if (asset.Status.Equals("under maintenance", StringComparison.OrdinalIgnoreCase))
                throw new Exception("Asset is under maintenance; cannot allocate.");

            // Business: must be maintained in last 2 years
            var lastMaint = await _db.MaintenanceRecords
                .Where(m => m.AssetId == assetId)
                .OrderByDescending(m => m.MaintenanceDate)
                .FirstOrDefaultAsync();

            if (lastMaint == null || lastMaint.MaintenanceDate < DateTime.Today.AddYears(-2))
                throw new AssetNotMaintainException("Asset not maintained within last 2 years.");

            // Check active allocation
            bool alreadyAllocated = await _db.AssetAllocations
                .AnyAsync(a => a.AssetId == assetId && a.ReturnDate == null);

            if (alreadyAllocated)
                throw new Exception("Asset is already allocated to someone.");

            _db.AssetAllocations.Add(new AssetAllocation
            {
                AssetId = assetId,
                EmployeeId = employeeId,
                AllocationDate = allocationDate,
                ReturnDate = null
            });

            asset.Status = "in use";
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeallocateAssetAsync(int assetId, int employeeId, DateTime returnDate)
        {
            var allocation = await _db.AssetAllocations
                .Where(a => a.AssetId == assetId && a.EmployeeId == employeeId && a.ReturnDate == null)
                .FirstOrDefaultAsync();

            if (allocation == null)
                throw new Exception("Active allocation not found for given asset and employee.");

            allocation.ReturnDate = returnDate;

            var asset = await _db.Assets.FindAsync(assetId);
            if (asset != null)
                asset.Status = "in use";

            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> PerformMaintenanceAsync(int assetId, DateTime maintenanceDate, string description, double cost)
        {
            var asset = await _db.Assets.FindAsync(assetId);
            if (asset == null)
                throw new AssetNotFoundException($"Asset with ID {assetId} not found.");

            _db.MaintenanceRecords.Add(new MaintenanceRecord
            {
                AssetId = assetId,
                MaintenanceDate = maintenanceDate,
                Description = description,
                Cost = cost
            });

            asset.Status = "under maintenance";
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> ReserveAssetAsync(int assetId, int employeeId, DateTime reservationDate, DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new Exception("End date must be >= start date.");

            var asset = await _db.Assets.FindAsync(assetId);
            if (asset == null)
                throw new AssetNotFoundException($"Asset with ID {assetId} not found.");

            var emp = await _db.Employees.FindAsync(employeeId);
            if (emp == null)
                throw new Exception($"Employee with ID {employeeId} not found.");

            // conflicts
            bool conflict = await _db.Reservations.AnyAsync(r =>
                r.AssetId == assetId &&
                r.Status != "canceled" &&
                !(endDate < r.StartDate || startDate > r.EndDate));

            if (conflict)
                throw new Exception("Reservation conflicts with an existing reservation.");

            _db.Reservations.Add(new Reservation
            {
                AssetId = assetId,
                EmployeeId = employeeId,
                ReservationDate = reservationDate,
                StartDate = startDate,
                EndDate = endDate,
                Status = "pending"
            });

            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> WithdrawReservationAsync(int reservationId)
        {
            var r = await _db.Reservations.FindAsync(reservationId);
            if (r == null)
                throw new Exception("Reservation not found.");

            r.Status = "canceled";
            return await _db.SaveChangesAsync() > 0;
        }

        public Task<List<Asset>> GetAssetsAsync()
            => _db.Assets.Include(a => a.Owner).OrderByDescending(a => a.AssetId).ToListAsync();

        public Task<Asset?> GetAssetByIdAsync(int assetId)
            => _db.Assets.Include(a => a.Owner).FirstOrDefaultAsync(a => a.AssetId == assetId);

        public Task<List<Employee>> GetEmployeesAsync()
            => _db.Employees.OrderBy(e => e.EmployeeId).ToListAsync();

        public Task<List<MaintenanceRecord>> GetMaintenanceAsync(int assetId)
            => _db.MaintenanceRecords.Where(m => m.AssetId == assetId).OrderByDescending(m => m.MaintenanceDate).ToListAsync();

        public Task<List<AssetAllocation>> GetAllocationsAsync()
            => _db.AssetAllocations.Include(a => a.Asset).Include(a => a.Employee)
                .OrderByDescending(a => a.AllocationId).ToListAsync();

        public Task<List<Reservation>> GetReservationsAsync()
            => _db.Reservations.Include(r => r.Asset).Include(r => r.Employee)
                .OrderByDescending(r => r.ReservationId).ToListAsync();
    }
}