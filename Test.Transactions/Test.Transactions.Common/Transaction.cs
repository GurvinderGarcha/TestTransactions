namespace Test.Transactions.Common
{
    public class Transaction
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

        public decimal? Amount { get { return _dto.Amount; } }

        public TransactionDto GetDto()
        {
            return _dto;
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Account) || string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(CurrencyCode) ||
                Amount == null) return false;
            return true;
        }
    }
}
