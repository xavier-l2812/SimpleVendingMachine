using Microsoft.EntityFrameworkCore;
using SimpleVendingMachine.Api.Data;
using SimpleVendingMachine.Api.Entities;
using SimpleVendingMachine.Api.Exceptions;
using SimpleVendingMachine.Api.Repositories.Contracts;
using SimpleVendingMachine.Models.Dtos;
using System.Linq;

namespace SimpleVendingMachine.Api.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly VendingMachineDbContext dbContext;

        public TransactionRepository(VendingMachineDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<TransactionDetail>> GetTransactionDetails(long transactionId)
        {
            var transactionDetails = await dbContext.TransactionDetails
                                           .Include(td => td.Product)
                                           .Where(td => td.TransactionId == transactionId)
                                           .ToListAsync();

            return transactionDetails;
        }

        public async Task<IEnumerable<Transaction>> GetTransactions(TransactionQuery transactionQuery = null)
        {
            List<Transaction> transactionList;
            if (transactionQuery is null)
            {
                transactionList = await dbContext.Transactions
                                        .Include(t => t.Account)
                                        .Include(t => t.TransactonType)
                                        .ToListAsync();
            }
            else
            {
                transactionList = await dbContext.Transactions
                                        .Include(t => t.Account)
                                        .Include(t => t.TransactonType)
                                        .Skip(transactionQuery.Skip.HasValue ? transactionQuery.Skip.Value : 0)
                                        .Take(transactionQuery.Take)
                                        .ToListAsync();
            }

            return transactionList;
        }

        public async Task<Transaction> PostTransaction(TransactionToAddDto postTransactionDto)
        {
            // Check if the products exist
            var productNotFound = postTransactionDto.TransactionDetailToAddDtos.Any(td => !ProductExists(td.ProductId));
            if (productNotFound)
            {
                throw new ProductNotFoundException();
            }
            // Check the inventory
            var outOfStockProducts = new List<string>();
            postTransactionDto.TransactionDetailToAddDtos.ForEach(td => ProductOutOfStock(td.ProductId, td.Qty, ref outOfStockProducts));
            if (outOfStockProducts.Any())
            {
                var errorMessage = string.Join(";", outOfStockProducts);
                throw new ProductOutOfStockException(errorMessage);
            }
            // Check the transaction type
            var transactionTypeNotFound = !TransactionTypeExists(postTransactionDto.TransactionTypeId);
            if (transactionTypeNotFound)
            {
                throw new TransactionTypeNotFoundException();
            }
            // Check Related Transaction Id
            if ((TransactionTypeDto)(postTransactionDto.TransactionTypeId) == TransactionTypeDto.Refund &&
                !postTransactionDto.RelatedTransactionId.HasValue)
            {
                throw new RelatedTransactionIdMissingException();
            }

            // Get the AccountId
            int accountId = 0;
            var existingAcct = GetAccount(postTransactionDto.Account);
            if (existingAcct == null)
            {
                var newAcct = await AddAccount(postTransactionDto.Account);
                accountId = newAcct.Id;
            }
            else
            {
                accountId = existingAcct.Id;
            }

            // Insert the transaction
            var newTransaction = new Transaction
            {
                CreatedAt = DateTime.UtcNow,
                TotalPrice = postTransactionDto.TotalPrice,
                TotalQuantity = postTransactionDto.TotalQuantity,
                TransactionTypeId = postTransactionDto.TransactionTypeId,
                AccountId = accountId,
                RelatedTransactionId = postTransactionDto.RelatedTransactionId
            };
            dbContext.Transactions.Add(newTransaction);
            await dbContext.SaveChangesAsync();

            // Insert the transaction details, and update the quantity in stock
            var transactionDetailList = new List<TransactionDetail>();
            var productList = new List<Product>();
            foreach (var tranDetailToAddDto in postTransactionDto.TransactionDetailToAddDtos)
            {
                var transactionDetail = new TransactionDetail
                {
                    TransactionId = newTransaction.Id,
                    ProductId = tranDetailToAddDto.ProductId,
                    Qty = tranDetailToAddDto.Qty,
                };
                transactionDetailList.Add(transactionDetail);

                var product = GetProduct(tranDetailToAddDto.ProductId);
                if ((TransactionTypeDto)(postTransactionDto.TransactionTypeId) == TransactionTypeDto.Refund)
                {
                    product.QtyInStock += tranDetailToAddDto.Qty;
                }
                else
                {
                    product.QtyInStock -= tranDetailToAddDto.Qty;
                }
                productList.Add(product);
            }
            dbContext.TransactionDetails.AddRange(transactionDetailList);
            dbContext.Products.UpdateRange(productList);
            await dbContext.SaveChangesAsync();

            return newTransaction;
        }

        public async Task<Account> AddAccount(AccountDto accountDto)
        {
            var newAcct = new Account
            {
                Name = accountDto.Name,
                CardNumber = accountDto.CardNumber,
                VerificationCode = accountDto.VerificationCode
            };
            dbContext.Accounts.Add(newAcct);
            await dbContext.SaveChangesAsync();

            return newAcct;
        }

        public Account GetAccount(AccountDto accountDto)
        {
            var existingAccount = dbContext.Accounts
                                  .Where(acct => acct.CardNumber == accountDto.CardNumber &&
                                         acct.VerificationCode == accountDto.VerificationCode)
                                  .FirstOrDefault();

            return existingAccount;
        }

        public bool ProductExists(int productId)
        {
            var result = dbContext.Products
                                  .Any(p => p.Id == productId);

            return result;
        }

        public bool ProductOutOfStock(int productId, int Qty, ref List<string> outOfStockProducts)
        {
            var product = dbContext.Products
                                   .Where(p => p.Id == productId)
                                   .FirstOrDefault();

            if (product != null && product.QtyInStock - Qty >= 0)
            {
                return true;
            }

            outOfStockProducts.Add($"{product.Name} - {product.Id}: {product.QtyInStock}");

            return false;
        }

        public bool TransactionTypeExists(int transactionTypeId)
        {
            var result = dbContext.TransactonTypes
                                  .Any(tt => tt.Id == transactionTypeId);

            return result;
        }

        public Product GetProduct(int productId)
        {
            var product = dbContext.Products.Find(productId);

            return product;
        }

        public async Task<Transaction> GetTransactionById(long transactionId)
        {
            var transaction = await dbContext.Transactions
                                  .Include(t => t.Account)
                                  .Include(t => t.TransactonType)
                                  .SingleOrDefaultAsync(t => t.Id == transactionId);

            return transaction;
        }
    }
}
