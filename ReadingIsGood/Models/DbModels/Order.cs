namespace ReadingIsGood.Models.DbModels;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public DateTime OrderDate { get; set; }

    public ICollection<Stock> Stocks { get; set; }
}