using SafnamBackend.Domain.Models;
using SafnamBackend.DTO;

namespace SafnamBackend.Domain.Interface
{
    public interface ITableBookingRepository
    {
        Task<IEnumerable<TableBooking>> GetAllAsync();
        Task<IEnumerable<TableBooking>> GetActiveAsync();
        Task<IEnumerable<TableBooking>> GetByUserAsync(int userId);
        Task<IEnumerable<TableBooking>> GetByTableAsync(int tableId);

        Task<int> CheckConflictAsync(int tableId, DateTime date, string timeSlot, int? excludeId = null);

        Task<int> CreateAsync(TableBookingDTO dto);
        Task<int> UpdateAsync(TableBookingDTO dto);

        Task<int> DeleteAsync(int id);
    }
}
