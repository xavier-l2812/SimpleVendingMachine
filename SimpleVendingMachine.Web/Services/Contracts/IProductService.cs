using SimpleVendingMachine.Models.Dtos;

namespace SimpleVendingMachine.Web.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();
    }
}
