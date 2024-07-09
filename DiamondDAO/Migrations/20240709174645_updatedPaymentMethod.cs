using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiamondDAO.Migrations
{
    /// <inheritdoc />
    public partial class updatedPaymentMethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Payment_PaymentMethodId",
                table: "Payment",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_PaymentMethod_PaymentMethodId",
                table: "Payment",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "PaymentMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_PaymentMethod_PaymentMethodId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_PaymentMethodId",
                table: "Payment");
        }
    }
}
