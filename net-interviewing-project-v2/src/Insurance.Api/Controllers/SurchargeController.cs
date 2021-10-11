using Insurance.Application.Services.Interfaces;
using Insurance.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers
{
    public class SurchargeController : Controller
    {
        private readonly ISurchargeService _surchargeService;

        public SurchargeController(ISurchargeService surchargeService)
        {
            _surchargeService = surchargeService;
        }

        [HttpPost]
        [Route("api/surcharge")]
        public async Task<IActionResult> AddSurchargeRates([FromBody] IList<ProductTypeSurchargeDto> productTypeSurchargeList)
        {
            if (!ModelState.IsValid)         
                return BadRequest(ModelState); 

            await _surchargeService.AddSurchargeRatesForProductTypes(productTypeSurchargeList);
            return Ok();
        }
    }
}