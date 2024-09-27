using Microsoft.EntityFrameworkCore;
using Surplus.Food.Distribution.Chain.Data;
using Surplus.Food.Distribution.Chain.Models.DbModels;
using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Services.Interface;

namespace Surplus.Food.Distribution.Chain.Services.Concrete
{
    public class AdminService(ApplicationDbContext context) : IAdminService
    {
        public async Task<bool> RegisterAdmin(RegisterRequest request, Guid userId)
        {
            var admin = await context.Admins.AnyAsync(x => x.Email == request.Email);

            if (admin == true)
            {
                throw new Exception("Admin already exists");
            }

            var newAdmin = new Admin
            {
                UserId = userId,
                Email = request.Email,
            };

            try
            {
                await context.Admins.AddAsync(newAdmin);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while registering admin \n {ex.Message}");
            }
        }
        public async Task<bool> UpdateFoodSupplier(FoodSuplierRequest request, Guid id)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var supplier = await context.FoodSuppliers.FirstOrDefaultAsync(x => x.Id == id);

            if (supplier == null)
            {
                return false;
            }

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                supplier.ContactNo = request.ContactNo;
                supplier.Email = request.Email;
                supplier.SuplierName = request.SuplierName;
                supplier.OwnerName = request.OwnerName;

                context.FoodSuppliers.Update(supplier);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error occured while updating food supplier \n {ex.Message}");
            }
        }

        public async Task<bool> DesableFoodSupplier(Guid id)
        {
            var supplier = await context.FoodSuppliers.FirstOrDefaultAsync(x => x.Id == id);
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == supplier.UserId);

            if (supplier == null)
            {
                return false;
            }

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                if (supplier.Status == true && user.EmailConfirmed == false)
                {
                    supplier.Status = false;
                    user.EmailConfirmed = true;
                }
                else
                {
                    supplier.Status = true;
                    user.EmailConfirmed = false;
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error occured while deleting food supplier \n {ex.Message}");
            }
        }

        public async Task<bool> DisableCustomer(Guid id)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == customer.UserId);

            if (customer == null)
            {
                return false;
            }

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                if (customer.Status == true && user.EmailConfirmed == false)
                {
                    customer.Status = false;
                    user.EmailConfirmed = true;
                }
                else
                {
                    customer.Status = true;
                    user.EmailConfirmed = false;
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error occured while deleting customer \n {ex.Message}");
            }
        }

        public async Task<bool> DisableDonor(Guid userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                if (user.EmailConfirmed == false)
                {
                    user.EmailConfirmed = true;
                }
                else
                    user.EmailConfirmed = false;


                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error occured while deleting donor \n {ex.Message}");
            }
        }
    }
}
