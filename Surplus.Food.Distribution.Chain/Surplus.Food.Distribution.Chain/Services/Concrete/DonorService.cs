using Microsoft.EntityFrameworkCore;
using Surplus.Food.Distribution.Chain.Data;
using Surplus.Food.Distribution.Chain.Models.DbModels;
using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Models.ResponseModels;
using Surplus.Food.Distribution.Chain.Services.Interface;

namespace Surplus.Food.Distribution.Chain.Services.Concrete
{
    public class DonorService(ApplicationDbContext context) : IDonorService
    {
        public async Task<bool> RegisterAsync(RegisterRequest request, Guid userId)
        {
            var donor = await context.Donors.AnyAsync(x => x.UserId == userId);

            if (donor == true)
            {
                throw new Exception("User already exists");
            }

            var newDonor = new Donor
            {
                Email = request.Email,
                UserId = userId,
                ContactNo = request.ContactNo,
                Status = true
            };

            try
            {
                await context.Donors.AddAsync(newDonor);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while registering donor \n {ex.Message}");
            }
        }

        public async Task<bool> MakeAPaymentAsync(PaymentRequest request, Guid? orderId)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var payment = new Payment
                {
                    OrderId = orderId,
                    DonerName = request.DonerName,
                    ContactNo = request.ContactNo,
                    CardholderName = request.CardholderName,
                    PaymentType = request.PaymentType,
                    CardNo = request.CardNo,
                    Amount = request.Amount,
                    ExpireDate = request.ExpireDate,
                    SecurityCode = request.SecurityCode,
                    Status = true
                };

                if (orderId != null)
                {
                    var order = await context.Orders.FirstOrDefaultAsync(x => x.Id == orderId );
                    
                    order.OrderStatusId = 12;
                    order.PaymentMethod = request.PaymentType;
                }

                await context.Payments.AddAsync(payment);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"Error occured while making payment \n {ex.Message}");
            }
            
        }

        public async Task<IEnumerable<DonorResponse>> GetAllDonorsAsync()
        {
            var donor = await context.Users.Where(u => u.Email == "guest@surplus.com").Select(x => new DonorResponse
            {
                Id = x.Id,
                ContactNo = x.PhoneNumber,
                Name = x.FirstName + " " + x.LastName,
            }).ToListAsync();

            return donor;
        }
    }
}
