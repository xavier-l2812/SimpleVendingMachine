using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleVendingMachine.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalPriceAndTotalQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<int>(
                name: "TotalQuantity",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Transactions",
                newName: "Amount");
        }
    }
}
