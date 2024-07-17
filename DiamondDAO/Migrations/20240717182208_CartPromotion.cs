using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiamondDAO.Migrations
{
    /// <inheritdoc />
    public partial class CartPromotion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartPromotion",
                columns: table => new
                {
                    CartPromotionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    PromotionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartPromotion", x => x.CartPromotionId);
                    table.ForeignKey(
                        name: "FK_CartPromotion_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartPromotion_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartPromotion_CartId",
                table: "CartPromotion",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartPromotion_PromotionId",
                table: "CartPromotion",
                column: "PromotionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartPromotion");
        }
    }
}
