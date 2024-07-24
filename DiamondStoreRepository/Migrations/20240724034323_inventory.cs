using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiamondStoreRepository.Migrations
{
    /// <inheritdoc />
    public partial class inventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentPromotion");

            migrationBuilder.AddColumn<int>(
                name: "JewelryInventory",
                table: "Jewelry",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiamondInventory",
                table: "Diamond",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JewelryInventory",
                table: "Jewelry");

            migrationBuilder.DropColumn(
                name: "DiamondInventory",
                table: "Diamond");

            migrationBuilder.CreateTable(
                name: "PaymentPromotion",
                columns: table => new
                {
                    PaymentPromotionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<int>(type: "int", nullable: true),
                    PromotionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPromotion", x => x.PaymentPromotionId);
                    table.ForeignKey(
                        name: "FK_PaymentPromotion_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId");
                    table.ForeignKey(
                        name: "FK_PaymentPromotion_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPromotion_PaymentId",
                table: "PaymentPromotion",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPromotion_PromotionId",
                table: "PaymentPromotion",
                column: "PromotionId");
        }
    }
}
