using Microsoft.AspNetCore.Mvc;
using Surplus.Food.Distribution.Chain.Models.RequestModels;
using Surplus.Food.Distribution.Chain.Services.Interface;

namespace Surplus.Food.Distribution.Chain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IAdminService adminService) : ControllerBase
    {

        [HttpPut("food-supplier/update/{id}")]
        public async Task<IActionResult> UpdateFoodSupplier(FoodSuplierRequest request, [FromRoute] Guid id)
        {
            var response = await adminService.UpdateFoodSupplier(request, id);

            return Ok(response);
        }

        [HttpPut("food-supplier/disable/{id}")]
        public async Task<IActionResult> DisableFoodSupplier([FromRoute] Guid id)
        {
            var response = await adminService.DesableFoodSupplier(id);

            return Ok(response);
        }

        [HttpPut("customer/disable/{id}")]
        public async Task<IActionResult> DisableCustomer([FromRoute] Guid id)
        {
            var response = await adminService.DisableCustomer(id);

            return Ok(response);
        }

        [HttpPut("donor/disable/{id}")]
        public async Task<IActionResult> DisableDonor([FromRoute] Guid id)
        {
            var response = await adminService.DisableDonor(id);

            return Ok(response);
        }
    }
}
