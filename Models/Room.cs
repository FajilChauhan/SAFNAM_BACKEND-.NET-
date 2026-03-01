using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace SafnamBackend.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNo { get; set; }
        public string Type { get; set; }
        public decimal PricePerDay { get; set; }
        public string? ImagePath { get; set; }
    }
}
