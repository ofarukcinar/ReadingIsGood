namespace ReadingIsGood.Models.ViewModels;

public class OrderVM
{
    public int Id { get; set; }
    public List<BookVM> Books { get; set; }
    public DateTime? OrderDate { get; set; }
}