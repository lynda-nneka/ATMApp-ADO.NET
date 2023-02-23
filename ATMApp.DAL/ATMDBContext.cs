using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ATMApp.DAL
{
    public class ATMDBContext : IDisposable
    {
        private readonly string _connString;

        private bool _disposed;

        private SqlConnection _dbConnection = null;

        public ATMDBContext() : this(@"Data Source=localhost,1433;User Id=SA;Password=Strong.Pwd-123;Initial Catalog=LynnAtmDB;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {

        }

        public ATMDBContext(string connString)
        {
            _connString = connString;
        }

        public async Task<SqlConnection> OpenConnection()
        {
            _dbConnection = new SqlConnection(_connString);
            await _dbConnection.OpenAsync();
            return _dbConnection;
        }

        public async Task CloseConnection()
        {
            if (_dbConnection?.State != ConnectionState.Closed)
            {
                await _dbConnection?.CloseAsync();
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dbConnection.Dispose();
            }

            _disposed = true;
        }
        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}


