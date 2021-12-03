using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services
{
    internal interface ICourseService
    {
        internal Task<Response<List<CourseDto>>> GetAllAsync();
        internal Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        internal Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);
        internal Task<Response<NoContent>> DeleteAsync(string id);
        internal Task<Response<CourseDto>> GetByIdAsync(string id);
        internal Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId);
    }
}
