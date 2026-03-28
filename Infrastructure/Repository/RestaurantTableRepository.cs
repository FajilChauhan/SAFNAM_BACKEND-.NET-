using Dapper;
using SafnamBackend.Data;
using SafnamBackend.Domain.Interface;
using SafnamBackend.Domain.Models;
using SafnamBackend.Infrastructure.Query;

namespace SafnamBackend.Infrastructure.Repository
{
    public class RestaurantTableRepository : IRestaurantTableRepository
    {
        private readonly DapperContext _context;

        public RestaurantTableRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RestaurantTable>> GetAllAsync()
        {
            using var con = _context.CreateConnection();
            return await con.QueryAsync<RestaurantTable>(RestaurantTableQueries.GetAll);
        }

        public async Task<RestaurantTable?> GetByIdAsync(int id)
        {
            using var con = _context.CreateConnection();
            return await con.QueryFirstOrDefaultAsync<RestaurantTable>(
                RestaurantTableQueries.GetById, new { Id = id });
        }

        public async Task<int> CreateAsync(RestaurantTable table)
        {
            using var con = _context.CreateConnection();
            return await con.ExecuteAsync(RestaurantTableQueries.Insert, table);
        }

        public async Task<int> UpdateAsync(RestaurantTable table)
        {
            using var con = _context.CreateConnection();
            return await con.ExecuteAsync(RestaurantTableQueries.Update, table);
        }

        public async Task<int> DeleteAsync(int id)
        {
            using var con = _context.CreateConnection();
            return await con.ExecuteAsync(RestaurantTableQueries.Delete, new { Id = id });
        }
    }
}