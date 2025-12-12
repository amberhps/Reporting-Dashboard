using Microsoft.Data.SqlClient;
using System.Data;

namespace ReportingDashboard.Data.Common
{
    public abstract class BaseContext : IDisposable
    {
        protected SqlConnection? _connection;
        private IDbTransaction? _transaction;

        public abstract IDbConnection Connection { get; }
        public IDbTransaction? Transaction => _transaction;
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

        public virtual void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();

            _transaction = null;
            _connection = null;

            GC.SuppressFinalize(this);
        }
    }
}
