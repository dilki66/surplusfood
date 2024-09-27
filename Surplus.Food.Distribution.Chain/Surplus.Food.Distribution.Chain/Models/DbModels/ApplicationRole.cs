using Microsoft.AspNetCore.Identity;

namespace Surplus.Food.Distribution.Chain.Models.DbModels
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public DateTime? CreatedDate { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
