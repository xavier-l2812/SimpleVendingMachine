using SimpleVendingMachine.Api.Entities;
using SimpleVendingMachine.Models.Dtos;

namespace SimpleVendingMachine.Api.Repositories.Contracts
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactions(TransactionQuery transactionQuery = null);
        Task<IEnumerable<TransactionDetail>> GetTransactionDetails(long transactionId);
        Task<Transaction> PostTransaction(TransactionToAddDto postTransactionDto);
    }
}
