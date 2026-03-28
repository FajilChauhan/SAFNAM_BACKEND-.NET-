using Dapper;
using SafnamBackend.Data;
using SafnamBackend.Domain.Interface;
using SafnamBackend.Domain.Models;
using SafnamBackend.Infrastructure.Query;

namespace SafnamBackend.Infrastructure.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DapperContext _c;

        public RoomRepository(DapperContext c)
        {
            _c = c;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            using var con = _c.CreateConnection();
            return await con.QueryAsync<Room>(RoomQueries.GetAll);
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            using var con = _c.CreateConnection();
            return await con.QueryFirstOrDefaultAsync<Room>(
                "SELECT * FROM Rooms WHERE Id=@Id",
                new { Id = id });
        }

        public async Task<int> CreateAsync(Room room)
        {
            using var con = _c.CreateConnection();
            return await con.ExecuteAsync(RoomQueries.Insert, room);
        }

        public async Task<int> UpdateAsync(Room room, bool hasImage)
        {
            using var con = _c.CreateConnection();

            return hasImage
                ? await con.ExecuteAsync(RoomQueries.UpdateWithImage, room)
                : await con.ExecuteAsync(RoomQueries.UpdateWithoutImage, room);
        }

        public async Task<int> DeleteAsync(int id)
        {
            using var con = _c.CreateConnection();
            return await con.ExecuteAsync(RoomQueries.Delete, new { Id = id });
        }

        public async Task<int> GetBookingCountAsync(int roomId)
        {
            using var con = _c.CreateConnection();
            return await con.ExecuteScalarAsync<int>(RoomQueries.GetBookingCount, new { Id = roomId });
        }
    }
}
