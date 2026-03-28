using Dapper;
using SafnamBackend.Data;
using SafnamBackend.Domain.Interface;
using SafnamBackend.Domain.Models;
using SafnamBackend.Infrastructure.Query;

namespace SafnamBackend.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _c;

        public OrderRepository(DapperContext c)
        {
            _c = c;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            using var con = _c.CreateConnection();
            return await con.QueryAsync<Order>(OrderQueries.GetAll);
        }

        public async Task<IEnumerable<Order>> GetPendingAsync()
        {
            using var con = _c.CreateConnection();
            return await con.QueryAsync<Order>(OrderQueries.GetPending);
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            using var con = _c.CreateConnection();
            return await con.QueryFirstOrDefaultAsync<Order>(
                OrderQueries.GetById, new { Id = id });
        }

        // 🔥 NEW
        public async Task<IEnumerable<Order>> GetByUserAsync(int userId)
        {
            using var con = _c.CreateConnection();
            return await con.QueryAsync<Order>(
                OrderQueries.GetByUser, new { UserId = userId });
        }

        // 🔥 NEW
        public async Task<IEnumerable<OrderItem>> GetItemsAsync(int orderId)
        {
            using var con = _c.CreateConnection();
            return await con.QueryAsync<OrderItem>(
                OrderQueries.GetItems, new { Id = orderId });
        }

        public async Task<int> CreateOrderAsync(Order order)
        {
            using var con = _c.CreateConnection();
            return await con.ExecuteScalarAsync<int>(
                OrderQueries.InsertOrder, order);
        }

        public async Task AddItemAsync(OrderItem item)
        {
            using var con = _c.CreateConnection();
            await con.ExecuteAsync(OrderQueries.InsertItem, item);
        }

        public async Task UpdateAsync(Order order)
        {
            using var con = _c.CreateConnection();

            // 🔥 PARTIAL UPDATE (BEST SIMPLE WAY)
            var sql = @"UPDATE Orders SET
                        Address = COALESCE(@Address, Address),
                        PaymentStatus = COALESCE(@PaymentStatus, PaymentStatus),
                        Status = COALESCE(@Status, Status)
                        WHERE Id = @Id";

            await con.ExecuteAsync(sql, order);
        }

        public async Task DeleteAsync(int id)
        {
            using var con = _c.CreateConnection();

            await con.ExecuteAsync(OrderQueries.DeleteItems, new { Id = id });
            await con.ExecuteAsync(OrderQueries.DeleteOrder, new { Id = id });
        }
    }
}