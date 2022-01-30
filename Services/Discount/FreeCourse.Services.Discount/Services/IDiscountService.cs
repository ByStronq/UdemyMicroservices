using FreeCourse.Services.Discount.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<NoContent>> AddAsync(DiscountCreateDto discountCreateDto);
        Task<Response<NoContent>> UpdateAsync(DiscountUpdateDto discountUpdateDto);
        Task<Response<NoContent>> DeleteAsync(int discountId);
        Task<Response<List<DiscountDto>>> GetAllAsync();
        Task<Response<DiscountDto>> GetByIdAsync(int discountId);

        Task<Response<DiscountDto>> GetByCodeAndUserIdAsync(string code, string userId);
    }
}
