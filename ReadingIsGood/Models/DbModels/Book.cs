namespace ReadingIsGood.Models.DbModels;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    public ICollection<Stock> Stocks { get; set; }
}