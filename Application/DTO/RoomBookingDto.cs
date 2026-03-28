namespace SafnamBackend.DTO
{
    public class RoomBookingDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string? Status { get; set; }
        public string? PaymentStatus { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
