using Microsoft.AspNetCore.Components;
using SimpleVendingMachine.Models.Dtos;
using SimpleVendingMachine.Web.Services.Contracts;

namespace SimpleVendingMachine.Web.Pages
{
    public class TransactionsBase : ComponentBase
    {
        [Inject]
        public ITransanctionService TransanctionService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<TransactionDto> Transactions { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var transactions = await TransanctionService.GetTransactions(new TransactionQuery { Skip = 0, Take = 20 });
                transactions.ForEach(t =>
                {
                    t.CardNumber = t.CardNumber.Trim();
                    t.CardNumber = t.CardNumber.Replace(" ", string.Empty);
                });
                Transactions = transactions.ToList();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
