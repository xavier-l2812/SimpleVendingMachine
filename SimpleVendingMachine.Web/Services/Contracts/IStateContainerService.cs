using SimpleVendingMachine.Models.Dtos;

namespace SimpleVendingMachine.Web.Services.Contracts
{
    public interface IStateContainerService
    {
        public List<ProductDto> Products { get; }
        public void SetProducts(IEnumerable<ProductDto> products);

        public event Action OnProductsChange;
    }
}
