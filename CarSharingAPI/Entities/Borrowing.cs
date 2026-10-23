using Microsoft.AspNetCore.Identity;

namespace CarSharingAPI.Entities
{
    public class Borrowing
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public BorrowingStatus Status { get; set; }
        public int CarId { get; set; }
        public string? Message { get; set; }
        public string BorrowerId { get; set; } = string.Empty;

        // Nav props
        public Car? Car { get; set; }
        public IdentityUser? Borrower { get; set; }
    }
}
