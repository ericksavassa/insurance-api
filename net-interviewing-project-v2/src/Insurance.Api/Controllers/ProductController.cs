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
        public async Task<IActionResult> CalculateProductInsurance([FromBody] ProductInsurenceDto productToInsure)
        {
            if (!ModelState.IsValid)         
                return BadRequest(ModelState); 

            var insuranceValue = await _productInsuranceService.CalculateProductInsurance(productToInsure.ProductId);
            productToInsure.InsuranceValue = insuranceValue;

            return Ok(productToInsure);
        }

        [HttpPost]
        [Route("api/insurance/product-list")]
        public async Task<IActionResult> CalculateInsuranceToProductList([FromBody] ProductListInsurenceDto productListToInsure)
        {
            if (!ModelState.IsValid)          
                return BadRequest(ModelState); 

            var insuranceValue = await _productInsuranceService.CalculateInsuranceToProductList(productListToInsure.ProductIds);
            productListToInsure.InsuranceValue = insuranceValue;

            return Ok(productListToInsure);
        }
    }
}