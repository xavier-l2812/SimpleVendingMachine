using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleVendingMachine.Api.Entities
{
    public class Transaction
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Amount { get; set; }
        public int TransactionTypeId { get; set; }
        public int AccountId { get; set; }

        [ForeignKey("TransactionTypeId")]
        public TransactonType TransactonType { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
