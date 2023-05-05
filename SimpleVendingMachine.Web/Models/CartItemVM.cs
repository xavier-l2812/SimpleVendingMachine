namespace SimpleVendingMachine.Web.Models
{
    public class CartItemVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImageURL { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; } = 0;

        public decimal TotalPrice 
        {
            get
            {
                return Price * Qty;
            }
        }
    }
}
