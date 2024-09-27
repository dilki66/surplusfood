using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Services.Interface;

namespace Surplus.Food.Distribution.Chain.Controllers
{
    [Route("api/donor")]
    [ApiController]
    public class DonorController(IDonorService donorService) : ControllerBase
    {

        [HttpPost("payment")]
        public async Task<IActionResult> MakeAPayment([FromQuery] Guid? orderId, [FromBody] PaymentRequest request)
        {
            var response = await donorService.MakeAPaymentAsync(request, orderId);

            return Ok(response);
        }

        [HttpGet("donors")]
        public async Task<IActionResult> GetAllDonors()
        {
            var response = await donorService.GetAllDonorsAsync();

            return Ok(response);
        }
    }
}
