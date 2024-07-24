using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiamondStoreRepository.Migrations
{
    /// <inheritdoc />
    public partial class cartpromotionandUserPromotion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartPromotion_Promotion_PromotionId",
                table: "CartPromotion");

            migrationBuilder.RenameColumn(
                name: "PromotionId",
                table: "CartPromotion",
                newName: "UserPromotionId");

            migrationBuilder.RenameIndex(
                name: "IX_CartPromotion_PromotionId",
                table: "CartPromotion",
                newName: "IX_CartPromotion_UserPromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartPromotion_UserPromotion_UserPromotionId",
                table: "CartPromotion",
                column: "UserPromotionId",
                principalTable: "UserPromotion",
                principalColumn: "UserPromotionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartPromotion_UserPromotion_UserPromotionId",
                table: "CartPromotion");

            migrationBuilder.RenameColumn(
                name: "UserPromotionId",
                table: "CartPromotion",
                newName: "PromotionId");

            migrationBuilder.RenameIndex(
                name: "IX_CartPromotion_UserPromotionId",
                table: "CartPromotion",
                newName: "IX_CartPromotion_PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartPromotion_Promotion_PromotionId",
                table: "CartPromotion",
                column: "PromotionId",
                principalTable: "Promotion",
                principalColumn: "PromotionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
