namespace DataAccessLayer.ViewModels;

public class OwnerVM
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public string Password { get; set; } = null!;
}
