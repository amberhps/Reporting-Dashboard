using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace ReportingDashboard.Data.Warehouse
{
    public class WarehouseDashboardContext(IOptionsSnapshot<AppSettings> settingSnapshot) : BaseContext
    {
        public override IDbConnection Connection 
        {
            get 
            {
                _connection ??= new SqlConnection(settingSnapshot.Value.Warehouse);

                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                return _connection;
            }
        }
    }
}
