using Microsoft.EntityFrameworkCore;
using Surplus.Food.Distribution.Chain.Data;
using Surplus.Food.Distribution.Chain.Models.DbModels;
using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Services.Interface;

namespace Surplus.Food.Distribution.Chain.Services.Concrete
{
    public class DeliveryService(ApplicationDbContext context)
    {
        public async Task<bool> RegisterAsync(RegisterRequest request, Guid userId)
        {
            var delivery = await context.DeliveryPersons.AnyAsync(x => x.UserId == userId);

            if (delivery == true)
            {
                throw new Exception("User already exists");
            }

            var newDelivery = new DeliveryPerson
            {
                Email = request.Email,
                UserId = userId,
                ContactNo = request.ContactNo,
                DrivingLicense = request.DrivingLicense
            };

            try
            {
                await context.DeliveryPersons.AddAsync(newDelivery);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while registering delivery person \n {ex.Message}");
            }
        }
    }
}
