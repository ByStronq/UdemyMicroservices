using AutoMapper;
using Dapper;
using FreeCourse.Services.Discount.Dtos;
using FreeCourse.Shared.Dtos;
using System.Data;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;

        public DiscountService(
            IDbConnection dbConnection,
            IMapper mapper
        ) {
            _dbConnection = dbConnection;
            _mapper = mapper;
        }

        public async Task<Response<NoContent>> AddAsync(DiscountCreateDto discountCreateDto)
        {
            var saveStatus = await _dbConnection.ExecuteAsync(
                "INSERT INTO discount(" +
                    "userid, " +
                    "rate, " +
                    "code, " +
                    "createddate, " +
                    "startdate, " +
                    "enddate) " +
                "VALUES(" +
                    "@UserId, " +
                    "@Rate, " +
                    "@Code, " +
                    "@CreatedDate, " +
                    "@StartDate, " +
                    "@EndDate)",
                _mapper.Map<Models.Discount>(
                    discountCreateDto
                ));

            return saveStatus > 0
                ? Response<NoContent>
                    .Success(204)
                : Response<NoContent>
                    .Fail("An error occurred while adding", 500);
        }

        public async Task<Response<NoContent>> UpdateAsync(DiscountUpdateDto discountUpdateDto)
        {
            bool isExistedDiscount = await _dbConnection.ExecuteScalarAsync<bool>(
                "SELECT COUNT(1) FROM discount WHERE id=@Id",
                discountUpdateDto.Id);

            if (!isExistedDiscount)
                return Response<NoContent>
                    .Fail("Discount not found", 404);

            var updateStatus = await _dbConnection.ExecuteAsync(
                "UPDATE discount SET " +
                    "userid=@UserId, " +
                    "rate=@Rate" +
                    "code=@Code, " +
                    "startdate=@StartDate, " +
                    "enddate=@EndDate " +
                "WHERE " +
                    "id=@Id",
                discountUpdateDto);

            return updateStatus > 0
                ? Response<NoContent>
                    .Success(204)
                : Response<NoContent>
                    .Fail("An error occurred while updating", 500);
        }

        public async Task<Response<NoContent>> DeleteAsync(int discountId)
        {
            bool isExistedDiscount = await _dbConnection.ExecuteScalarAsync<bool>(
                "SELECT COUNT(1) FROM discount WHERE id=@discountId",
                discountId);

            if (!isExistedDiscount)
                return Response<NoContent>
                    .Fail("Discount not found", 404);

            var deleteStatus = await _dbConnection.ExecuteAsync(
                "DELETE FROM discount " +
                "WHERE " +
                    "id=@discountId",
                discountId);

            return deleteStatus > 0
                ? Response<NoContent>
                    .Success(204)
                : Response<NoContent>
                    .Fail("An error occurred while deleting", 500);
        }

        public async Task<Response<List<DiscountDto>>> GetAllAsync()
            => Response<List<DiscountDto>>.Success(
                _mapper.Map<List<DiscountDto>>(
                    await _dbConnection.QueryAsync<Models.Discount>(
                        "SELECT * FROM discount"
                    )), 204);

        public async Task<Response<DiscountDto>> GetByIdAsync(int discountId)
        {
            var discount = await _dbConnection
                .QuerySingleOrDefaultAsync<Models.Discount>(
                    "SELECT * FROM discount WHERE id=@discountId",
                    discountId);

            return discount != null
                ? Response<DiscountDto>.Success(
                    _mapper.Map<DiscountDto>(
                        discount), 200)
                : Response<DiscountDto>
                    .Fail("Discount not found", 404);
        }

        public async Task<Response<DiscountDto>> GetByCodeAndUserIdAsync(string code, string userId)
        {
            var discount = await _dbConnection
                .QueryFirstOrDefaultAsync<Models.Discount>(
                    "SELECT * FROM discount " +
                    "WHERE " +
                        "userid=@userId AND " +
                        "code=@code", new
                    {
                        userId,
                        code
                    });

            return discount != null
                ? Response<DiscountDto>.Success(
                    _mapper.Map<DiscountDto>(
                        discount), 200)
                : Response<DiscountDto>
                    .Fail("Discount not found", 404);
        }
    }
}
