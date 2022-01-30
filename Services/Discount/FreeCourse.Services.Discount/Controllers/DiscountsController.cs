using FreeCourse.Services.Discount.Dtos;
using FreeCourse.Services.Discount.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Discount.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class DiscountsController : CustomBaseController
    {
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly IDiscountService _discountService;

        public DiscountsController(
            ISharedIdentityService sharedIdentityService,
            IDiscountService discountService
        ) {
            _sharedIdentityService = sharedIdentityService;
            _discountService = discountService;
        }

        [HttpGet] public async Task<IActionResult> GetAll() => CreateActionResultInstance(await _discountService.GetAllAsync());

        [HttpGet("{id}")] public async Task<IActionResult> GetById(int id) => CreateActionResultInstance(await _discountService.GetByIdAsync(id));

        [HttpGet("/api/[controller]/[action]/{code}")] public async Task<IActionResult> GetByCode(string code) => CreateActionResultInstance(await _discountService.GetByCodeAndUserIdAsync(code, _sharedIdentityService.GetUserId));

        [HttpPost] public async Task<IActionResult> Add(DiscountCreateDto discount) => CreateActionResultInstance(await _discountService.AddAsync(discount));

        [HttpPut] public async Task<IActionResult> Update(DiscountUpdateDto discount) => CreateActionResultInstance(await _discountService.UpdateAsync(discount));

        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) => CreateActionResultInstance(await _discountService.DeleteAsync(id));
    }
}
