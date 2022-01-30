using AutoMapper;
using FreeCourse.Services.Discount.Dtos;

namespace FreeCourse.Services.Discount.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Models.Discount, DiscountCreateDto>().ReverseMap();
            CreateMap<Models.Discount, DiscountUpdateDto>().ReverseMap();
            CreateMap<Models.Discount, DiscountDto>().ReverseMap();
        }
    }
}
