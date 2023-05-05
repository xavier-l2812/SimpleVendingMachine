using Microsoft.AspNetCore.Components;
using SimpleVendingMachine.Models.Dtos;
using SimpleVendingMachine.Web.Services.Contracts;

namespace SimpleVendingMachine.Web.Pages
{
    public class ProductsBase : ComponentBase, IDisposable
    {
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IStateContainerService StateContainerService { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var products = await ProductService.GetItems();
                StateContainerService.SetProducts(products);
                StateContainerService.OnProductsChange += StateHasChanged;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory()
        {
            return from product in StateContainerService.Products
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup;
        }
        protected string GetCategoryName(IGrouping<int, ProductDto> groupedProductDtos)
        {
            return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key).CategoryName;
        }

        public void Dispose()
        {
            StateContainerService.OnProductsChange -= StateHasChanged;
        }
    }
}
