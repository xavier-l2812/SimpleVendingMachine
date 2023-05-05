using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleVendingMachine.Api.Helpers;
using SimpleVendingMachine.Api.Repositories.Contracts;
using SimpleVendingMachine.Models.Dtos;

namespace SimpleVendingMachine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<TransactionDto>> GetTransaction(long id)
        {
            try
            {
                var transaction = await transactionRepository.GetTransactionById(id);

                var transactionDto = transaction.ConvertToDto();

                return Ok(transactionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> PostTransaction([FromBody] TransactionToAddDto transactionToAddDto)
        {
            try
            {
                var newTransaction = await transactionRepository.PostTransaction(transactionToAddDto);

                var newTransactionDto = newTransaction.ConvertToDto();

                return CreatedAtAction(nameof(GetTransaction), new { id = newTransactionDto.Id }, newTransactionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
