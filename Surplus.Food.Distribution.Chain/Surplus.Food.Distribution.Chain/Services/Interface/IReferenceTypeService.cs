using Surplus.Food.Distribution.Chain.Models.DbModels;
using Surplus.Food.Distribution.Chain.Models.ResponseModels;

namespace Surplus.Food.Distribution.Chain.Services.Interface
{
    public interface IReferenceTypeService
    {
        Task<RefResponse<RefOrderStatus>> GetOrderStatusAsync();
        Task<RefResponse<RefPriceStatus>> GetPriceStatusAsync();
        Task<RefResponse<RefServiceType>> GetServiceTypeAsync();
    }
}
