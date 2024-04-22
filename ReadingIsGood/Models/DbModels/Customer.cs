namespace ReadingIsGood.Models.DbModels;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    public ICollection<Order> Orders { get; set; }
}