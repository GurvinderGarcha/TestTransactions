namespace Test.Transactions.Common
{
    public class TransactionDto
    {
        public TransactionDto()
        {
            
        }

        public TransactionDto(int? id, string account, string description, string currencyCode, decimal amount)
        {
            Id = id;
            Account = account;
            Description = description;
            CurrencyCode = currencyCode;
            Amount = amount;
        }

        public int? Id { get; set; }

        public string Account { get; set; }

        public string Description { get; set; }

        public string CurrencyCode { get; set; }

        public decimal Amount { get; set; }
    }
}
