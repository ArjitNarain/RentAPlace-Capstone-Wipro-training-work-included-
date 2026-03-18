using Microsoft.Data.SqlClient;

namespace DigitalAssetManagementSystem.util
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection(string configFileName)
        {
            string cs = DBPropertyUtil.GetConnectionString(configFileName);
            return new SqlConnection(cs);
        }
    }
}