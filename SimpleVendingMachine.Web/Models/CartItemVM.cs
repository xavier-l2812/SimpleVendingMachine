namespace SimpleVendingMachine.Web.Models
{
    public class CartItemVM
    {
        private int quantity = 0;

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImageURL { get; set; }
        public decimal Price { get; set; }

        public int Qty
        {
            get
            {
                return quantity;
            }
            set
            {
                if (value > 0)
                {
                    quantity = value;
                }
            }
        }

        public decimal TotalPrice 
        {
            get
            {
                return Price * Qty;
            }
        }
    }
}
