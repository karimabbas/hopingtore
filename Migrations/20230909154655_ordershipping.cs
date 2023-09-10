using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopingStore.Migrations
{
    /// <inheritdoc />
    public partial class ordershipping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shippings_Shipping_Id",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "Shipping_Id",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00f56449-ece8-4791-bda9-adb6e48b55f6", "AQAAAAIAAYagAAAAEBp5k8EiKXMmGba1KVJ+S5B6iaMbH0+NZLUhkfx9Lc/3wFXCt5KExw0JQmSiYvu3GQ==", "4c2f8248-f059-449a-acae-83bbb058f963" });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shippings_Shipping_Id",
                table: "Orders",
                column: "Shipping_Id",
                principalTable: "Shippings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shippings_Shipping_Id",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "Shipping_Id",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0192d6f-d2df-4cea-b76c-88c28a59002a", "AQAAAAIAAYagAAAAEJPyBFrkaPeN/KQALLa2ur+SkjVwUwV7CFRhmteqlTwAuosj3A8sVBiALm5ndPLf/w==", "b564efbb-b062-4d50-a889-f5a1ae623e2c" });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shippings_Shipping_Id",
                table: "Orders",
                column: "Shipping_Id",
                principalTable: "Shippings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
