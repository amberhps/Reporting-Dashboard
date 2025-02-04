using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ReportingDashboard.Data.Models;
using System.Data;

namespace ReportingDashboard.Data
{
    public class WarehouseContext(IOptions<AppSettings> settingSnapshot) : BaseContext
    {
        private readonly AppSettings _settings = settingSnapshot.Value;

        private readonly Lock _lock = new();

        public override IDbConnection Connection => _connection ??= new SqlConnection(
            Environment == WarehouseEnvironment.Test ?
            _settings.WarehouseTest :
            _settings.WarehouseProd);

        public WarehouseEnvironment Environment { get; private set; } = WarehouseEnvironment.Test;

        public void SwitchEnvironment()
        {
            lock(_lock) 
            {
                if (_connection != null)
                {
                    RollBack();
                    _connection.Dispose();
                    _connection = null;
                }

                Environment = Environment == WarehouseEnvironment.Test ? WarehouseEnvironment.Prod : WarehouseEnvironment.Test;
            }
        }
    }
}
