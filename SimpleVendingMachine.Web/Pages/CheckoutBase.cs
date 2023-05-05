using Microsoft.AspNetCore.Components;
using SimpleVendingMachine.Models.Dtos;
using SimpleVendingMachine.Web.Models;
using SimpleVendingMachine.Web.Services.Contracts;

namespace SimpleVendingMachine.Web.Pages
{
    public class CheckoutBase : ComponentBase
    {
        [Inject]
        public ITransanctionService TransanctionService{ get; set; }

        [Inject]
        public IStateContainerService StateContainerService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public AccountDto Account { get; set; }

        public string Message { get; set; }

        public string ErrorMessage { get; set; }

        protected override void OnInitialized()
        {
            Message = "Assuming a card reader has read the credit card information. " +
                      "This page is for final review before posting the transaction.";

            // Hard coded account information for now.
            Account = new AccountDto
            {
                Name = "Robert Leeson",
                CardNumber = "4242 4242 4242 4242",
                VerificationCode = "321"
            };
        }

        protected void PostPurchaseTran()
        {
            var transactionDetailToAddDtos = StateContainerService.CartItems
                .Select(ci => new TransactionDetailToAddDto
                {
                    ProductId = ci.ProductId,
                    Qty = ci.Qty,
                }).ToList();

            var transactionToAddDto = new TransactionToAddDto
            {
                TransactionTypeId = (int)TransactionTypeDto.Purchase,
                Account = Account,
                TransactionDetailToAddDtos = transactionDetailToAddDtos,
                RelatedTransactionId = null,
                TotalPrice = StateContainerService.TotalPrice
            };

            try
            {
                TransanctionService.PostTransaction(transactionToAddDto);

                NavigationManager.NavigateTo("/");
            }
            catch(Exception ex) 
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
