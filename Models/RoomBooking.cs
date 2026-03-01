namespace SafnamBackend.Models
{
    public class RoomBooking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool Status { get; set; }
        public string? PaymentStatus { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
