using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagmentAPI.Repository.Migrations
{
    public partial class OrderSet_finalPrice_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalValue",
                table: "Orders",
                newName: "TotalPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Orders",
                newName: "TotalValue");
        }
    }
}
