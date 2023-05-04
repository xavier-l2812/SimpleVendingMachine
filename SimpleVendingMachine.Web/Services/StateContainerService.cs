using SimpleVendingMachine.Models.Dtos;
using SimpleVendingMachine.Web.Services.Contracts;

namespace SimpleVendingMachine.Web.Services
{
    public class StateContainerService : IStateContainerService
    {
        public StateContainerService() 
        {
            Products = new List<ProductDto>();
        }

        public List<ProductDto> Products { get; protected set; }

        public void SetProducts(IEnumerable<ProductDto> products)
        {
            Products = products.ToList();
            NotifyProductsChanged();
        }

        public event Action OnProductsChange;

        public void NotifyProductsChanged() => OnProductsChange?.Invoke();
    }
}
