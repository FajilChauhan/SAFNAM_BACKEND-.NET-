namespace SafnamBackend.Domain.Models
{
    public class RestaurantTable
    {
        public int Id { get; set; }
        public int TableNo { get; set; }
        public int Floor { get; set; }
        public decimal ExtraCharge { get; set; }
        public bool IsActive { get; set; }
    }

}
