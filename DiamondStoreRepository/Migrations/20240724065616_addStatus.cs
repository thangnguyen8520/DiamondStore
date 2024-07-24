using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiamondStoreRepository.Migrations
{
    /// <inheritdoc />
    public partial class addStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "PaymentMethod",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Cart",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cart");
        }
    }
}
