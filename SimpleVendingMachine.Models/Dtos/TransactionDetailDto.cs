
namespace SimpleVendingMachine.Models.Dtos
{
    public class TransactionDetailDto
    {
        public long TransactionId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }

        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public string ImageURL { get; set; }
    }
}
