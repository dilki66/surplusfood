using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Surplus.Food.Distribution.Chain.Models.DbModels;

namespace Surplus.Food.Distribution.Chain.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
        ApplicationRole, Guid, IdentityUserClaim<Guid>, ApplicationUserRole,
        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<DeliveryPerson> DeliveryPersons { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<FoodSupplier> FoodSuppliers { get; set; }
        public DbSet<FoodSupplierReview> FoodSupplierReviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<RefOrderStatus> RefOrderStatuses { get; set; }
        public DbSet<RefPriceStatus> RefPriceStatuses { get; set; }
        public DbSet<RefServiceType> RefServiceTypes { get; set; }
        public DbSet<Cart> Carts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("User");
            });

            builder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable("Role");
            });

            builder.Entity<ApplicationUserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                entity.HasOne(r => r.Role)
                      .WithMany(r => r.UserRoles)
                      .HasForeignKey(r => r.RoleId)
                      .IsRequired();

                entity.HasOne(u => u.User)
                      .WithMany(u => u.UserRoles)
                      .HasForeignKey(u => u.UserId)
                      .IsRequired();

                entity.ToTable("UserRole");
            });
        }
    }
}
