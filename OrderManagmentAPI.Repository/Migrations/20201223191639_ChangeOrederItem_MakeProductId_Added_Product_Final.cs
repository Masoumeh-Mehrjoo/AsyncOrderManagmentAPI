using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagmentAPI.Repository.Migrations
{
    public partial class ChangeOrederItem_MakeProductId_Added_Product_Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Productid",
                table: "OrderItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Productid",
                table: "OrderItems",
                column: "Productid");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_Productid",
                table: "OrderItems",
                column: "Productid",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_Productid",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_Productid",
                table: "OrderItems");

            migrationBuilder.AlterColumn<int>(
                name: "Productid",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
