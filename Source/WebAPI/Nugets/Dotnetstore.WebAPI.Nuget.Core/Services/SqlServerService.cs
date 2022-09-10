using Dotnetstore.WebAPI.Nuget.Core.Interfaces;
using Microsoft.Data.SqlClient;

namespace Dotnetstore.WebAPI.Nuget.Core.Services;

public sealed class SqlServerService : ISqlServerService
{
    string ISqlServerService.BuildConnectionString(string dataSource, string initialCatalog, string applicationName,
        bool integratedSecurity, bool multipleActiveResultSets, string userID, string password)
    {
        var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = dataSource,
            InitialCatalog = initialCatalog,
            IntegratedSecurity = integratedSecurity,
            MultipleActiveResultSets = multipleActiveResultSets
        };

        if (!string.IsNullOrWhiteSpace(applicationName))
            sqlConnectionStringBuilder.ApplicationName = applicationName;

        if (!integratedSecurity &&
            (!string.IsNullOrWhiteSpace(userID) ||
             !string.IsNullOrWhiteSpace(password)))
        {
            sqlConnectionStringBuilder.UserID = userID;
            sqlConnectionStringBuilder.Password = password;
        }
        else
        {
            sqlConnectionStringBuilder.IntegratedSecurity = true;
        }

        return sqlConnectionStringBuilder.ConnectionString;
    }

    SqlConnectionStringBuilder ISqlServerService.ConvertToSqlConnectionStringBuilder(string connectionString)
    {
        try
        {
            return new SqlConnectionStringBuilder(connectionString);
        }
        catch
        {
            return new SqlConnectionStringBuilder();
        }
    }
}