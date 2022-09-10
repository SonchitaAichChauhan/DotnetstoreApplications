using Microsoft.Data.SqlClient;

namespace Dotnetstore.WebAPI.Nuget.Core.Interfaces;

public interface ISqlServerService
{
    string BuildConnectionString(string dataSource, string initialCatalog, string applicationName = "",
        bool integratedSecurity = true, bool multipleActiveResultSets = true, string userID = "", string password = "");

    SqlConnectionStringBuilder ConvertToSqlConnectionStringBuilder(string connectionString);
}