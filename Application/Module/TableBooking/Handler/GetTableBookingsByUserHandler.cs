using MediatR;
using SafnamBackend.Domain.Interface;
using TableBookingModel = SafnamBackend.Domain.Models.TableBooking;

namespace SafnamBackend.Application.Module.TableBooking.Handler
{
    public class GetTableBookingsByUserHandler : IRequestHandler<GetTableBookingsByUserQuery, IEnumerable<TableBookingModel>>
    {
        private readonly ITableBookingRepository _repo;

        public GetTableBookingsByUserHandler(ITableBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TableBookingModel>> Handle(GetTableBookingsByUserQuery request, CancellationToken cancellationToken)
            => await _repo.GetByUserAsync(request.UserId);
    }
}
