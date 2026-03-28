using MediatR;
using SafnamBackend.Domain.Interface;
using TableBookingModel = SafnamBackend.Domain.Models.TableBooking;

namespace SafnamBackend.Application.Module.TableBooking.Handler
{
    public class GetActiveTableBookingsHandler : IRequestHandler<GetActiveTableBookingsQuery, IEnumerable<TableBookingModel>>
    {
        private readonly ITableBookingRepository _repo;

        public GetActiveTableBookingsHandler(ITableBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TableBookingModel>> Handle(GetActiveTableBookingsQuery request, CancellationToken cancellationToken)
            => await _repo.GetActiveAsync();
    }
}
