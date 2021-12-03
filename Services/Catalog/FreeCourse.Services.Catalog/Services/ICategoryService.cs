using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services
{
    internal interface ICategoryService
    {
        internal Task<Response<List<CategoryDto>>> GetAllAsync();
        internal Task<Response<CategoryDto>> CreateAsync(Category category);
        internal Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
