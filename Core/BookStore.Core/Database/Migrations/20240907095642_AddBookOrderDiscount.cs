using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Core.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddBookOrderDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "BookOrders",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "BookOrders");
        }
    }
}
