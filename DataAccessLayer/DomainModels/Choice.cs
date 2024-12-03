namespace DataAccessLayer.DomainModels;

public class Choice
{
    public int Id { get; set; }

    public string Choice1 { get; set; } = null!;

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
