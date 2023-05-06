
using Newtonsoft.Json;
using SimpleVendingMachine.Models.Dtos;
using SimpleVendingMachine.Web.Services.Contracts;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace SimpleVendingMachine.Web.Services
{
    public class TransactionService : ITransanctionService
    {
        private readonly HttpClient httpClient;

        public TransactionService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<TransactionDto>> GetTransactions(TransactionQuery transactionQuery)
        {
            var queryString = objectToQueryString(transactionQuery);
            var requestUri = $"api/Transaction?{queryString}";

            var response = await httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<TransactionDto>>();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{response.StatusCode}; Message: {errorMessage}");
            }
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

        public async Task<List<TransactionDetailDto>> GetTransactionDetails(long transactionId)
        {
            var response = await httpClient.GetAsync($"api/Transaction/{transactionId}/TransactionDetails");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<TransactionDetailDto>>();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{response.StatusCode}; Message: {errorMessage}");
            }
        }

        private string objectToQueryString(object obj)
        {
            var jsonStr = JsonConvert.SerializeObject(obj);
            var dict = JsonConvert.DeserializeObject<IDictionary<string, string>>(jsonStr);
            var parameters = dict.Select(e => $"{HttpUtility.UrlEncode(e.Key)}={HttpUtility.UrlEncode(e.Value)}");
            var queryStr = string.Join("&", parameters);

            return queryStr;
        }
    }
}
