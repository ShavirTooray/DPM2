using System.ComponentModel.DataAnnotations;

namespace LeaseLink.Models
{
    public class Property
    {
        public int Id { get; set; }
        [Required, MaxLength(120)] public string Name { get; set; } = "";
        [Required, MaxLength(200)] public string Address { get; set; } = "";
        [MaxLength(100)] public string City { get; set; } = "";
        [MaxLength(100)] public string Province { get; set; } = "";
        public List<Unit> Units { get; set; } = new();
    }

    public class Unit
    {
        public int Id { get; set; }
        [Required] public int PropertyId { get; set; }
        public Property Property { get; set; } = default!;
        [Required, MaxLength(30)] public string Number { get; set; } = "";
        public decimal MonthlyRent { get; set; }
        public bool IsVacant { get; set; } = true;
        public string? Description { get; set; }
    }
}
