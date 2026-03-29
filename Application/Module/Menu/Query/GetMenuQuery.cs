using MediatR;
using MenuModel = SafnamBackend.Domain.Models.Menu;

namespace SafnamBackend.Application.Module.Menu.Query
{
    public class GetMenuQuery : IRequest<IEnumerable<MenuModel>>
    {
    }

    public class GetMenuByIdQuery : IRequest<MenuModel?>
    {
        public int Id { get; set; }
    }
}
