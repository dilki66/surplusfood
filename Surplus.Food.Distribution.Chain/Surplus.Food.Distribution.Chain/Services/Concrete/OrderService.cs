using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Surplus.Food.Distribution.Chain.Data;
using Surplus.Food.Distribution.Chain.Models.DbModels;
using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Models.ResponseModels;
using Surplus.Food.Distribution.Chain.Services.Interface;

namespace Surplus.Food.Distribution.Chain.Services.Concrete
{
    public class OrderService(ApplicationDbContext context) : IOrderService
    {
        public async Task<OrderResponse> OrderFoodAsync(OrderRequest request, Guid userId)
        {
            if (request == null || request.FoodItems == null)
            {
                throw new KeyNotFoundException("No food items found");
            }

            var customer = await context.Users.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new KeyNotFoundException("Customer not found");
            var cart = await context.Carts.Where(x => x.UserId == userId && x.IsDeleted == false).ToListAsync();

            if (cart is not null)
            {
                foreach (var item in cart)
                {
                    item.IsDeleted = true;
                }
            }

            var order = new Order
            {
                SenderName = $"{customer.FirstName} {customer.LastName}",
                UserId = userId,
                RecieverName = request.RecieverName,
                ServiceTypeId = request.ServiceTypeId,
                PaymentMethod = request.PaymentMethod,
                OrderStatusId = 1,
                PriceStatusId = request.PriceStatusId,
                PickupTime = request.PickupTime,
                Location = request.Location,
                CreatedAt = DateTime.Now
            };

            if (request.ServiceTypeId == 2 && request.PriceStatusId == 1)
            {
                order.PaymentMethod = "N/A";
            }

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();

                var existingFoodIds = await context.FoodItems
                                          .Where(x => request.FoodItems.Select(item => item.Id).Contains(x.Id))
                                          .Select(x => x.Id)
                                          .ToListAsync();

                foreach (var item in request.FoodItems)
                {
                    if (!existingFoodIds.Contains(item.Id))
                    {
                        throw new KeyNotFoundException("Food not found");
                    }

                    var orderItem = new OrderItems
                    {
                        FoodItemId = item.Id,
                        OrderId = order.Id,
                        Quantity = item.Quantity,
                        Amount = item.Amount,
                    };

                    await context.OrderItems.AddAsync(orderItem);
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                var response = new OrderResponse
                {
                    Id = order.Id,
                    SenderName = order.SenderName,
                    RecieverName = order.RecieverName
                };

                return response;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error occured while ordering food \n {ex.Message}");
            }
        }

        public async Task<bool> UpdateOrderStatusAsync(Guid orderId, int orderStatus)
        {
            var order = await context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                throw new KeyNotFoundException("Order not found");
            }

            order.OrderStatusId = orderStatus;

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                context.Orders.Update(order);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error occured while updating order status \n {ex.Message}");
            }
        }

        public async Task<OrderListResponse> GetAllOrders(int page)
        {
            try
            {
                var pageResult = 20f;
                var pageCount = Math.Ceiling(context.Orders.Count() / pageResult);

                var orders = await context.Orders
                    .Include(x => x.OrderItems)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .Select(x => new OrderResponse
                    {
                        Id = x.Id,
                        SenderName = x.SenderName,
                        RecieverName = x.RecieverName,
                        ServiceTypeId = x.ServiceType.ServiceType,
                        PaymentMethod = x.PaymentMethod,
                        PickupTime = x.PickupTime,
                        Location = x.Location,
                        OrderStatusId = x.OrderStatus.Status,
                        Price = x.OrderItems.Sum(item => item.Amount * item.Quantity)
                    }).ToListAsync();

                var response = new OrderListResponse
                {
                    Orders = orders,
                    Pages = (int)pageCount,
                    CurrentPage = page
                };

                return response ?? throw new KeyNotFoundException("Orders not found");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderListResponse> GetOrdersForCustomer(int page, Guid userId)
        {
            try
            {
                var pageResult = 20f;
                var pageCount = Math.Ceiling(context.Orders.Count() / pageResult);

                var orders = await context.Orders
                    .Where(x => x.UserId == userId)
                    .Include(x => x.Payments)
                    .Include(x => x.OrderItems)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .Select(x => new OrderResponse
                    {
                        Id = x.Id,
                        SenderName = x.SenderName,
                        RecieverName = x.RecieverName,
                        ServiceTypeId = x.ServiceType.ServiceType,
                        PaymentMethod = x.Payments.Where(p => p.OrderId == x.Id).Select(p => p.PaymentType).FirstOrDefault() ?? x.PaymentMethod,
                        PickupTime = x.PickupTime,
                        Location = x.Location,
                        OrderStatusId = x.OrderStatus.Status,
                        Price = x.OrderItems.Sum(item => item.Amount * item.Quantity)
                    }).ToListAsync();

                var response = new OrderListResponse
                {
                    Orders = orders,
                    Pages = (int)pageCount,
                    CurrentPage = page
                };

                return response ?? throw new KeyNotFoundException("Orders not found");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderListResponse> GetLatestOrderAsync(Guid userId)
        {
            try
            {
                int page = 1;
                var pageResult = 20f;
                var pageCount = Math.Ceiling(context.Orders.Count() / pageResult);

                var order = await context.Orders
                    .Where(x => x.UserId == userId && x.OrderStatusId == 1 || x.OrderItems.Sum(item => item.Amount * item.Quantity) != 0)
                    .OrderByDescending(x => x.CreatedAt)
                    .Include(x => x.OrderItems)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .Select(x => new OrderResponse
                    {
                        Id = x.Id,
                        SenderName = x.SenderName,
                        RecieverName = x.RecieverName,
                        ServiceTypeId = x.ServiceType.ServiceType,
                        FoodStatus = x.PriceStatus.PriceStatus,
                        PaymentMethod = x.PaymentMethod,
                        PickupTime = x.PickupTime,
                        Location = x.Location,
                        OrderStatusId = x.OrderStatus.Status,
                        Price = x.OrderItems.Sum(item => item.Amount * item.Quantity)
                    }).FirstOrDefaultAsync();

                if (order != null && order.ServiceTypeId == "Delivery")
                {
                    order.Price += 200;
                }

                var response = new OrderListResponse
                {
                    Orders = new List<OrderResponse> { order },
                    Pages = 1,
                    CurrentPage = 1
                };

                return response ?? throw new KeyNotFoundException("Orders not found");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderListResponse> GetOrdersForDelevery(int page)
        {
            try
            {
                var pageResult = 20f;
                var pageCount = Math.Ceiling(context.Orders.Count() / pageResult);

                var orders = await context.Orders
                    .Include(x => x.OrderItems)
                    .ThenInclude(x => x.FoodItem)
                    .ThenInclude(x => x.FoodSupplier)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .Where(x => (x.ServiceTypeId == 1 || x.ServiceTypeId == 3) && x.OrderStatusId == 11)
                    .Select(x => new OrderResponse
                    {
                        Id = x.Id,
                        SenderName = x.SenderName,
                        RecieverName = x.RecieverName,
                        ServiceTypeId = x.ServiceType.ServiceType,
                        PaymentMethod = x.PaymentMethod,
                        PickupTime = x.PickupTime,
                        Location = x.Location,
                        OrderStatusId = x.OrderStatus.Status,
                        FoodSupplierName = x.OrderItems.Where(item => item.OrderId == x.Id).Select(item => item.FoodItem).FirstOrDefault()!.FoodSupplier.SuplierName,
                        FoodSupplierAddress = x.OrderItems.Where(item => item.OrderId == x.Id).Select(item => item.FoodItem).FirstOrDefault()!.FoodSupplier.Location,
                        CustomerContactNo = context.Users.Where(u => u.Id == x.UserId).Select(u => u.PhoneNumber).FirstOrDefault(),
                        Price = x.OrderItems.Sum(item => item.Amount * item.Quantity)
                    }).ToListAsync();

                var response = new OrderListResponse
                {
                    Orders = orders,
                    Pages = (int)pageCount,
                    CurrentPage = page
                };

                return response ?? throw new KeyNotFoundException("Orders not found");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderListResponse> GetOrdersDonors(int page)
        {
            try
            {
                var pageResult = 20f;
                var pageCount = Math.Ceiling(context.Orders.Count() / pageResult);

                var orders = await context.Orders
                    .Include(x => x.OrderItems)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .Where(x => x.PriceStatusId == 2 && x.OrderStatusId == 1)
                    .Select(x => new OrderResponse
                    {
                        Id = x.Id,
                        SenderName = x.SenderName,
                        RecieverName = x.RecieverName,
                        ServiceTypeId = x.ServiceType.ServiceType,
                        PaymentMethod = x.PaymentMethod,
                        PickupTime = x.PickupTime,
                        Location = x.Location,
                        OrderStatusId = x.OrderStatus.Status,
                        Price = x.OrderItems.Sum(item => item.Amount)
                    }).ToListAsync();

                foreach (var item in orders)
                {
                    if (item.ServiceTypeId == "Delivery")
                    {
                        item.Price += 200;
                    }
                }

                var response = new OrderListResponse
                {
                    Orders = orders,
                    Pages = (int)pageCount,
                    CurrentPage = page
                };

                return response ?? throw new KeyNotFoundException("Orders not found");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderListResponse> GetOrdersByFoodSupplier(Guid supplierId, int page)
        {
            try
            {
                var pageResult = 20f;
                var pageCount = Math.Ceiling(context.FoodItems.Count() / pageResult);

                var orderItems = await context.OrderItems
                    .Where(x => x.FoodItem.FoodSupplier.UserId == supplierId)
                    .Include(x => x.FoodItem)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .Select(x => new OrderResponse
                    {
                        Id = x.Order.Id,
                        FoodCategory = x.FoodItem.Category,
                        SenderName = x.Order.SenderName,
                        RecieverName = x.Order.RecieverName,
                        ServiceTypeId = x.Order.ServiceType.ServiceType,
                        PaymentMethod = x.Order.PaymentMethod,
                        PickupTime = x.Order.PickupTime,
                        Location = x.Order.Location,
                        Price = x.FoodItem.Price,
                        Qty = x.Order.OrderItems.Where(item => item.FoodItemId == x.FoodItemId).First().Quantity,
                        Total = x.Order.OrderItems.Sum(item => item.Amount),
                        OrderStatusId = x.Order.OrderStatus.Status
                    }).ToListAsync();

                //var orderItemsNames = await context.OrderItems.Where(x => x.OrderId == orderItems.Select(x => x.Id).FirstOrDefault()).Select(x => x.FoodItem.Category).ToListAsync();

                //string foodNames = "";

                //foreach (var item in orderItemsNames)
                //{
                //    foodNames += $"{item}, ";

                //    //foreach (var oitem in orderItems)
                //    //{
                //    //    oitem.FoodCategory = foodNames;
                //    //}

                //    for (int i = 0; i < orderItems.Count(); i++)
                //    {
                //        orderItems[i].FoodCategory = foodNames;
                //    }
                //}

                var response = new OrderListResponse
                {
                    Orders = orderItems,
                    Pages = (int)pageCount,
                    CurrentPage = page
                };

                return response ?? throw new KeyNotFoundException("Orders not found");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Cart(OrderRequest request, Guid userId)
        {
            decimal deliveryFee = 0;

            if (request == null)
            {
                throw new KeyNotFoundException("No food items found");
            }

            var existingFoodIds = await context.FoodItems
                                          .Where(x => request.FoodItems.Select(item => item.Id).Contains(x.Id))
                                          .Select(x => x.Id)
                                          .ToListAsync();

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                if (request.ServiceTypeId == 1)
                {
                    deliveryFee = 200;
                }

                foreach (var item in request.FoodItems)
                {
                    if (!existingFoodIds.Contains(item.Id))
                    {
                        throw new KeyNotFoundException("Food not found");
                    }

                    var cart = new Cart
                    {
                        UserId = userId,
                        FoodItemId = item.Id,
                        ServiceTypeId = request.ServiceTypeId,
                        Quantity = item.Quantity,
                        Amount = item.Amount,
                        DeliveryFee = deliveryFee,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false
                    };

                    await context.Carts.AddAsync(cart);
                    await context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<CartResponse> GetCart(Guid userId)
        {
            var foodList = await context.Carts
                .Where(x => x.UserId == userId && x.IsDeleted == false)
                .Include(x => x.FoodItem)
                .Select(x => new FoodList
                {
                    FoodItemId = x.FoodItemId,
                    FoodItemName = x.FoodItem.Category,
                    Quantity = x.Quantity,
                    ServiceTypeId = x.FoodItem.ServiceTypeId,
                    Amount = x.Amount
                }).ToListAsync();


            decimal total = 0;

            foreach (var item in foodList)
            {
                total += item.Amount * item.Quantity;
            }

            var cart = await context.Carts.Where(x => x.UserId == userId && x.IsDeleted == false && x.ServiceTypeId == 1).FirstOrDefaultAsync();

            if (cart == null)
            {
                cart = await context.Carts.Where(x => x.UserId == userId && x.IsDeleted == false).FirstOrDefaultAsync();
            }

            var supplierName = await context.FoodItems.Include(x => x.FoodSupplier).Where(x => x.Id == cart.FoodItemId).Select(x => x.FoodSupplier.SuplierName).FirstOrDefaultAsync();

            if (cart.DeliveryFee != 0)
            {
                total += cart.DeliveryFee;
            }

            return new CartResponse
            {
                FoodList = foodList,
                SupplierName = supplierName,
                Total = total
            };
        }

        public async Task<bool> RemoveFromCart(Guid foodItemId, Guid userId)
        {
            var cart = await context.Carts.FirstOrDefaultAsync(x => x.FoodItemId == foodItemId && x.UserId == userId && x.IsDeleted == false);

            if (cart == null)
            {
                throw new KeyNotFoundException("Food not found in cart");
            }

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                cart.IsDeleted = true;
                context.Carts.Update(cart);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
