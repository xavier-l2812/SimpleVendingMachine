using SimpleVendingMachine.Models.Dtos;
using SimpleVendingMachine.Web.Services.Contracts;
using System.Net.Http.Json;

namespace SimpleVendingMachine.Web.Services
{
    public class TransactionService : ITransanctionService
    {
        private readonly HttpClient httpClient;

        public TransactionService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<List<TransactionDto>> GetTransactions(TransactionQuery transactionQuery)
        {
            throw new NotImplementedException();
        }

        public async Task<TransactionDto> PostTransaction(TransactionToAddDto transactionToAddDto)
        {
            var response = await httpClient.PostAsJsonAsync<TransactionToAddDto>("api/Transaction", transactionToAddDto);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(TransactionDto);
                }
                return await response.Content.ReadFromJsonAsync<TransactionDto>();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{response.StatusCode}; Message: {errorMessage}");
            }
        }
    }
}
