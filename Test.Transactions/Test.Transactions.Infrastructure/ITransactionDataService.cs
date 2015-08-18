using System.Collections.Generic;
using Test.Transactions.Common;

namespace Test.Transactions.Infrastructure
{
    public interface ITransactionDataService
    {
        TransactionDto GetTransaction(int transactionId);
        List<TransactionDto> GetTransactions();
        void SaveTransaction(TransactionDto dto);
        void InsertTransaction(string account, string description, string currencyCode, decimal amount);
        void DeleteTransaction(int transactionId);
        void DeleteAllTransactions();
    }
}
