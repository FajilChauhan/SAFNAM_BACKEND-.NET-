using MediatR;
using Microsoft.AspNetCore.Http;
using MenuModel = SafnamBackend.Domain.Models.Menu;

namespace SafnamBackend.Application.Module.Menu.Command
{
    public class CreateMenuCommand : IRequest<string>
    {
        public MenuModel Menu { get; set; }
        public IFormFile Image { get; set; }
    }

    public class UpdateMenuStatusCommand : IRequest<string>
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateMenuCommand : IRequest<string>
    {
        public MenuModel Menu { get; set; }
        public IFormFile? Image { get; set; }
    }

    public class DeleteMenuCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}