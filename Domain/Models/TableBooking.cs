namespace SafnamBackend.Domain.Models
{
    public class TableBooking
    {
        public int Id { get; set; }

        public int TableId { get; set; }
        public int TableNo { get; set; }

        public int? FLoor { get; set; }

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime BookingDate { get; set; }
        public string? TimeSlot { get; set; }
        public string? Status { get; set; }
    }
}
