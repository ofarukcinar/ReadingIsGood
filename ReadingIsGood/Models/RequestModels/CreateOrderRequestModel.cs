using System.ComponentModel.DataAnnotations;

namespace ReadingIsGood.Models.RequestModels;

public class CreateOrderRequestModel
{
    [Required(ErrorMessage = "BookId is required")]
    public List<BookCount> Books { get; set; }
}

public class BookCount
{
    [Required(ErrorMessage = "BookId is required")]
    public int BookId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Count must be greater than or equal to 1")]
    public int Count { get; set; }
}