using System.ComponentModel.DataAnnotations;

namespace ReadingIsGood.Models.RequestModels;

public class CustomerRequestModel
{
    [Required(ErrorMessage = "Id is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; }

    [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
    [Required(ErrorMessage = "Name is required")]

    public string Name { get; set; }
}