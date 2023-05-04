using Microsoft.AspNetCore.Components;
using SimpleVendingMachine.Models.Dtos;

namespace SimpleVendingMachine.Web.Pages
{
    public class DisplayProductsBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
