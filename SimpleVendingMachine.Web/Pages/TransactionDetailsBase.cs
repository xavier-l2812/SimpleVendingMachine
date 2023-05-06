using Microsoft.AspNetCore.Components;
using SimpleVendingMachine.Models.Dtos;
using SimpleVendingMachine.Web.Services;
using SimpleVendingMachine.Web.Services.Contracts;

namespace SimpleVendingMachine.Web.Pages
{
    public class TransactionDetailsBase : ComponentBase, IDisposable
    {
        [Parameter]
        public long Id { get; set; }

        [Inject]
        public ITransanctionService TransanctionService { get; set; }

        [Inject]
        public IStateContainerService StateContainerService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<TransactionDetailDto> TransactionDetails { get; set; }

        public TransactionDto SelectedTransaction { get; set; }

        public string ErrorMessage { get; set; }

        protected override void OnInitialized()
        {
            StateContainerService.OnSelectedTransactionChange += StateHasChanged;
        }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                SelectedTransaction = StateContainerService.SelectedTransaction;
                if (Id != SelectedTransaction.Id)
                {
                    NavigationManager.NavigateTo("/Transactions");
                }

                TransactionDetails = await TransanctionService.GetTransactionDetails(Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected void PostRefundTran()
        {
            var transactionDetailToAddDtos = TransactionDetails
                .Select(td => new TransactionDetailToAddDto
                {
                    ProductId = td.ProductId,
                    Qty = td.Qty,
                }).ToList();

            var account = new AccountDto
            {
                Id = SelectedTransaction.AccountId,
                Name = SelectedTransaction.AccountName,
                CardNumber = SelectedTransaction.CardNumber,
                VerificationCode = SelectedTransaction.VerificationCode,
            };

            var transactionToAddDto = new TransactionToAddDto
            {
                TransactionTypeId = (int)TransactionTypeDto.Refund,
                Account = account,
                TransactionDetailToAddDtos = transactionDetailToAddDtos,
                RelatedTransactionId = SelectedTransaction.Id,
                TotalPrice = SelectedTransaction.TotalPrice
            };

            try
            {
                TransanctionService.PostTransaction(transactionToAddDto);

                StateContainerService.ClearSelectedTransaction();

                NavigationManager.NavigateTo("/Transactions");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void Dispose()
        {
            StateContainerService.OnSelectedTransactionChange -= StateHasChanged;
        }
    }
}
