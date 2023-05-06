using SimpleVendingMachine.Models.Dtos;

namespace SimpleVendingMachine.Web.Services.Contracts
{
    public interface ITransanctionService
    {
        Task<TransactionDto> PostTransaction(TransactionToAddDto transactionToAddDto);
        Task<List<TransactionDto>> GetTransactions(TransactionQuery transactionQuery);
        Task<List<TransactionDetailDto>> GetTransactionDetails(long transactionId);
    }
}
