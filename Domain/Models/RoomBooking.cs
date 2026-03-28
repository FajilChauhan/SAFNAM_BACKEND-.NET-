namespace SafnamBackend.Domain.Models
{
    public class RoomBooking
    {
        public int Id { get; set; }
        public int RoomNo { get; set; }
        public string? UserName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string? Status { get; set; }
        public string? PaymentStatus { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
