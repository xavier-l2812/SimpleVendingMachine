using Microsoft.AspNetCore.Components;
using SimpleVendingMachine.Models.Dtos;
using SimpleVendingMachine.Web.Models;
using SimpleVendingMachine.Web.Services.Contracts;

namespace SimpleVendingMachine.Web.Pages
{
    public class ProductDetailBase : ComponentBase, IDisposable
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IStateContainerService StateContainerService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ProductDto Product { get; set; }
        public string ErrorMessage { get; set; }

        protected override void OnInitialized()
        {
            try
            {
                var product = StateContainerService.Products
                                               .Where(p => p.Id == Id)
                                               .FirstOrDefault();
                if (product == null)
                {
                    NavigationManager.NavigateTo("/");
                }

                Product = product;

                StateContainerService.OnProductsChange += StateHasChanged;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected void AddToCart()
        {
            try
            {
                var cartItemVm = new CartItemVM
                {
                    ProductId = Product.Id,
                    ProductName = Product.Name,
                    ProductDescription = Product.Description,
                    ProductImageURL = Product.ImageURL,
                    Price = Product.Price,
                    Qty = 1
                };
                StateContainerService.AddCartItems(cartItemVm);

                NavigationManager.NavigateTo("/ShoppingCart");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void Dispose()
        {
            StateContainerService.OnProductsChange -= StateHasChanged;
        }
    }
}
