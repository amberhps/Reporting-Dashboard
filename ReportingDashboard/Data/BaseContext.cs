using Microsoft.Data.SqlClient;
using System.Data;

namespace ReportingDashboard.Data
{
    public abstract class BaseContext : IDisposable
    {
        protected SqlConnection? _connection;
        private IDbTransaction? _transaction;

        private bool _disposed = false;

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

        ~BaseContext()
        {
            InternalDispose(false);
        }
    }
}
