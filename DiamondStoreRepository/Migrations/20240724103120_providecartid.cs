using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiamondStoreRepository.Migrations
{
    /// <inheritdoc />
    public partial class providecartid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_User_UserId",
                table: "Cart");

            migrationBuilder.DropTable(
                name: "PaymentDiamond");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Cart",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CartId",
                table: "Payment",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_User_UserId",
                table: "Cart",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Cart_CartId",
                table: "Payment",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_User_UserId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Cart_CartId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_CartId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Payment");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Cart",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "PaymentDiamond",
                columns: table => new
                {
                    PaymentDiamondId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiamondId = table.Column<int>(type: "int", nullable: true),
                    PaymentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDiamond", x => x.PaymentDiamondId);
                    table.ForeignKey(
                        name: "FK_PaymentDiamond_Diamond_DiamondId",
                        column: x => x.DiamondId,
                        principalTable: "Diamond",
                        principalColumn: "DiamondId");
                    table.ForeignKey(
                        name: "FK_PaymentDiamond_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDiamond_DiamondId",
                table: "PaymentDiamond",
                column: "DiamondId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDiamond_PaymentId",
                table: "PaymentDiamond",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_User_UserId",
                table: "Cart",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
