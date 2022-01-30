namespace FreeCourse.Services.Discount.Dtos
{
    public class DiscountCreateDto
    {
        public string UserId { get; set; }
        public decimal Rate { get; set; }
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
