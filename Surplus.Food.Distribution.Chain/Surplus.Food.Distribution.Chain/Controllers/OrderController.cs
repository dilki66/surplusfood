using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Models.ResponseModels;
using Surplus.Food.Distribution.Chain.Services.Interface;
using System.Security.Claims;

namespace Surplus.Food.Distribution.Chain.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpPost("food")]
        public async Task<ActionResult<OrderResponse>> OrderFoodAsync([FromBody] OrderRequest request)
        {
            var response = await orderService.OrderFoodAsync(request, request.UserId);

            var url = $"{Request.GetDisplayUrl()}/{response.Id}";

            return Created(url, response);
        }

        [HttpPut("{orderId}/{OrderStatusId}")]
        public async Task<ActionResult<bool>> UpdateOrderStatusAsync([FromRoute] Guid orderId, [FromRoute] int OrderStatusId)
        {
            var response = await orderService.UpdateOrderStatusAsync(orderId, OrderStatusId);

            return Ok(response);
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<OrderListResponse>> GetAllOrders([FromRoute] int page)
        {
            var response = await orderService.GetAllOrders(page);

            return Ok(response);
        }

        [HttpGet("supplier/{supplierId}/{page}")]
        public async Task<ActionResult<OrderListResponse>> GetOrdersByFoodSupplier([FromRoute] Guid supplierId, [FromRoute] int page)
        {
            var response = await orderService.GetOrdersByFoodSupplier(supplierId, page);

            return Ok(response);
        }

        [HttpPost("cart")]
        public async Task<ActionResult<bool>> Cart([FromBody] OrderRequest request)
        {
            var response = await orderService.Cart(request, request.UserId);

            return Ok(response);
        }

        [HttpGet("cart/{userId}")]
        public async Task<ActionResult<CartResponse>> GetCart(Guid userId)
        {
            var response = await orderService.GetCart(userId);

            return Ok(response);
        }

        [HttpDelete("cart/{userId}/{foodItemId}")]
        public async Task<ActionResult<bool>> ClearCart([FromRoute] Guid userId, [FromRoute] Guid foodItemId)
        {
            var response = await orderService.RemoveFromCart(foodItemId, userId);

            return Ok(response);
        }

        [HttpGet("delivery/{page}")]
        public async Task<ActionResult<OrderListResponse>> GetOrdersForDelevery([FromRoute] int page)
        {
            var response = await orderService.GetOrdersForDelevery(page);

            return Ok(response);
        }

        [HttpGet("donor/{page}")]
        public async Task<ActionResult<OrderListResponse>> GetOrdersDonors([FromRoute] int page)
        {
            var response = await orderService.GetOrdersDonors(page);

            return Ok(response);
        }

        [HttpGet("customer/{userId}/{page}")]
        public async Task<ActionResult<OrderListResponse>> GetOrdersForCustomer([FromRoute] Guid userId, [FromRoute] int page)
        {
            var response = await orderService.GetOrdersForCustomer(page, userId);

            return Ok(response);
        }

        [HttpGet("latest/{userId}")]
        public async Task<ActionResult<OrderListResponse>> GetLatestOrderAsync([FromRoute] Guid userId)
        {
            var response = await orderService.GetLatestOrderAsync(userId);

            return Ok(response);
        }
    }
}
