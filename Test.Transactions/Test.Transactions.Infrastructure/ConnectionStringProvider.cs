using System.Configuration;

namespace Test.Transactions.Infrastructure
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private string _connectionString = null;

        public string GetConnectionString()
        {
            return _connectionString ??
                   (_connectionString = ConfigurationManager.ConnectionStrings["TransactionDb"].ConnectionString);
        }
    }
}