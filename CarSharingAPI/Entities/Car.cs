using Microsoft.AspNetCore.Identity;

namespace CarSharingAPI.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Seats { get; set; }
        public FuelType FuelType { get; set; }
        public Transmission Transmission { get; set; }
        public string OwnerId { get; set; } = string.Empty;

        // Navigation properties
        public IdentityUser? Owner { get; set; }
        public ICollection<Borrowing>? Borrowings { get; set; }
    }
}
