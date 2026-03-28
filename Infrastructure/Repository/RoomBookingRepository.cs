using Dapper;
using SafnamBackend.Data;
using SafnamBackend.Domain.Models;
using SafnamBackend.DTO;
using SafnamBackend.Infrastructure.Query;

namespace SafnamBackend.Infrastructure.Repository
{
    public class RoomBookingRepository : IRoomBookingRepository
    {
        private readonly DapperContext _c;

        public RoomBookingRepository(DapperContext c)
        {
            _c = c;
        }

        public async Task<IEnumerable<RoomBooking>> GetAllAsync()
        {
            using var con = _c.CreateConnection();
            return await con.QueryAsync<RoomBooking>(RoomBookingQueries.GetAll);
        }

        public async Task<IEnumerable<RoomBooking>> GetByRoomAsync(int roomId)
        {
            using var con = _c.CreateConnection();
            return await con.QueryAsync<RoomBooking>(
                RoomBookingQueries.GetByRoom, new { RoomId = roomId });
        }

        public async Task<IEnumerable<RoomBooking>> GetByUserAsync(int userId)
        {
            using var con = _c.CreateConnection();
            return await con.QueryAsync<RoomBooking>(
                RoomBookingQueries.GetByUser, new { UserId = userId });
        }

        public async Task<IEnumerable<RoomBooking>> GetActiveAsync()
        {
            using var con = _c.CreateConnection();
            return await con.QueryAsync<RoomBooking>(RoomBookingQueries.GetActive);
        }

        public async Task<decimal> GetRoomPriceAsync(int roomId)
        {
            using var con = _c.CreateConnection();
            return await con.ExecuteScalarAsync<decimal>(
                RoomBookingQueries.GetRoomPrice, new { Id = roomId });
        }

        public async Task<int> CreateAsync(RoomBookingDto dto, decimal totalAmount)
        {
            using var con = _c.CreateConnection();

            return await con.ExecuteAsync(RoomBookingQueries.Insert, new
            {
                dto.RoomId,
                dto.UserId,
                dto.CheckIn,
                dto.CheckOut,
                Status = "Booked",
                PaymentStatus = "Pending",
                TotalAmount = totalAmount
            });
        }

        public async Task<int> UpdateAsync(RoomBookingDto dto)
        {
            using var con = _c.CreateConnection();

            return await con.ExecuteAsync(RoomBookingQueries.Update, new
            {
                dto.RoomId,
                dto.UserId,
                dto.CheckIn,
                dto.CheckOut,
                dto.Status,
                dto.PaymentStatus,
                dto.TotalAmount,
                dto.Id
            });
        }

        public async Task<int> DeleteAsync(int id)
        {
            using var con = _c.CreateConnection();
            return await con.ExecuteAsync(RoomBookingQueries.Delete, new { Id = id });
        }

        public async Task<int> CheckConflictAsync(int roomId, DateTime checkIn, DateTime checkOut, int? excludeId = null)
        {
            using var con = _c.CreateConnection();

            var sql = RoomBookingQueries.CheckConflict;

            if (excludeId != null)
                sql += " AND Id != @Id";

            return await con.ExecuteScalarAsync<int>(sql, new
            {
                RoomId = roomId,
                CheckIn = checkIn,
                CheckOut = checkOut,
                Id = excludeId
            });
        }
    }
}
