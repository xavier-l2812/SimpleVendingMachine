using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SimpleVendingMachine.Web.Models;
using SimpleVendingMachine.Web.Services.Contracts;
using System.ComponentModel;

namespace SimpleVendingMachine.Web.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IStateContainerService StateContainerService { get; set; }

        protected void DeleteCartItem(int productId)
        {
            StateContainerService.RemoveCartItem(productId);
        }
    }
}
