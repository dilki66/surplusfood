using Microsoft.EntityFrameworkCore;
using Surplus.Food.Distribution.Chain.Data;
using Surplus.Food.Distribution.Chain.Models.DbModels;
using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Models.ResponseModels;
using Surplus.Food.Distribution.Chain.Services.Interface;

namespace Surplus.Food.Distribution.Chain.Services.Concrete
{
    public class FoodSupplierService(ApplicationDbContext context) : IFoodSupplierService
    {
        public async Task<bool> RegisterAasync(RegisterRequest request, Guid userId)
        {
            var foodSupplier = await context.FoodSuppliers.AnyAsync(x => x.UserId == userId);

            if (foodSupplier == true)
            {
                throw new Exception("User already exists");
            }

            var newFoodSupplier = new FoodSupplier
            {
                SuplierName = request.SuplierName,
                Email = request.Email,
                UserId = userId,
                ContactNo = request.ContactNo,
                Nic = request.Nic,
                TrainingLicense = request.TrainingLicense,
                OwnerName = request.OwnerName,
                OwnerNic = request.OwnerNic,
                Location = request.Location,
                BrNo = request.BrNo,
                Status = false
            };

            try
            {
                await context.FoodSuppliers.AddAsync(newFoodSupplier);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while registering food supplier \n {ex.Message}");
            }
        }

        public async Task<FoodItemListResponse> GetFoodItemsBySuplierAsync(Guid suplierId, int page)
        {
            var pageResult = 10f;
            var pageCount = Math.Ceiling(context.FoodItems.Count() / pageResult);

            var foodItems = await context.FoodItems.Where(x => x.FoodSupplier.UserId == suplierId && x.DeletedFlag == false)
                                                   .Skip((page - 1) * (int)pageResult)
                                                   .Take((int)pageResult)
                                                   .Select(x => new FoodItemResponse
                                                   {
                                                       Id = x.Id,
                                                       Image = x.Image,
                                                       Category = x.Category,
                                                       ServiceTypeId = x.ServiceType.ServiceType,
                                                       FoodSupplierId = x.FoodSupplier.UserId,
                                                       Quantity = x.Quantity,
                                                       PriceStatus = x.PriceStatus.PriceStatus,
                                                       Price = x.Price,
                                                       Offers = x.Offers
                                                   })
                                                   .ToListAsync() ?? throw new KeyNotFoundException("Food not found");

            return new FoodItemListResponse
            {
                FoodItems = foodItems,
                Pages = (int)pageCount,
                CurrentPage = page
            };
        }

        public async Task<FoodItemResponse> AddFoodItemAsync(FoodItemRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            byte[]? file64 = null;
            string base64String = request.Image;

            if (base64String != null)
            {
                base64String = base64String.Substring(base64String.IndexOf(",") + 1);
                file64 = Convert.FromBase64String(base64String);
            }

            var foodItem = new FoodItem
            {
                Image = file64,
                Category = request.Category,
                ServiceTypeId = request.ServiceTypeId,
                FoodSupplierId = request.FoodSupplierId,
                Quantity = request.Quantity,
                Price = request.Price,
                PriceStatusId = request.PriceStatus,
                PickupTime = request.PickupTime,
                Location = request.Location,
                Offers = request.Offers,
                DeletedFlag = false
            };

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await context.FoodItems.AddAsync(foodItem);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new FoodItemResponse
                {
                    Id = foodItem.Id,
                    Image = foodItem.Image,
                    Category = foodItem.Category,
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error occured while adding food item \n {ex.Message}");
            }
        }

        public async Task<FoodItemResponse> UpdateFoodItemAsync(FoodItemRequest request, Guid id)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var foodItem = await context.FoodItems.FirstOrDefaultAsync(x => x.Id == id && x.DeletedFlag == false) ?? throw new KeyNotFoundException("Food not found");

            byte[]? file64 = null;
            string base64String = request.Image;

            if (base64String != null)
            {
                base64String = base64String.Substring(base64String.IndexOf(",") + 1);
                file64 = Convert.FromBase64String(base64String);
            }

            if (foodItem.ServiceTypeId == request.ServiceTypeId)
            {
                foodItem.Image = file64;
                foodItem.Category = request.Category;
                foodItem.Quantity = request.Quantity;
                foodItem.Price = request.Price;
                foodItem.PriceStatusId = request.PriceStatus;
            }
            else if (foodItem.PriceStatusId == request.PriceStatus)
            {
                foodItem.Image = file64;
                foodItem.Category = request.Category;
                foodItem.Quantity = request.Quantity;
                foodItem.ServiceTypeId = request.ServiceTypeId;
                foodItem.Price = request.Price;
            }
            else if (foodItem.ServiceTypeId == request.ServiceTypeId && foodItem.PriceStatusId == request.PriceStatus)
            {
                foodItem.Image = file64;
                foodItem.Category = request.Category;
                foodItem.Quantity = request.Quantity;
                foodItem.Price = request.Price;
            }
            else
            {
                foodItem.Image = file64;
                foodItem.Category = request.Category;
                foodItem.ServiceTypeId = request.ServiceTypeId;
                foodItem.Quantity = request.Quantity;
                foodItem.Price = request.Price;
                foodItem.PriceStatusId = request.PriceStatus;
            }

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                context.FoodItems.Update(foodItem);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new FoodItemResponse
                {
                    Id = foodItem.Id,
                    Image = foodItem.Image,
                    Category = foodItem.Category,
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error occured while updating food item \n {ex.Message}");
            }
        }

        public async Task<bool> DeleteFoodItemAsync(Guid id)
        {
            var foodItem = await context.FoodItems.FirstOrDefaultAsync(x => x.Id == id && x.DeletedFlag == false) ?? throw new KeyNotFoundException("Food not found");

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                foodItem.DeletedFlag = true;
                context.FoodItems.Update(foodItem);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error occured while deleting food item \n {ex.Message}");
            }
        }

        public async Task<IEnumerable<FoodSupplierResponse>> GetAllSupplierDetails()
        {
            var suppliers = await context.FoodSuppliers.Include(x => x.User)
                                                      .Select(x => new FoodSupplierResponse
                                                      {
                                                          Id = x.Id,
                                                          SuplierName = x.SuplierName,
                                                          ContactNo = x.ContactNo,
                                                          Email = x.Email,
                                                          OwnerName = x.OwnerName,
                                                          Status = x.Status
                                                      })
                                                      .ToListAsync() ?? throw new KeyNotFoundException("Food supplier not found");

            return suppliers;
        }
    }
}
