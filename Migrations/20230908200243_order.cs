using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopingStore.Migrations
{
    /// <inheritdoc />
    public partial class order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shippings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shipper_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shipper_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postal_Code = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Order_Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Order_Status = table.Column<int>(type: "int", nullable: false),
                    Payment_Id = table.Column<int>(type: "int", nullable: true),
                    Shipping_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Payments_Payment_Id",
                        column: x => x.Payment_Id,
                        principalTable: "Payments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Shippings_Shipping_Id",
                        column: x => x.Shipping_Id,
                        principalTable: "Shippings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Quantity = table.Column<int>(type: "int", nullable: false),
                    Product_Color = table.Column<int>(type: "int", nullable: false),
                    Product_Size = table.Column<int>(type: "int", nullable: true),
                    Total_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0192d6f-d2df-4cea-b76c-88c28a59002a", "AQAAAAIAAYagAAAAEJPyBFrkaPeN/KQALLa2ur+SkjVwUwV7CFRhmteqlTwAuosj3A8sVBiALm5ndPLf/w==", "b564efbb-b062-4d50-a889-f5a1ae623e2c" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Order_Id",
                table: "OrderDetails",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Product_Id",
                table: "OrderDetails",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Customer_Id",
                table: "Orders",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Payment_Id",
                table: "Orders",
                column: "Payment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Shipping_Id",
                table: "Orders",
                column: "Shipping_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Shippings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c2e825d9-379d-4692-80dd-2fd4e1e8bd45", "AQAAAAIAAYagAAAAEHa+BXJUnNe/177qFWdVkT68VdrV66Nl5vvjZUNX6slocgvWIfEmgBqleNSRj1Q77A==", "5af8dfdb-6319-446c-9048-4dfdd4675031" });
        }
    }
}
