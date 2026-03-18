using Microsoft.Extensions.Configuration;

namespace DigitalAssetManagementSystem.util
{
    public static class DBPropertyUtil
    {
        public static string GetConnectionString(string configFileName, string connectionName = "DefaultConnection")
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configFileName, optional: false, reloadOnChange: true);

            var config = builder.Build();
            return config.GetConnectionString(connectionName)
                   ?? throw new InvalidOperationException("Connection string not found.");
        }
    }
}