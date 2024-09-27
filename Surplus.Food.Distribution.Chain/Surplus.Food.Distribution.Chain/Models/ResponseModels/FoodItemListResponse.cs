using Surplus.Food.Distribution.Chain.Models.DbModels;

namespace Surplus.Food.Distribution.Chain.Models.ResponseModels
{
    public class FoodItemListResponse
    {
        public List<FoodItemResponse> FoodItems { get; set; } = new List<FoodItemResponse>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
