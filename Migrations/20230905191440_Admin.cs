using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopingStore.Migrations
{
    /// <inheritdoc />
    public partial class Admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b421e928-0613-9ebd-a64c-f10b6a706e73", null, "admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "22e40406-8a9d-2d82-912c-5d6a640ee696", 0, "f0b5e8e0-fd77-460d-adc9-afc3f45c32ab", "Admin@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEBA1O+sOmGW7v8d/YS/yNC4eopx2Y6LmsTjbS0NaCqJX4PAalRSfseN4CtUci715sg==", null, false, "1750a3fb-79f8-48e0-8edd-4d209babe1fd", false, "Admin@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b421e928-0613-9ebd-a64c-f10b6a706e73");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696");
        }
    }
}
