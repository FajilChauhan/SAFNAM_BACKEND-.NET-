using SafnamBackend.Domain.Models;

namespace SafnamBackend.Domain.Interface
{
    public interface IRestaurantTableRepository
    {
        Task<IEnumerable<RestaurantTable>> GetAllAsync();
        Task<RestaurantTable?> GetByIdAsync(int id);

        Task<int> CreateAsync(RestaurantTable table);
        Task<int> UpdateAsync(RestaurantTable table);
        Task<int> DeleteAsync(int id);
    }
}
