using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleVendingMachine.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddRelatedTransactionIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RelatedTransactionId",
                table: "Transactions",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatedTransactionId",
                table: "Transactions");
        }
    }
}
