using DigitalAssetManagementSystem.entity;

namespace DigitalAssetManagementSystem.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext db)
        {
            if (!db.Employees.Any())
            {
                db.Employees.AddRange(
                    new Employee { Name = "Admin Employee", Department = "IT", Email = "admin@company.com", Password = null },
                    new Employee { Name = "John", Department = "HR", Email = "john@company.com", Password = null }
                );
                db.SaveChanges();
            }
        }
    }
}