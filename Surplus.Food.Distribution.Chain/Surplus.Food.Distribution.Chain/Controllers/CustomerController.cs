using Microsoft.AspNetCore.Mvc;
using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Models.ResponseModels;
using Surplus.Food.Distribution.Chain.Services.Interface;

namespace Surplus.Food.Distribution.Chain.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        [HttpGet("food/{page}")]
        public async Task<ActionResult<FoodItemListResponse>> SearchFoodAsync([FromQuery] Guid supplierId, [FromQuery] string foodCategory,
            [FromQuery] int foodStatus, [FromQuery] string location, [FromQuery] int deliveryStatus, [FromQuery] string pickupTime, [FromQuery] string offers, [FromRoute] int page)
        {
            var response = await customerService.SearchFoodAsync(supplierId, foodCategory, foodStatus, location, deliveryStatus, pickupTime, offers, page);

            return Ok(response);
        }

        [HttpPost("review")]
        public async Task<ActionResult<bool>> AddReviewAsync([FromBody] ReviewRequest request, [FromQuery] Guid userId)
        {
            var response = await customerService.AddReviewAsync(request, userId);

            return Ok(response);
        }

        [HttpGet("details")]
        public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetAllCustomerDetails()
        {
            var response = await customerService.GetAllCustomerDetails();

            return Ok(response);
        }

        [HttpGet("reviews/{supplierId}")]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetAllReviews([FromRoute] Guid supplierId)
        {
            var response = await customerService.GetAllReviews(supplierId);

            return Ok(response);
        }
    }
}
