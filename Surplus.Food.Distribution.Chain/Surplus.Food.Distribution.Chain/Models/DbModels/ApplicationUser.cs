using Microsoft.AspNetCore.Identity;

namespace Surplus.Food.Distribution.Chain.Models.DbModels
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public ICollection<Admin> Admins { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Donor> Donors { get; set; }
        public ICollection<DeliveryPerson> DeliveryPersons { get; set; }
        public ICollection<FoodSupplier> FoodSuppliers { get; set; }
    }
}
