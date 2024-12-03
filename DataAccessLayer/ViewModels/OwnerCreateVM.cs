using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.ViewModels;

public class OwnerCreateVM
{
    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Contact is required")]
    public string Contact { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;
}
