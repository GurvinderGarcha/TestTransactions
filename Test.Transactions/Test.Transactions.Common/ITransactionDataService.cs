using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Transactions.Common
{

    public interface ITransactionDataService
    {
        TransactionDto GetTransaction(int transactionId);
        List<TransactionDto> GetTransactions();
        void SaveTransaction(TransactionDto dto);
        int InsertTransaction(string account, string description, string currencyCode, decimal amount);
        void DeleteTransaction(int transactionId);
    }

    public class TransactionDataService : ITransactionDataService
    {
        private string _connectionString;
        
        public TransactionDataService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["TransactionsDb"].ConnectionString;
        }

        public TransactionDto GetTransaction(int transactionId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("select id, account, description, currencyCode, amount from Transactions", conn))
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
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("select id, account, description, currencyCode, amount from Transactions", conn))
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
           using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = string.Format(@"
if exists(select null from Transactions where id = {0}) 
begin
    update transactions
    set Account = '{1}',description = '{2}', currencyCode = '{3}', amount = '{4}'
    where id = {0}
end
", dto.Id);
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int InsertTransaction(string account, string description, string currencyCode, decimal amount)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = string.Format(@"
insert into transactions(account, description, currencyCode, amount)values('{0}','{1}','{2}','{3}')

select scope_identity
", account,
                                          description, currencyCode, amount);

                using (var cmd = new SqlCommand(query, conn))
                {
                    var id = cmd.ExecuteScalar();
                    return (int) id;
                }
            }
        }

        public void DeleteTransaction(int transactionId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var query = string.Format(@"
delete from transactions where id = {0}
", transactionId);
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
