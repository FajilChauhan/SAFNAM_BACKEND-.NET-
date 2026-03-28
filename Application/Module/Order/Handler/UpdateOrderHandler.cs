using MediatR;
using SafnamBackend.Application.Module.Order.Command;
using SafnamBackend.Domain.Interface;

namespace SafnamBackend.Application.Module.Order.Handler
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, string>
    {
        private readonly IOrderRepository _repo;

        public UpdateOrderHandler(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            // 🔥 Works for BOTH:
            // - User updates Address
            // - Admin updates Status / PaymentStatus
            // - Any single field update

            await _repo.UpdateAsync(request.Order);

            return "Updated Successfully";
        }
    }
}
