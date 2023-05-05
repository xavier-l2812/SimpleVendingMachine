using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleVendingMachine.Api.Entities
{
    public class Transaction
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        public int TransactionTypeId { get; set; }
        public int AccountId { get; set; }
        public long? RelatedTransactionId { get; set; }

        [ForeignKey("TransactionTypeId")]
        public TransactionType TransactonType { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
