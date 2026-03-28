using MediatR;
using SafnamBackend.Domain.Interface;
using TableBookingModel = SafnamBackend.Domain.Models.TableBooking;

namespace SafnamBackend.Application.Module.TableBooking.Handler
{
    public class GetTableBookingsByTableHandler : IRequestHandler<GetTableBookingsByTableQuery, IEnumerable<TableBookingModel>>
    {
        private readonly ITableBookingRepository _repo;

        public GetTableBookingsByTableHandler(ITableBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TableBookingModel>> Handle(GetTableBookingsByTableQuery request, CancellationToken cancellationToken)
            => await _repo.GetByTableAsync(request.TableId);
    }
}
