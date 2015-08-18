using System.Collections.Generic;
using System.Linq;
using Test.Transactions.Common;
using Test.Transactions.Infrastructure;

namespace Test.Transactions.Core
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionDataService _service;

        public TransactionsService(ITransactionDataService dataService)
        {
            _service = dataService;
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            var dtos = _service.GetTransactions();
            return dtos.Select(s => new Transaction(s));
        }

        public void SaveTransaction(Transaction transaction)
        {
            var dto = transaction.GetDto();
            if (dto.Id == null)
                _service.InsertTransaction(dto.Account, dto.Description, dto.CurrencyCode, dto.Amount);
            else _service.SaveTransaction(dto);
        }

        public void DeleteTransaction(int transactionId)
        {
            _service.DeleteTransaction(transactionId);
        }

        public Transaction GetTransaction(int transactionId)
        {
            var dtos = _service.GetTransaction(transactionId);
            return new Transaction(dtos);
        }

        public void DeleteAllTransactions()
        {
            _service.DeleteAllTransactions();
        }
    }
}