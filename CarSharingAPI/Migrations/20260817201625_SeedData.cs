using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarSharingAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "user1-id", 0, "ace51aec-fdd3-4b70-8079-0451663b2193", null, false, false, null, null, "JAN", "AQAAAAIAAYagAAAAEKrL0bBYT7Y1LG3g+JiYRL7fQKjFJXgCaG/lu8sfEctXIztXETNRpfvi/EB00wmhsA==", null, false, "d51786d1-701a-4f00-8a18-5fc8267a838e", false, "jan" },
                    { "user2-id", 0, "09204884-244e-4a16-8e63-f36f27b62c61", null, false, false, null, null, "PIET", "AQAAAAIAAYagAAAAELGViKZLp2cSsXpJp6KS+4+xH4kcIazd9ONgC3ijBkgSBOaSUeUCmGC08lSV+uAhPQ==", null, false, "aa636bdb-f5c9-40f7-ad3e-94fed9cef4af", false, "piet" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "Description", "FuelType", "Model", "OwnerId", "Seats", "Transmission", "Year" },
                values: new object[,]
                {
                    { 1, "Audi", "In zeer goede staat", 0, "A3", "user1-id", 5, 1, 2022 },
                    { 2, "BMW", "Luxueuse uitvoering", 0, "X3 M", "user2-id", 5, 0, 2020 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user1-id");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user2-id");
        }
    }
}
