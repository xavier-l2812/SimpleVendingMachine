
namespace SimpleVendingMachine.Models.Dtos
{
    public class TransactionDto
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        public long? RelatedTransactionId { get; set; }

        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string CardNumber { get; set; }

        public int TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; }
    }
}
