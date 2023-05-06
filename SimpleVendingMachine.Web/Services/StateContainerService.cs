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
        public TransactionDto SelectedTransaction { get; protected set; }

        public decimal TotalPrice 
        {
            get
            {
                return CartItems.Sum(ci => ci.TotalPrice);
            }
        }
        public int TotalQuantity 
        {
            get
            {
                return CartItems.Sum(p => p.Qty);
            }
        }

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
        }

        public void RemoveCartItem(int productId)
        {
            var cartItemToRemove = CartItems.SingleOrDefault(ci => ci.ProductId == productId);

            if (cartItemToRemove != null) 
            { 
                CartItems.Remove(cartItemToRemove);
            }
        }

        public void ClearCartItems()
        {
            CartItems.Clear();
        }

        public void UpdateCartItemQty(int productId, int qty)
        {
            var cartItemToUpdate = CartItems.SingleOrDefault(ci => ci.ProductId == productId);

            if (cartItemToUpdate != null) 
            {
                cartItemToUpdate.Qty = qty;
            }
        }

        public void SetSelectedTransaction(TransactionDto transactionDto)
        {
            SelectedTransaction = transactionDto;
            NotifySelectedTransactionChanged();
        }

        public void ClearSelectedTransaction()
        {
            SelectedTransaction = null;
            NotifySelectedTransactionChanged();
        }

        public event Action OnProductsChange;
        public void NotifyProductsChanged() => OnProductsChange?.Invoke();

        public event Action OnSelectedTransactionChange;
        public void NotifySelectedTransactionChanged() => OnSelectedTransactionChange?.Invoke();
    }
}
