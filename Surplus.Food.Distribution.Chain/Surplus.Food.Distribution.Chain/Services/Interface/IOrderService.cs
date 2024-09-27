using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Models.ResponseModels;

namespace Surplus.Food.Distribution.Chain.Services.Interface
{
    public interface IOrderService
    {
        Task<OrderResponse> OrderFoodAsync(OrderRequest request, Guid userId);
        Task<bool> UpdateOrderStatusAsync(Guid orderId, int orderStatus);
        Task<OrderListResponse> GetAllOrders(int page);
        Task<OrderListResponse> GetOrdersByFoodSupplier(Guid supplierId, int page);
        Task<bool> Cart(OrderRequest request, Guid userId);
        Task<CartResponse> GetCart(Guid userId);
        Task<bool> RemoveFromCart(Guid foodItemId, Guid userId);
        Task<OrderListResponse> GetOrdersForDelevery(int page);
        Task<OrderListResponse> GetOrdersDonors(int page);
        Task<OrderListResponse> GetOrdersForCustomer(int page, Guid userId);
        Task<OrderListResponse> GetLatestOrderAsync(Guid userId);
    }
}
