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
        public IStateContainerService StateContainerService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<TransactionDto> Transactions { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var transactions = await TransanctionService.GetTransactions(new TransactionQuery { Skip = 0, Take = 20 });

                Transactions = transactions.ToList();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected void OpenTransactionDetails(long transactionId)
        {
            var transaction = Transactions.SingleOrDefault(t => t.Id == transactionId);
            if (transaction != null)
            {
                StateContainerService.SetSelectedTransaction(transaction);
            }

            NavigationManager.NavigateTo($"/TransactionDetails/{transactionId}");
        }
    }
}
