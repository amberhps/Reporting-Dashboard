using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ReportingDashboard.Data.Models;
using System.Data;

namespace ReportingDashboard.Data
{
    public class WarehouseContext(IOptionsSnapshot<AppSettings> settingSnapshot) : BaseContext
    {
        public override IDbConnection Connection => _connection ??= new SqlConnection(settingSnapshot.Value.Warehouse);
    }
}
