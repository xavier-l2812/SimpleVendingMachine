using SimpleVendingMachine.Api.Entities;

namespace SimpleVendingMachine.Api.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetItem(int productId);
        Task<ProductCategory> GetCategory(int categoryId);
        Task<IEnumerable<Product>> GetItemsByCategory(int categoryId);
    }
}
