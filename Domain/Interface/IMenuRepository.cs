using SafnamBackend.Domain.Models;

namespace SafnamBackend.Domain.Interface
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAllAsync();
        Task<int> CreateAsync(Menu menu);
        Task<int> UpdateAsync(Menu menu, bool hasImage);
        Task<int> DeleteAsync(int id);
    }
}
