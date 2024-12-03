namespace DataAccessLayer.DomainModels;

public class Owner
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
