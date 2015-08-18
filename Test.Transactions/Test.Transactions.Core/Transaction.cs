using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Test.Transactions.Common;
using Test.Transactions.Infrastructure;

namespace Test.Transactions.Core
{
    public interface ITransaction
    {
        int? Id { get; }
        string Account { get; }
        string Description { get; }
        string CurrencyCode { get; }
        decimal Amount { get; }
        void Save();
    }

    public class Transaction : ITransaction
    {
        private readonly TransactionDto _dto;

        public Transaction(TransactionDto dto)
        {
            _dto = dto;
        }

        public int? Id { get { return _dto.Id; }  }

        public string Account { get { return _dto.Account; }  }

        public string Description { get { return _dto.Description; }  }

        public string CurrencyCode { get { return _dto.CurrencyCode; }  }

        public decimal Amount { get { return _dto.Amount; } }

        public void Save()
        {

        }

        public TransactionDto GetDto()
        {
            return _dto;
        }
    }
}
