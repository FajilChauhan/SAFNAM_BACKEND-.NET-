using MediatR;
using SafnamBackend.Application.Module.Menu.Command;
using SafnamBackend.Domain.Interface;

namespace SafnamBackend.Application.Module.Menu.Handler
{
    public class DeleteMenuHandler : IRequestHandler<DeleteMenuCommand, string>
    {
        private readonly IMenuRepository _repo;

        public DeleteMenuHandler(IMenuRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            // 🔥 CHECK IF USED IN ORDER ITEMS
            var isUsed = await _repo.IsUsedInOrders(request.Id);

            if (isUsed)
                throw new Exception("❌ Cannot delete. This item is used in orders.");

            await _repo.DeleteAsync(request.Id);

            return "Deleted successfully";
        }
    }
}
