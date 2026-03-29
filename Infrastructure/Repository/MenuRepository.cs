using Dapper;
using SafnamBackend.Data;
using SafnamBackend.Domain.Interface;
using SafnamBackend.Domain.Models;
using SafnamBackend.Infrastructure.Query;
namespace SafnamBackend.Infrastructure.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly DapperContext _context;

        public MenuRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            using var con = _context.CreateConnection();
            return await con.QueryAsync<Menu>(MenuQueries.GetAll);
        }

        public async Task<Menu?> GetByIdAsync(int id)
        {
            using var con = _context.CreateConnection();
            return await con.QueryFirstOrDefaultAsync<Menu>(
                MenuQueries.GetById,
                new { Id = id });
        }

        public async Task<IEnumerable<Menu>> GetAllActiveAsync()
        {
            using var con = _context.CreateConnection();
            return await con.QueryAsync<Menu>(MenuQueries.GetAllActive);
        }

        public async Task<int> CreateAsync(Menu menu)
        {
            using var con = _context.CreateConnection();
            return await con.ExecuteAsync(MenuQueries.Insert, menu);
        }

        public async Task UpdateStatusAsync(int id, bool isActive)
        {
            using var con = _context.CreateConnection();
            await con.ExecuteAsync(MenuQueries.UpdateStatus, new { Id = id, IsActive = isActive });
        }
        public async Task<int> UpdateAsync(Menu menu, bool hasImage)
        {
            using var con = _context.CreateConnection();

            return hasImage
                ? await con.ExecuteAsync(MenuQueries.UpdateWithImage, menu)
                : await con.ExecuteAsync(MenuQueries.UpdateWithoutImage, menu);
        }

        public async Task<int> DeleteAsync(int id)
        {
            using var con = _context.CreateConnection();
            return await con.ExecuteAsync(MenuQueries.Delete, new { Id = id });
        }

        public async Task<bool> IsUsedInOrders(int menuId)
        {
            using var con = _context.CreateConnection();

            var count = await con.ExecuteScalarAsync<int>(
                MenuQueries.IsUsedInOrder,
                new { MenuId = menuId });

            return count > 0;
        }
    }
}
