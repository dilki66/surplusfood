using System.ComponentModel.DataAnnotations;

namespace Surplus.Food.Distribution.Chain.Models.RequestModels
{
    public class AuthenticationRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }
    }
}
