using System.ComponentModel.DataAnnotations;

namespace ReadingIsGood.Models.RequestModels;

public class BookRequestModel
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; set; }
}