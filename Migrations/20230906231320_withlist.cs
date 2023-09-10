using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopingStore.Migrations
{
    /// <inheritdoc />
    public partial class withlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserWishLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Product_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWishLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWishLists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserWishLists_Products_Product_id",
                        column: x => x.Product_id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c2e825d9-379d-4692-80dd-2fd4e1e8bd45", "AQAAAAIAAYagAAAAEHa+BXJUnNe/177qFWdVkT68VdrV66Nl5vvjZUNX6slocgvWIfEmgBqleNSRj1Q77A==", "5af8dfdb-6319-446c-9048-4dfdd4675031" });

            migrationBuilder.CreateIndex(
                name: "IX_UserWishLists_Product_id",
                table: "UserWishLists",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserWishLists_UserId",
                table: "UserWishLists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWishLists");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0b5e8e0-fd77-460d-adc9-afc3f45c32ab", "AQAAAAIAAYagAAAAEBA1O+sOmGW7v8d/YS/yNC4eopx2Y6LmsTjbS0NaCqJX4PAalRSfseN4CtUci715sg==", "1750a3fb-79f8-48e0-8edd-4d209babe1fd" });
        }
    }
}
