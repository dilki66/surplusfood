using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Models.ResponseModels;

namespace Surplus.Food.Distribution.Chain.Services.Interface
{
    public interface IDonorService
    {
        Task<bool> MakeAPaymentAsync(PaymentRequest request, Guid? orderId);
        Task<IEnumerable<DonorResponse>> GetAllDonorsAsync();
    }
}
