using Microsoft.EntityFrameworkCore;
using SimpleVendingMachine.Api.Entities;

namespace SimpleVendingMachine.Api.Data
{
    public class VendingMachineDbContext : DbContext
    {
        public VendingMachineDbContext(DbContextOptions<VendingMachineDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Products
            // Beverage Category
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Soda",
                Description = "Super sweet; bad for your health, but can cheer you up!",
                ImageURL = "/Images/Beverage/Soda.jpg",
                Price = 0.95m,
                QtyInStock = 80,
                CategoryId = 1

            });
            // Food Category
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 6,
                Name = "Candy Bar",
                Description = "Very sweet, providing energy for you.",
                ImageURL = "/Images/Snacks/CandyBar.jpg",
                Price = 0.60m,
                QtyInStock = 100,
                CategoryId = 2

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 7,
                Name = "Chips",
                Description = "Delicious chips, best choice for gathering.",
                ImageURL = "/Images/Snacks/Chips.jpg",
                Price = 0.99m,
                QtyInStock = 50,
                CategoryId = 2

            });

            //Add Product Categories
            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
            {
                Id = 1,
                Name = "Beverage",
                IconCSS = "fas fa-coffee"
            });
            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
            {
                Id = 2,
                Name = "Snacks",
                IconCSS = "fas fa-cookie-bite"
            });

            //Add Transaction Types
            modelBuilder.Entity<TransactonType>().HasData(new TransactonType
            {
                Id = 1,
                Name = "Purchase",
            });
            modelBuilder.Entity<TransactonType>().HasData(new TransactonType
            {
                Id = 2,
                Name = "Refund",
            });
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<TransactonType> TransactonTypes { get; set; }
    }
}
