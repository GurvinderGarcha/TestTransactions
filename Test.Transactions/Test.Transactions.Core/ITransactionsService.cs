using System;
using System.Collections.Generic;
using Test.Transactions.Common;

namespace Test.Transactions.Core
{
    public interface ITransactionsService
    {
        IEnumerable<Transaction> GetTransactions();
        void SaveTransaction(Transaction transaction);
        void DeleteTransaction(int transactionId);
        Transaction GetTransaction(int transactionId);
        void DeleteAllTransactions();
    }
}