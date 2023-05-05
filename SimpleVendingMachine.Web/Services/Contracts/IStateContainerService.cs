using SimpleVendingMachine.Models.Dtos;
using SimpleVendingMachine.Web.Models;

namespace SimpleVendingMachine.Web.Services.Contracts
{
    public interface IStateContainerService
    {
        public List<ProductDto> Products { get; }
        public void SetProducts(IEnumerable<ProductDto> products);
        public event Action OnProductsChange;

        public List<CartItemVM> CartItems { get; }
        public decimal TotalPrice { get; }
        public int TotalQuantity { get; }
        public void AddCartItems(CartItemVM cartItem);
        public void RemoveCartItem(int productId);
        public void ClearCartItems();
        public void UpdateCartItemQty(int productId, int qty);
    }
}
