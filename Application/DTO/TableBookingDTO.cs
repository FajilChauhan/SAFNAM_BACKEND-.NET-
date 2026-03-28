namespace SafnamBackend.DTO
{
    public class TableBookingDTO
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public int UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public string? TimeSlot { get; set; }
        public string? Status { get; set; }
    }
}
