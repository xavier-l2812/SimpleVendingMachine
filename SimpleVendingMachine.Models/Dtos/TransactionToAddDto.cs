
namespace SimpleVendingMachine.Models.Dtos
{
    public class TransactionToAddDto
    {
        public int TransactionTypeId { get; set; }
        public AccountDto Account { get; set; }
        public List<TransactionDetailToAddDto> TransactionDetailToAddDtos { get; set; } = new List<TransactionDetailToAddDto>();
        public long? RelatedTransactionId { get; set; } = null;
        public decimal TotalPrice { get; set; }

        public int TotalQuantity
        {
            get
            {
                var result = TransactionDetailToAddDtos.Sum(td => td.Qty);

                return result;
            }
        }
    }
}
