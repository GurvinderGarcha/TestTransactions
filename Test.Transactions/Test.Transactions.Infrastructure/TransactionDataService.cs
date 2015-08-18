using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Test.Transactions.Common;

namespace Test.Transactions.Infrastructure
{
    public class TransactionDataService : ITransactionDataService
    {
        private readonly string _connectionString;
        private readonly IDbConnectionProvider _connectionProvider;
        
        public TransactionDataService(IDbConnectionProvider connectionProvider)
        {
            //_connectionString = ConfigurationManager.ConnectionStrings["TransactionDb"].ConnectionString;
            _connectionProvider = connectionProvider;
        }

        public TransactionDto GetTransaction(int transactionId)
        {
            var query = string.Format("select id, account, description, currencyCode, amount from TransactionData where id = {0}", transactionId);
            using (var conn = _connectionProvider.OpenConnection())
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TransactionDto(
                                id: reader.GetInt32(0),
                                account:reader.GetString(1),
                                description:reader.GetString(2),
                                currencyCode:reader.GetString(3),
                                amount:reader.GetDecimal(4)
                                );
                        }
                    }
                }
            }

            return null;
        }

        public List<TransactionDto> GetTransactions()
        {
            var transactions = new List<TransactionDto>();
            using (var conn = _connectionProvider.OpenConnection())
            {
                using (var cmd = new SqlCommand("select id, account, description, currencyCode, amount from TransactionData", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transactions.Add(new TransactionDto(
                                id: reader.GetInt32(0),
                                account:reader.GetString(1),
                                description:reader.GetString(2),
                                currencyCode:reader.GetString(3),
                                amount:reader.GetDecimal(4)
                                ));
                        }
                    }
                }
            }

            return transactions;
        }

        public void SaveTransaction(TransactionDto dto)
        {
            using (var conn = _connectionProvider.OpenConnection())
            {
                var query = string.Format(@"
if exists(select null from TransactionData where id = {0}) 
begin
    update TransactionData
    set Account = '{1}',description = '{2}', currencyCode = '{3}', amount = '{4}'
    where id = {0}
end
", dto.Id, dto.Account,dto.Description,dto.CurrencyCode,dto.Amount);
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertTransaction(string account, string description, string currencyCode, decimal amount)
        {
            var query = string.Format(@"insert into TransactionData(account, description, currencyCode, amount)values('{0}','{1}','{2}','{3}')", account, description, currencyCode, amount);
            using (var conn = _connectionProvider.OpenConnection())
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTransaction(int transactionId)
        {
            var query = string.Format(@"delete from TransactionData where id = {0}", transactionId);
            using (var conn = _connectionProvider.OpenConnection())
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAllTransactions()
        {
            var query = string.Format(@"delete from TransactionData");
            using (var conn = _connectionProvider.OpenConnection())
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}