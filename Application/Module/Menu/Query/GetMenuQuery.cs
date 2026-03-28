using MediatR;
using MenuModel = SafnamBackend.Domain.Models.Menu;

namespace SafnamBackend.Application.Module.Menu.Query
{
    public class GetMenuQuery : IRequest<IEnumerable<MenuModel>>
    {
    }
}
