using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiamondDAO.Migrations
{
    /// <inheritdoc />
    public partial class cart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cart_JewelryId",
                table: "Cart",
                column: "JewelryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Jewelry_JewelryId",
                table: "Cart",
                column: "JewelryId",
                principalTable: "Jewelry",
                principalColumn: "JewelryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Jewelry_JewelryId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_JewelryId",
                table: "Cart");
        }
    }
}
