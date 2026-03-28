using SafnamBackend.Domain.Models;

namespace SafnamBackend.Domain.Interface
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<IEnumerable<Order>> GetPendingAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetByUserAsync(int userId); // 🔥 NEW
        Task<IEnumerable<OrderItem>> GetItemsAsync(int orderId); // 🔥 NEW

        Task<int> CreateOrderAsync(Order order);
        Task AddItemAsync(OrderItem item);

        Task UpdateAsync(Order order);

        Task DeleteAsync(int id);
    }
}