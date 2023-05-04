using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleVendingMachine.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToTransactionDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_ProductId",
                table: "TransactionDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionDetails_Products_ProductId",
                table: "TransactionDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionDetails_Products_ProductId",
                table: "TransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_TransactionDetails_ProductId",
                table: "TransactionDetails");
        }
    }
}
