using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Surplus.Food.Distribution.Chain.Data;
using Surplus.Food.Distribution.Chain.Models.DbModels;
using Surplus.Food.Distribution.Chain.Models.ResponseModels;
using Surplus.Food.Distribution.Chain.Seeds;
using Surplus.Food.Distribution.Chain.Services.Concrete;
using Surplus.Food.Distribution.Chain.Services.Interface;
using System.Text;

namespace Surplus.Food.Distribution.Chain.Extension
{
    public static class ServiceExtension
    {
        public static void DatabaseExtention(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                                         new MySqlServerVersion(new Version(8, 0, 32))));

            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();
        }

        public static void Authentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.RequireHttpsMetadata = false;
                configureOptions.SaveToken = true;
                configureOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidIssuer = configuration["ApiSettings:Issuer"],
                    ValidateAudience = false,
                    ValidAudience = configuration["ApiSettings:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["ApiSettings:SecretKey"]!))
                };

                configureOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.NoResult();
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/plain";
                        return context.Response.WriteAsync(context.Exception.ToString());
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new BaseResponse<string>("You are not Authorized"));
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new BaseResponse<string>("You are not authorized to access this resource"));
                        return context.Response.WriteAsync(result);
                    },
                };
            });
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IReferenceTypeService, ReferenceTypeServices>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IFoodSupplierService, FoodSupplierService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IDonorService, DonorService>();
        }

        public static async Task DatabaseSeedsAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedDefaultRoles.SeedAsync(roleManager);
            await SeedPriceStatus.SeedAsync(context);
            await SeedServiceType.SeedAsync(context);
            await SeedOrderStatus.SeedAsync(context);
            await SeedGuest.SeedAsync(userManager);
            await SeedDefasultAdmin.SeedAsync(userManager);
        }

        public static async Task EnsureMigrationOfContextAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var scopedProvider = scope.ServiceProvider;
            var context = scopedProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();
        }
    }
}
