namespace LeaseLink.Models;

public class Property
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal MonthlyRent { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
