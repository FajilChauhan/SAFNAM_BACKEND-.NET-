namespace SafnamBackend.Domain.Models
{
    public class GameDiscount
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GamePoint { get; set; }
        public int Discount { get; set; }
        public DateTime Date { get; set; }
        public bool Applied { get; set; }
    }
}
