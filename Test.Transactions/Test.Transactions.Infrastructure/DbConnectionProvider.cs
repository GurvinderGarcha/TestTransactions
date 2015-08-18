using System.Data;
using System.Data.SqlClient;

namespace Test.Transactions.Infrastructure
{
    public class DbConnectionProvider : IDbConnectionProvider
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public DbConnectionProvider(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public SqlConnection OpenConnection()
        {
            var conn = new SqlConnection(_connectionStringProvider.GetConnectionString());
            conn.Open();
            return conn;
        }
    }
}