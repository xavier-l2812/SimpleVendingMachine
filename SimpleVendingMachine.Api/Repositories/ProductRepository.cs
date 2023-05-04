using Microsoft.EntityFrameworkCore;
using SimpleVendingMachine.Api.Data;
using SimpleVendingMachine.Api.Entities;
using SimpleVendingMachine.Api.Repositories.Contracts;

namespace SimpleVendingMachine.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly VendingMachineDbContext dbContext;

        public ProductRepository(VendingMachineDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await dbContext.ProductCategories.ToListAsync();

            return categories;
        }

        public async Task<ProductCategory> GetCategory(int categoryId)
        {
            var category = await dbContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == categoryId);

            return category;
        }

        public async Task<Product> GetItem(int productId)
        {
            var product = await dbContext.Products
                                .Include(p => p.ProductCategory)
                                .SingleOrDefaultAsync(p => p.Id == productId);

            return product;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await dbContext.Products
                                 .Include(p => p.ProductCategory).ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetItemsByCategory(int categoryId)
        {
            var products = await dbContext.Products
                                 .Include(p => p.ProductCategory)
                                 .Where(p => p.CategoryId == categoryId).ToListAsync();

            return products;
        }
    }
}
