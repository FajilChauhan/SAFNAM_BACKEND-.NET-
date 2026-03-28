using SafnamBackend.Domain.Models;
using SafnamBackend.DTO;

public interface IRoomBookingRepository
{
    Task<IEnumerable<RoomBooking>> GetAllAsync();
    Task<IEnumerable<RoomBooking>> GetByRoomAsync(int roomId);
    Task<IEnumerable<RoomBooking>> GetByUserAsync(int userId); // 🔥 history
    Task<IEnumerable<RoomBooking>> GetActiveAsync();
    Task<decimal> GetRoomPriceAsync(int roomId);

    Task<int> CreateAsync(RoomBookingDto dto, decimal totalAmount);
    Task<int> UpdateAsync(RoomBookingDto dto);

    Task<int> DeleteAsync(int id);
    Task<int> CheckConflictAsync(int roomId, DateTime checkIn, DateTime checkOut, int? excludeId = null);
}