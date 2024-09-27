using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Surplus.Food.Distribution.Chain.Data;
using Surplus.Food.Distribution.Chain.Enums;
using Surplus.Food.Distribution.Chain.Models.DbModels;
using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Models.ResponseModels;
using Surplus.Food.Distribution.Chain.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Surplus.Food.Distribution.Chain.Services.Concrete
{
    public class AuthService(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IConfiguration configuration) : IAuthService
    {
        private readonly string _secretKey = configuration.GetSection("ApiSettings:SecretKey").Value!;

        public async Task<AuthenticationResponse> RegisterAsync(RegisterRequest request)
        {
            var applicationUser = userManager.FindByEmailAsync(request.Email).Result;

            if (applicationUser != null)
            {
                throw new Exception("User already exists");
            }

            var user = new ApplicationUser();
            if (request.Role == Roles.FoodSupplier.ToString())
            {
                user = new ApplicationUser
                {
                    Email = request.Email,
                    UserName = request.Email,
                    FirstName = "Food",
                    LastName = "Supplier",
                    PhoneNumber = request.ContactNo,
                    NormalizedEmail = request.Email.ToUpper(),
                    EmailConfirmed = true,
                };
            }
            else
            {
                user = new ApplicationUser
                {
                    Email = request.Email,
                    UserName = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.ContactNo,
                    NormalizedEmail = request.Email.ToUpper(),
                    EmailConfirmed = true,
                };
            }

            using var transaction = context.Database.BeginTransaction();
            try
            {
                var result = await userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.FirstOrDefault().Description);
                }
                bool isSuccess = false;

                CustomerService customerService = new(context);
                FoodSupplierService foodSupplierService = new(context);
                DeliveryService deliveryService = new(context);
                AdminService adminService = new(context);
                DonorService donorService = new(context);

                isSuccess = request.Role switch
                {
                    nameof(Roles.Admin) => await adminService.RegisterAdmin(request, user.Id),
                    nameof(Roles.Customer) => await customerService.RegisterAsync(request, user.Id),
                    nameof(Roles.FoodSupplier) => await foodSupplierService.RegisterAasync(request, user.Id),
                    nameof(Roles.DeliveryPerson) => await deliveryService.RegisterAsync(request, user.Id),
                    nameof(Roles.Guest) => await donorService.RegisterAsync(request, user.Id),
                    _ => throw new ArgumentException($"Invalid role: {request.Role}"),
                };

                if (!isSuccess)
                {
                    throw new Exception($"Failed to register {request.Email}");
                }

                var role = await roleManager.FindByNameAsync(request.Role!) ?? throw new Exception("Role does not exist");
                var roleResult = await userManager.AddToRoleAsync(user, role.Name!);
                if (!roleResult.Succeeded)
                {
                    throw new Exception(roleResult.Errors.FirstOrDefault()!.Description);
                }

                transaction.Commit();

                return new AuthenticationResponse()
                {
                    Token = null,
                    Email = request.Email,
                    Role = request.Role,
                    UserId = user.Id
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public async Task<AuthenticationResponse> LoginAsync(AuthenticationRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new KeyNotFoundException("User does not exist");
            }

            //var foodsup = await context.FoodSuppliers.FirstOrDefaultAsync(x => x.UserId == user.Id);
            //var donor = await context.Donors.FirstOrDefaultAsync(x => x.UserId == user.Id);
            //var customer = await context.Customers.FirstOrDefaultAsync(x => x.UserId == user.Id);


            var result = await userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                throw new ApplicationException("Invalid credentials");
            }

            var roles = await userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()!)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            if (roles.FirstOrDefault() == Roles.FoodSupplier.ToString())
            {
                var supplier = await context.FoodSuppliers.FirstOrDefaultAsync(x => x.UserId == user.Id);

                if (supplier is null)
                {
                    throw new KeyNotFoundException("Food supplier not found");
                }

                return new AuthenticationResponse()
                {
                    Token = tokenHandler.WriteToken(token),
                    Email = user.Email,
                    Role = roles.FirstOrDefault()!,
                    UserId = user.Id,
                    SupplierName = supplier.SuplierName,
                    SupplierId = supplier.Id
                };
            }

            if (roles.FirstOrDefault() == Roles.Customer.ToString())
            {
                var customer = await context.Customers.FirstOrDefaultAsync(x => x.UserId == user.Id);

                if (customer is null)
                {
                    throw new KeyNotFoundException("Customer not found");
                }

                return new AuthenticationResponse()
                {
                    Token = tokenHandler.WriteToken(token),
                    Email = user.Email,
                    Role = roles.FirstOrDefault()!,
                    UserId = user.Id,
                    CustomerAddress = customer.Address,
                };
            }

            return new AuthenticationResponse()
            {
                Token = tokenHandler.WriteToken(token),
                Email = user.Email,
                Role = roles.FirstOrDefault()!,
                UserId = user.Id
            };
        }
    }
}
