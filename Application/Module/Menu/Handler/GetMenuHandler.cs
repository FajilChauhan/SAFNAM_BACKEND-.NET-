using MediatR;
using SafnamBackend.Application.Module.Menu.Query;
using SafnamBackend.Domain.Interface;
using MenuModel = SafnamBackend.Domain.Models.Menu;

namespace SafnamBackend.Application.Module.Menu.Handler
{
    public class GetMenuHandler : IRequestHandler<GetMenuQuery, IEnumerable<MenuModel>>
    {
        private readonly IMenuRepository _repo;

        public GetMenuHandler(IMenuRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<MenuModel>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync();
        }
    }
}
