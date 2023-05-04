using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleVendingMachine.Api.Helpers;
using SimpleVendingMachine.Api.Repositories.Contracts;
using SimpleVendingMachine.Models.Dtos;

namespace SimpleVendingMachine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        {
            try
            {
                var products = await productRepository.GetItems();

                if (products == null)
                {
                    return NotFound();
                }
                else
                {
                    var productDtos = products.ConvertToDto();

                    return Ok(productDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database.");
            }
        }
    }
}
