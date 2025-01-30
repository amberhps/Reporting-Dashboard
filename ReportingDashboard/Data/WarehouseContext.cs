using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ReportingDashboard.Data.Models;
using System.Data;

namespace ReportingDashboard.Data
{
    public class WarehouseContext(IOptions<AppSettings> settingSnapshot) : IDisposable
    {
        private readonly AppSettings _settings = settingSnapshot.Value;

        private SqlConnection? _connection;
        private IDbTransaction? _transaction;

        private bool _disposed = false;
        public IDbConnection Connection => _connection ??= new SqlConnection(
            Environment == WarehouseEnvironment.Test ?
            _settings.WarehouseTest :
            _settings.WarehouseProd);

        public IDbTransaction? Transaction => _transaction;

        public WarehouseEnvironment Environment { get; private set; } = WarehouseEnvironment.Test;

        private readonly Lock _lock = new();

        public IDbTransaction BeginTransaction() => _transaction ??= Connection.BeginTransaction();

        public void Commit()
        {
            _transaction?.Commit();
            _transaction?.Dispose();
            _transaction = null;
        }

        public void RollBack()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

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
        public virtual void Dispose()
        {
            InternalDispose(true);
            GC.SuppressFinalize(this);
        }

        protected void InternalDispose(bool disposing)
        {
            if (_disposed)
                return;

            if (!disposing)
            {
                _disposed = true;
                return;
            }

            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }

            _disposed = true;
        }

        ~WarehouseContext()
        {
            InternalDispose(false);
        }
    }
}
