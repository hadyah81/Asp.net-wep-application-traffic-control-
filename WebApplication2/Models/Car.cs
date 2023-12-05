namespace WebApplication2.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        // Other car properties
        public string UserId { get; set; } // Foreign key
        public Customer User { get; set; } // Navigation property
        public List<Violation> Violations { get; set; }
    }
}
