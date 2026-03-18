using DigitalAssetManagementSystem.dao;
using DigitalAssetManagementSystem.Data;
using DigitalAssetManagementSystem.entity;
using DigitalAssetManagementSystem.exception;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DigitalAssetManagementSystem.Tests
{
    public class AssetManagementServiceTests
    {
        private static ApplicationDbContext CreateDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var db = new ApplicationDbContext(options);

            db.Employees.Add(new Employee { EmployeeId = 1, Name = "Test Emp", Department = "IT", Email = "t@t.com" });
            db.SaveChanges();
            return db;
        }

        [Fact]
        public async Task Asset_Created_Successfully()
        {
            var db = CreateDb();
            IAssetManagementService svc = new AssetManagementServiceImpl(db);

            var asset = new Asset
            {
                Name = "Dell Laptop",
                Type = "laptop",
                SerialNumber = "SN-001",
                PurchaseDate = DateTime.Today,
                Location = "Bangalore",
                Status = "in use",
                OwnerId = 1
            };

            var ok = await svc.AddAssetAsync(asset);
            Assert.True(ok);
            Assert.Equal(1, db.Assets.Count());
        }

        [Fact]
        public async Task Maintenance_Added_Successfully()
        {
            var db = CreateDb();
            IAssetManagementService svc = new AssetManagementServiceImpl(db);

            db.Assets.Add(new Asset
            {
                AssetId = 10,
                Name = "HP Printer",
                Type = "equipment",
                SerialNumber = "SN-PR-10",
                PurchaseDate = DateTime.Today.AddYears(-1),
                Location = "Office",
                Status = "in use"
            });
            db.SaveChanges();

            var ok = await svc.PerformMaintenanceAsync(10, DateTime.Today, "General service", 500);
            Assert.True(ok);
            Assert.Single(db.MaintenanceRecords);
        }

        [Fact]
        public async Task Reservation_Created_Successfully()
        {
            var db = CreateDb();
            IAssetManagementService svc = new AssetManagementServiceImpl(db);

            db.Assets.Add(new Asset
            {
                AssetId = 20,
                Name = "Projector",
                Type = "equipment",
                SerialNumber = "SN-PRO-20",
                PurchaseDate = DateTime.Today.AddYears(-1),
                Location = "Meeting room",
                Status = "in use"
            });
            db.SaveChanges();

            var ok = await svc.ReserveAssetAsync(
                assetId: 20,
                employeeId: 1,
                reservationDate: DateTime.Today,
                startDate: DateTime.Today.AddDays(1),
                endDate: DateTime.Today.AddDays(2));

            Assert.True(ok);
            Assert.Single(db.Reservations);
        }

        [Fact]
        public async Task Throws_AssetNotFoundException_When_Asset_Not_Found()
        {
            var db = CreateDb();
            IAssetManagementService svc = new AssetManagementServiceImpl(db);

            await Assert.ThrowsAsync<AssetNotFoundException>(async () =>
                await svc.PerformMaintenanceAsync(999, DateTime.Today, "Test", 100));
        }
    }
}