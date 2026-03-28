namespace SafnamBackend.Domain.Models
{
    public class TableBooking
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }

        public int TableFLoor { get; set; }
        public string? UserName { get; set; }
        public DateTime BookingDate { get; set; }
        public string? TimeSlot { get; set; }
        public string? Status { get; set; }
    }
}
