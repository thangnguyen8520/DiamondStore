using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiamondStoreRepository.Migrations
{
    /// <inheritdoc />
    public partial class removecartid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Cart_CartId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_CartId",
                table: "Payment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Payment_CartId",
                table: "Payment",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Cart_CartId",
                table: "Payment",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
