using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiamondDAO.Migrations
{
    /// <inheritdoc />
    public partial class createDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Jewelry",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Diamond",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Jewelry");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Diamond");
        }
    }
}
