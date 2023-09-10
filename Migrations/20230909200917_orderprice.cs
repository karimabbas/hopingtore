using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopingStore.Migrations
{
    /// <inheritdoc />
    public partial class orderprice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f4b5808e-e6d0-4b7c-b8e8-28502894c169", "AQAAAAIAAYagAAAAEHEoI5Olc9jfMZm6NHdI8HEbHo/zJ3a8rIfT9IZJn0t+2UTlohxxksn5DGE0mi7Pfg==", "b67abb0d-0eb8-4a80-88ef-c7a9332b8c62" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00f56449-ece8-4791-bda9-adb6e48b55f6", "AQAAAAIAAYagAAAAEBp5k8EiKXMmGba1KVJ+S5B6iaMbH0+NZLUhkfx9Lc/3wFXCt5KExw0JQmSiYvu3GQ==", "4c2f8248-f059-449a-acae-83bbb058f963" });
        }
    }
}
