using SafnamBackend.Domain.Models;

namespace SafnamBackend.Domain.Interface
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAllAsync();
        Task<Menu?> GetByIdAsync(int id);
        Task<IEnumerable<Menu>> GetAllActiveAsync();
        Task<int> CreateAsync(Menu menu);     
        Task UpdateStatusAsync(int id, bool isActive);
        Task<int> UpdateAsync(Menu menu, bool hasImage);
        Task<int> DeleteAsync(int id);
        Task<bool> IsUsedInOrders(int menuId);
    }
}
