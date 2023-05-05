using SimpleVendingMachine.Api.Entities;
using SimpleVendingMachine.Models.Dtos;

namespace SimpleVendingMachine.Api.Helpers
{
    public static class DtoConversions
    {
        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products)
        {
            return (from product in products
                    select new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        ImageURL = product.ImageURL,
                        Price = product.Price,
                        QtyInStock = product.QtyInStock,
                        CategoryId = product.ProductCategory.Id,
                        CategoryName = product.ProductCategory.Name
                    }).ToList();
        }

        public static TransactionDto ConvertToDto(this Transaction transaction) 
        {
            return new TransactionDto
            {
                Id = transaction.Id,
                CreatedAt = transaction.CreatedAt,
                TotalPrice = transaction.TotalPrice,
                TotalQuantity = transaction.TotalQuantity,
                RelatedTransactionId = transaction.RelatedTransactionId,
                AccountId = transaction.AccountId,
                AccountName = transaction.Account?.Name,
                CardNumber = transaction.Account?.CardNumber,
                TransactionTypeId = transaction.TransactionTypeId,
                TransactionTypeName = transaction.TransactonType?.Name
            };
        }

        public static IEnumerable<TransactionDto> ConvertToDto(this IEnumerable<Transaction> transactions)
        {
            return (from transaction in transactions
                    select new TransactionDto
                    {
                        Id = transaction.Id,
                        CreatedAt = transaction.CreatedAt,
                        TotalPrice = transaction.TotalPrice,
                        TotalQuantity = transaction.TotalQuantity,
                        RelatedTransactionId = transaction.RelatedTransactionId,
                        AccountId = transaction.AccountId,
                        AccountName = transaction.Account?.Name,
                        CardNumber = transaction.Account?.CardNumber,
                        TransactionTypeId = transaction.TransactionTypeId,
                        TransactionTypeName = transaction.TransactonType?.Name
                    }).ToList();
        }
    }
}
