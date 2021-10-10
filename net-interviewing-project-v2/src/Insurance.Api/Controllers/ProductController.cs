using Insurance.Api.Dtos;
using Insurance.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductInsuranceService _productInsuranceService;

        public ProductController(IProductInsuranceService productInsuranceService)
        {
            _productInsuranceService = productInsuranceService;
        }

        [HttpPost]
        [Route("api/insurance/product")]
        public async Task<IActionResult> CalculateInsurance([FromBody] ProductInsurenceDto productToInsure)
        {
            var insuranceValue = await _productInsuranceService.CalculateProductInsurance(productToInsure.ProductId);
            productToInsure.InsuranceValue = insuranceValue;

            return Ok(productToInsure);
        }
    }
}