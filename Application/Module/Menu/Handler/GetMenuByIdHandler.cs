using MediatR;
using SafnamBackend.Application.Module.Menu.Query;
using SafnamBackend.Domain.Interface;
using MenuModel = SafnamBackend.Domain.Models.Menu;

namespace SafnamBackend.Application.Module.Menu.Handler
{
    public class GetMenuByIdHandler : IRequestHandler<GetMenuByIdQuery, MenuModel?>
    {
        private readonly IMenuRepository _repo;

        public GetMenuByIdHandler(IMenuRepository repo)
        {
            _repo = repo;
        }

        public async Task<MenuModel?> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.Id);
        }
    }
}
