using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Models.ResponseModels;
using Surplus.Food.Distribution.Chain.Services.Interface;

namespace Surplus.Food.Distribution.Chain.Controllers
{
    [Route("api/supplier")]
    [ApiController]
    public class SupplierController(IFoodSupplierService supplierService) : ControllerBase
    {
        [HttpGet("fooditem/{page}")]
        public async Task<ActionResult<FoodItemListResponse>> GetFoodItemsBySupplier([FromRoute] int page, [FromQuery] Guid userId)
        {
            var foodItems = await supplierService.GetFoodItemsBySuplierAsync(userId, page);

            return Ok(foodItems);
        }

        [HttpPost("fooditem")]
        public async Task<ActionResult<FoodItemResponse>> AddFoodItem([FromBody] FoodItemRequest request)
        {
            var response = await supplierService.AddFoodItemAsync(request);
            var url = $"{Request.GetDisplayUrl()}/{response.Id}";

            return Created(url, response);
        }

        [HttpPut("fooditem/{id}")]
        public async Task<ActionResult<FoodItemResponse>> UpdateFoodItem([FromRoute] Guid id, [FromBody] FoodItemRequest request)
        {
            var response = await supplierService.UpdateFoodItemAsync(request, id);

            return Ok(response);
        }

        [HttpDelete("fooditem/{id}")]
        public async Task<ActionResult<FoodItemResponse>> DeleteFoodItem([FromRoute] Guid id)
        {
            var response = await supplierService.DeleteFoodItemAsync(id);

            return Ok(response);
        }

        [HttpGet("supplier")]
        public async Task<ActionResult<IEnumerable<FoodSupplierResponse>>> GetAllSupplierDetails()
        {
            var response = await supplierService.GetAllSupplierDetails();

            return Ok(response);
        }
    }
}
