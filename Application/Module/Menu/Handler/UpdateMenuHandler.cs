using MediatR;
using SafnamBackend.Application.Module.Menu.Command;
using SafnamBackend.Domain.Interface;

namespace SafnamBackend.Application.Module.Menu.Handler
{
    public class UpdateMenuHandler : IRequestHandler<UpdateMenuCommand, string>
    {
        private readonly IMenuRepository _repo;
        private readonly IWebHostEnvironment _env;

        public UpdateMenuHandler(IMenuRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public async Task<string> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = request.Menu;
            var image = request.Image;
            if (menu.Id <= 0)
                throw new Exception("Invalid Menu Id");

            // 🔥 DUPLICATE CHECK (IMPORTANT)
            var allMenus = await _repo.GetAllAsync();

            if (allMenus.Any(m => m.ItemName.ToLower().Trim() == menu.ItemName.ToLower().Trim()))
            {
                throw new Exception("Menu item already exists");
            }
            bool hasImage = false;

            if (image != null)
            {
                var folder = Path.Combine(_env.WebRootPath, "images");
                var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await image.CopyToAsync(stream);

                menu.ImagePath = "/images/" + fileName;
                hasImage = true;
            }

            await _repo.UpdateAsync(menu, hasImage);

            return "Updated";
        }
    }
}
