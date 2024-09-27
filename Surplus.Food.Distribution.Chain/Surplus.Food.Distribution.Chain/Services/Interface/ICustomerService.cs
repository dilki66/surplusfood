using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Models.ResponseModels;

namespace Surplus.Food.Distribution.Chain.Services.Interface
{
    public interface ICustomerService
    {
        Task<FoodItemListResponse> SearchFoodAsync(Guid supplierId, string foodCategory, int foodStatus,
            string location, int deliveryStatus, string pickupTime, string offers, int page);
        Task<bool> AddReviewAsync(ReviewRequest request, Guid userId);
        Task<IEnumerable<CustomerResponse>> GetAllCustomerDetails();
        Task<IEnumerable<ReviewResponse>> GetAllReviews(Guid supplierId);
    }
}
