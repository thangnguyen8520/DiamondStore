using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiamondDAO.Migrations
{
    /// <inheritdoc />
    public partial class updatedJewelry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "LaborCost",
                table: "Jewelry",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LaborCost",
                table: "Jewelry");
        }
    }
}
