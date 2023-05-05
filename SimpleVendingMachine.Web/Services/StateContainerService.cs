using SimpleVendingMachine.Models.Dtos;
using SimpleVendingMachine.Web.Models;
using SimpleVendingMachine.Web.Services.Contracts;

namespace SimpleVendingMachine.Web.Services
{
    public class StateContainerService : IStateContainerService
    {
        public StateContainerService() 
        {
            Products = new List<ProductDto>();
            CartItems = new List<CartItemVM>();
        }

        public List<ProductDto> Products { get; protected set; }
        public List<CartItemVM> CartItems { get; protected set; }

        public void SetProducts(IEnumerable<ProductDto> products)
        {
            Products = products.ToList();
            NotifyProductsChanged();
        }

        public void AddCartItems(CartItemVM cartItem)
        {
            var existingCartItem = CartItems.SingleOrDefault(ci => ci.ProductId == cartItem.ProductId);

            if (existingCartItem != null) 
            {
                existingCartItem.Qty += cartItem.Qty;
            }
            else
            {
                CartItems.Add(cartItem);
            }

            NotifyCartItemsChanged();
        }

        public void RemoveCartItem(int productId)
        {
            var cartItemToRemove = CartItems.SingleOrDefault(ci => ci.ProductId == productId);

            if (cartItemToRemove != null) 
            { 
                CartItems.Remove(cartItemToRemove);
                NotifyCartItemsChanged();
            }
        }

        public void ClearCartItems()
        {
            CartItems.Clear();
            NotifyCartItemsChanged();
        }

        public void UpdateCartItemQty(int productId, int qty)
        {
            var cartItemToUpdate = CartItems.SingleOrDefault(ci => ci.ProductId == productId);

            if (cartItemToUpdate != null) 
            {
                cartItemToUpdate.Qty = qty;
                NotifyCartItemsChanged();
            }
        }

        public event Action OnProductsChange;
        public event Action OnCartItemsChange;

        public void NotifyProductsChanged() => OnProductsChange?.Invoke();
        public void NotifyCartItemsChanged() => OnCartItemsChange?.Invoke();
    }
}
