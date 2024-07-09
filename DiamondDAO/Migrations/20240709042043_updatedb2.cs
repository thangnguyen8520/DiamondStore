using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiamondDAO.Migrations
{
    /// <inheritdoc />
    public partial class updatedb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diamond_DiamondPrice_DiamondPriceId",
                table: "Diamond");

            migrationBuilder.DropTable(
                name: "DiamondPrice");

            migrationBuilder.DropIndex(
                name: "IX_Diamond_DiamondPriceId",
                table: "Diamond");

            migrationBuilder.DropColumn(
                name: "DiamondPriceId",
                table: "Diamond");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiamondPriceId",
                table: "Diamond",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DiamondPrice",
                columns: table => new
                {
                    DiamondPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaratWeight = table.Column<double>(type: "float", nullable: true),
                    Clarity = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Cut = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Origin = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PricePerCarat = table.Column<double>(type: "float", nullable: true),
                    UpdateDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiamondPrice", x => x.DiamondPriceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diamond_DiamondPriceId",
                table: "Diamond",
                column: "DiamondPriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diamond_DiamondPrice_DiamondPriceId",
                table: "Diamond",
                column: "DiamondPriceId",
                principalTable: "DiamondPrice",
                principalColumn: "DiamondPriceId");
        }
    }
}
