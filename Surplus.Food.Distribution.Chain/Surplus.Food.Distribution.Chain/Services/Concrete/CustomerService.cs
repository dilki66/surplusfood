using Microsoft.EntityFrameworkCore;
using Surplus.Food.Distribution.Chain.Data;
using Surplus.Food.Distribution.Chain.Models.DbModels;
using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Models.ResponseModels;
using Surplus.Food.Distribution.Chain.Services.Interface;

namespace Surplus.Food.Distribution.Chain.Services.Concrete
{
    public class CustomerService(ApplicationDbContext context) : ICustomerService
    {
        public async Task<bool> RegisterAsync(RegisterRequest request, Guid userId)
        {
            var customer = await context.Customers.AnyAsync(x => x.UserId == userId);

            if (customer == true)
            {
                throw new Exception("User already exists");
            }

            var newCustomer = new Customer
            {
                Email = request.Email,
                UserId = userId,
                ContactNo = request.ContactNo,
                Nic = request.Nic,
                Address = request.Address
            };

            try
            {
                await context.Customers.AddAsync(newCustomer);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured while registering customer \n {ex.Message}");
            }
        }

        public async Task<FoodItemListResponse> SearchFoodAsync(
            Guid supplierId, string foodCategory, int foodStatus, string location, int deliveryStatus, string pickupTime, string offers, int page)
        {
            try
            {
                var pageResult = 10f;
                var pageCount = Math.Ceiling(context.FoodItems.Count() / pageResult);

                var foodItems = await context.FoodItems
                                             .Include(x => x.ServiceType)
                                             .Where(x => x.FoodSupplierId == supplierId &&
                                                         x.Category == foodCategory &&
                                                         x.PriceStatusId == foodStatus &&
                                                         x.Location == location &&
                                                         x.ServiceTypeId == deliveryStatus &&
                                                         x.PickupTime == pickupTime &&
                                                         x.Offers == offers &&
                                                         x.DeletedFlag == false)
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
                                                 Price = x.Price
                                             })
                                             .ToListAsync() ?? throw new KeyNotFoundException("Food not found");


                return new FoodItemListResponse
                {
                    FoodItems = foodItems,
                    Pages = (int)pageCount,
                    CurrentPage = page
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddReviewAsync(ReviewRequest request, Guid userId)
        {
            var foodSupplier = await context.FoodSuppliers.FirstOrDefaultAsync(x => x.Id == request.SupplierId)
                                        ?? throw new KeyNotFoundException("Food supplier not found");

            var isReviewExist = await context.FoodSupplierReviews.FirstOrDefaultAsync(x => x.CustomerId == userId && x.SupplierId == request.SupplierId);

            using var transaction = context.Database.BeginTransaction();
            try
            {
                if (isReviewExist is not null)
                {
                    isReviewExist.Review = request.Review;
                    isReviewExist.Rating = request.Rating;
                    isReviewExist.UpdatedAt = DateTime.Now;
                }
                else
                {
                    var review = new FoodSupplierReview
                    {
                        SupplierId = request.SupplierId,
                        CustomerId = userId,
                        Review = request.Review,
                        Rating = request.Rating,
                        CreatedAt = DateTime.Now
                    };

                    await context.FoodSupplierReviews.AddAsync(review);
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error occured while adding review \n {ex.Message}");
            }
        }

        public async Task<IEnumerable<CustomerResponse>> GetAllCustomerDetails()
        {
            var customers = await context.Customers
                                       .Include(x => x.User)
                                       .Select(x => new CustomerResponse
                                       {
                                           Id = x.Id,
                                           UserId = x.UserId,
                                           Name = $"{x.User.FirstName} {x.User.LastName}",
                                           ContactNo = x.ContactNo,
                                           Status = x.Status
                                       })
                                       .ToListAsync();

            return customers;
        }

        public async Task<IEnumerable<ReviewResponse>> GetAllReviews(Guid supplierId)
        {
            var reviews = await (from review in context.FoodSupplierReviews
                                 join customer in context.Customers on review.CustomerId equals customer.UserId
                                 where review.SupplierId == supplierId
                                 select new ReviewResponse
                                 {
                                     Id = review.Id,
                                     CustomerId = customer.Id,
                                     CustomerName = $"{customer.User.FirstName} {customer.User.LastName}",
                                     Review = review.Review,
                                     Rating = review.Rating
                                 }).ToListAsync();

            return reviews;
        }
    }
}
