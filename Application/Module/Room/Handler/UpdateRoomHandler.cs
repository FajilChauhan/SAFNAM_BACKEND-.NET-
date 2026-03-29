using MediatR;
using SafnamBackend.Application.Module.Room.Command;
using SafnamBackend.Domain.Interface;
using RoomModel = SafnamBackend.Domain.Models.Room;

namespace SafnamBackend.Application.Module.Room.Handler
{
    public class UpdateRoomHandler : IRequestHandler<UpdateRoomCommand, string>
    {
        private readonly IRoomRepository _repo;
        private readonly IWebHostEnvironment _env;

        public UpdateRoomHandler(IRoomRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public async Task<string> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            string? imagePath = null;
            bool hasImage = false;

            var allRooms = await _repo.GetAllAsync();

            if (allRooms.Any(m => m.RoomNo == dto.RoomNo))
            {
                throw new Exception("Room No already exists");
            }

            if (dto.Image != null)
            {
                var folder = Path.Combine(_env.WebRootPath, "images");
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
                var path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await dto.Image.CopyToAsync(stream);

                imagePath = "/images/" + fileName;
                hasImage = true;
            }

            var room = new RoomModel
            {
                Id = dto.Id,
                RoomNo = dto.RoomNo,
                Type = dto.Type,
                PricePerDay = dto.PricePerDay,
                ImagePath = imagePath
            };

            await _repo.UpdateAsync(room, hasImage);

            return "Updated";
        }
    }
}
