using System.ComponentModel.DataAnnotations;

namespace Surplus.Food.Distribution.Chain.Models.RequestModels
{
    public class RegisterRequest
    {
        public string? Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Phone]
        public string? ContactNo { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? Nic { get; set; }
        public string? Address { get; set; }
        public string? SuplierName { get; set; }
        public string? TrainingLicense { get; set; }
        public string? OwnerName { get; set; }
        public string? OwnerNic { get; set; }
        public string? Location { get; set; }
        public string? BrNo { get; set; }
        public string? DrivingLicense { get; set; }

        [MinLength(6)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfPassword { get; set; }
    }
}
