namespace SafnamBackend.Application.DTO
{
    public class RoomDto
    {
        public int Id { get; set; } // for update
        public int RoomNo { get; set; }
        public string Type { get; set; }
        public decimal PricePerDay { get; set; }
        public IFormFile? Image { get; set; } // optional for update
    }
}
