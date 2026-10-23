using CarSharingAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CarSharingAPI.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Borrowing> Borrowings => Set<Borrowing>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Borrowing>()
                .HasOne(b => b.Car)
                .WithMany(b => b.Borrowings)
                .HasForeignKey(b => b.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Borrowing>()
                .HasOne(b => b.Borrower)
                .WithMany()
                .HasForeignKey(b => b.BorrowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Owner)
                .WithMany()
                .HasForeignKey(c  => c.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            var hasher = new PasswordHasher<IdentityUser>();

            var user1 = new IdentityUser
            {
                Id = "user1-id",
                UserName = "jan",
                NormalizedUserName = "JAN",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            user1.PasswordHash = hasher.HashPassword(user1, "Test123!");

            var user2 = new IdentityUser
            {
                Id = "user2-id",
                UserName = "piet",
                NormalizedUserName = "PIET",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            user2.PasswordHash = hasher.HashPassword(user2, "Test123!");

            modelBuilder.Entity<IdentityUser>().HasData(user1, user2);

            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    Id = 1,
                    Brand = "Audi",
                    Model = "A3",
                    Description = "In zeer goede staat",
                    Year = 2022,
                    Seats = 5,
                    FuelType = FuelType.Petrol,
                    Transmission = Transmission.Manual,
                    OwnerId = "user1-id"
                },
                new Car
                {
                    Id = 2,
                    Brand = "BMW",
                    Model = "X3 M",
                    Description = "Luxueuse uitvoering",
                    Year = 2020,
                    Seats = 5,
                    FuelType = FuelType.Petrol,
                    Transmission = Transmission.Automatic,
                    OwnerId = "user2-id"
                }
            );
        }
    }
}
