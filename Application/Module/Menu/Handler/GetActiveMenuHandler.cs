using MediatR;
using SafnamBackend.Application.Module.Menu.Query;
using SafnamBackend.Domain.Interface;
using MenuModel = SafnamBackend.Domain.Models.Menu;

namespace SafnamBackend.Application.Module.Menu.Handler
{
    public class GetActiveMenuHandler : IRequestHandler<GetActiveMenuQuery, IEnumerable<MenuModel>>
    {
        private readonly IMenuRepository _repo;

        public GetActiveMenuHandler(IMenuRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<MenuModel>> Handle(GetActiveMenuQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllActiveAsync();
        }
    }
}
