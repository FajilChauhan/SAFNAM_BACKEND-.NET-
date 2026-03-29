using SafnamBackend.Domain.Models;

namespace SafnamBackend.Domain.Interface
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(int id);
        Task<IEnumerable<Room>> GetAllActiveAsync();
        Task<int> CreateAsync(Room room); 
        Task UpdateStatusAsync(int id, bool isActive);
        Task<int> UpdateAsync(Room room, bool hasImage);

        Task<int> DeleteAsync(int id);

        Task<int> GetBookingCountAsync(int roomId); // 🔥
    }
}
