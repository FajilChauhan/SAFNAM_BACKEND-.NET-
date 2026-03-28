using MediatR;
using SafnamBackend.Domain.Interface;
using TableBookingModel = SafnamBackend.Domain.Models.TableBooking;

namespace SafnamBackend.Application.Module.TableBooking.Handler
{
    public class GetAllTableBookingsHandler : IRequestHandler<GetAllTableBookingsQuery, IEnumerable<TableBookingModel>>
    {
        private readonly ITableBookingRepository _repo;

        public GetAllTableBookingsHandler(ITableBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TableBookingModel>> Handle(GetAllTableBookingsQuery request, CancellationToken cancellationToken)
            => await _repo.GetAllAsync();
    }
}
