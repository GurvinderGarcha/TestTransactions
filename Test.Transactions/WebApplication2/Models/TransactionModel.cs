using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class TransactionModel
    {
        public TransactionModel()
        {
            
        }

        public int Id { get; set; }

        [Required]
        public string Account { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}