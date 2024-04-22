using System.ComponentModel.DataAnnotations;

namespace ReadingIsGood.Models.RequestModels;

public class BookStockRequestModel
{
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Count is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Count must be greater than or equal to 0")]
    public int Count { get; set; }
}