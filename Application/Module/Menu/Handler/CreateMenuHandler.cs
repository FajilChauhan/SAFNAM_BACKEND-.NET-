using MediatR;
using SafnamBackend.Application.Module.Menu.Command;
using SafnamBackend.Domain.Interface;

namespace SafnamBackend.Application.Module.Menu.Handler
{
    public class CreateMenuHandler : IRequestHandler<CreateMenuCommand, string>
    {
        private readonly IMenuRepository _repo;
        private readonly IWebHostEnvironment _env;

        public CreateMenuHandler(IMenuRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public async Task<string> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = request.Menu;
            var image = request.Image;

            // 🔥 Validation
            if (string.IsNullOrEmpty(menu.ItemName) || image == null || string.IsNullOrEmpty(menu.Type))
                throw new Exception("All fields are required");

            var folder = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            var path = Path.Combine(folder, fileName);

            using var stream = new FileStream(path, FileMode.Create);
            await image.CopyToAsync(stream);

            menu.ImagePath = "/images/" + fileName;

            await _repo.CreateAsync(menu);

            return "Menu Created";
        }
    }
}
