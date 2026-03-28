using MediatR;
using SafnamBackend.Application.Module.Room.Command;
using SafnamBackend.Domain.Interface;
using RoomModel = SafnamBackend.Domain.Models.Room;

namespace SafnamBackend.Application.Module.Room.Handler
{
    public class CreateRoomHandler : IRequestHandler<CreateRoomCommand, string>
    {
        private readonly IRoomRepository _repo;
        private readonly IWebHostEnvironment _env;

        public CreateRoomHandler(IRoomRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public async Task<string> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            if (dto.Image == null)
                throw new Exception("Image required");

            var folder = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
            var path = Path.Combine(folder, fileName);

            using var stream = new FileStream(path, FileMode.Create);
            await dto.Image.CopyToAsync(stream);

            var room = new RoomModel
            {
                RoomNo = dto.RoomNo,
                Type = dto.Type,
                PricePerDay = dto.PricePerDay,
                ImagePath = "/images/" + fileName
            };

            await _repo.CreateAsync(room);

            return "Room Created";
        }
    }
}
