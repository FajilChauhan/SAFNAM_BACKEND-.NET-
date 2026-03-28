using SafnamBackend.Domain.Models;

namespace SafnamBackend.Domain.Interface
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(int id);

        Task<int> CreateAsync(Room room);
        Task<int> UpdateAsync(Room room, bool hasImage);

        Task<int> DeleteAsync(int id);

        Task<int> GetBookingCountAsync(int roomId); // 🔥
    }
}
