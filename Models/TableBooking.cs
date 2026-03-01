namespace SafnamBackend.Models
{
    public class TableBooking
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public int UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public string TimeSlot { get; set; }
        public bool Status { get; set; }
    }
}
