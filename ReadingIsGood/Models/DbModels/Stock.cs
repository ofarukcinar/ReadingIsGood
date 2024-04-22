namespace ReadingIsGood.Models.DbModels;

public class Stock
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int? OrderId { get; set; }
    public Order? Order { get; set; }
    public bool Sold { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime SoldDate { get; set; }
}