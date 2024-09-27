using Microsoft.AspNetCore.Mvc;
using Surplus.Food.Distribution.Chain.Services.Interface;

namespace Surplus.Food.Distribution.Chain.Controllers
{
    [Route("api/reference")]
    [ApiController]
    public class ReferenceController(IReferenceTypeService referenceTypeService) : ControllerBase
    {
        [HttpGet("order-status")]
        public async Task<IActionResult> GetOrderStatus()
        {
            var response = await referenceTypeService.GetOrderStatusAsync();
            return Ok(response);
        }

        [HttpGet("price-status")]
        public async Task<IActionResult> GetPriceStatus()
        {
            var response = await referenceTypeService.GetPriceStatusAsync();
            return Ok(response);
        }

        [HttpGet("service-type")]
        public async Task<IActionResult> GetServiceType()
        {
            var response = await referenceTypeService.GetServiceTypeAsync();
            return Ok(response);
        }
    }
}
