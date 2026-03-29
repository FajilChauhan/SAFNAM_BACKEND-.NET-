using SafnamBackend.Domain.Models;

namespace SafnamBackend.Domain.Interface
{
    public interface IRestaurantTableRepository
    {
        Task<IEnumerable<RestaurantTable>> GetAllAsync();
        Task<RestaurantTable?> GetByIdAsync(int id);
        Task<IEnumerable<RestaurantTable>> GetAllActiveAsync();
        Task<int> CreateAsync(RestaurantTable table);
        Task UpdateStatusAsync(int id, bool isActive);   
        Task<int> UpdateAsync(RestaurantTable table);
        Task<int> DeleteAsync(int id);
    }
}
