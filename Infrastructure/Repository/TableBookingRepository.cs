using Dapper;
using SafnamBackend.Data;
using SafnamBackend.Domain.Interface;
using SafnamBackend.Domain.Models;
using SafnamBackend.DTO;
using SafnamBackend.Infrastructure.Query;

namespace SafnamBackend.Infrastructure.Repository
{
    public class TableBookingRepository : ITableBookingRepository
    {
        private readonly DapperContext _c;

        public TableBookingRepository(DapperContext c)
        {
            _c = c;
        }

        public async Task<IEnumerable<TableBooking>> GetAllAsync()
        {
            using var con = _c.CreateConnection();
            return await con.QueryAsync<TableBooking>(TableBookingQueries.GetAll);
        }

        public async Task<IEnumerable<TableBooking>> GetActiveAsync()
        {
            using var con = _c.CreateConnection();
            return await con.QueryAsync<TableBooking>(TableBookingQueries.GetActive);
        }

        public async Task<IEnumerable<TableBooking>> GetByUserAsync(int userId)
        {
            using var con = _c.CreateConnection();
            return await con.QueryAsync<TableBooking>(
                TableBookingQueries.GetByUser, new { UserId = userId });
        }

        public async Task<IEnumerable<TableBooking>> GetByTableAsync(int tableId)
        {
            using var con = _c.CreateConnection();
            return await con.QueryAsync<TableBooking>(
                TableBookingQueries.GetByTable, new { TableId = tableId });
        }

        public async Task<int> CheckConflictAsync(int tableId, DateTime date, string timeSlot, int? excludeId = null)
        {
            using var con = _c.CreateConnection();

            var sql = TableBookingQueries.CheckConflict;

            if (excludeId != null)
                sql += " AND Id != @Id";

            return await con.ExecuteScalarAsync<int>(sql, new
            {
                TableId = tableId,
                BookingDate = date,
                TimeSlot = timeSlot,
                Id = excludeId
            });
        }

        public async Task<int> CreateAsync(TableBookingDTO dto)
        {
            using var con = _c.CreateConnection();
            return await con.ExecuteAsync(TableBookingQueries.Insert, dto);
        }

        public async Task<int> UpdateAsync(TableBookingDTO dto)
        {
            using var con = _c.CreateConnection();
            return await con.ExecuteAsync(TableBookingQueries.Update, dto);
        }

        public async Task<int> DeleteAsync(int id)
        {
            using var con = _c.CreateConnection();
            return await con.ExecuteAsync(TableBookingQueries.Delete, new { Id = id });
        }
    }
}
