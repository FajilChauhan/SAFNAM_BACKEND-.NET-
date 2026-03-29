namespace SafnamBackend.Domain.Models;

public class Menu
{
    public int Id { get; set; }
    public string ItemName { get; set; }
    public string? ImagePath { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public string? Type { get; set; }
}
