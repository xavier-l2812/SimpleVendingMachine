using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleVendingMachine.Api.Entities
{
    public class TransactionDetail
    {
        public long Id { get; set; }
        public long TransactionId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
