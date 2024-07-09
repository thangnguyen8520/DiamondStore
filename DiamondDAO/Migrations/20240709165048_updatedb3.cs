using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiamondDAO.Migrations
{
    /// <inheritdoc />
    public partial class updatedb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaratWeight",
                table: "Diamond");

            migrationBuilder.DropColumn(
                name: "Clarity",
                table: "Diamond");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Diamond");

            migrationBuilder.DropColumn(
                name: "Cut",
                table: "Diamond");

            migrationBuilder.RenameColumn(
                name: "Origin",
                table: "Diamond",
                newName: "DiamondCertificate");

            migrationBuilder.AddColumn<int>(
                name: "DiamondClarityId",
                table: "Diamond",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiamondColorId",
                table: "Diamond",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiamondCutId",
                table: "Diamond",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "DiamondDiameter",
                table: "Diamond",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<float>(
                name: "DiamondPrice",
                table: "Diamond",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<double>(
                name: "DiamondWeight",
                table: "Diamond",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Diamond",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DiamondClarity",
                columns: table => new
                {
                    DiamondClarityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiamondClarityName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiamondClarity", x => x.DiamondClarityId);
                });

            migrationBuilder.CreateTable(
                name: "DiamondColor",
                columns: table => new
                {
                    DiamondColorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiamondColorName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiamondColor", x => x.DiamondColorId);
                });

            migrationBuilder.CreateTable(
                name: "DiamondCut",
                columns: table => new
                {
                    DiamondCutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiamondCutName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiamondCut", x => x.DiamondCutId);
                });

            migrationBuilder.CreateTable(
                name: "JewelryMaterial",
                columns: table => new
                {
                    JewelryMaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JewelryMaterialName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JewelryMaterial", x => x.JewelryMaterialId);
                });

            migrationBuilder.CreateTable(
                name: "JewelrySize",
                columns: table => new
                {
                    JewelrySizeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JewelrySizeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JewelrySize", x => x.JewelrySizeId);
                });

            migrationBuilder.CreateTable(
                name: "JewelryType",
                columns: table => new
                {
                    JewelryTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JewelryTypeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JewelryType", x => x.JewelryTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Jewelry",
                columns: table => new
                {
                    JewelryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JewelryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: true),
                    JewelryMaterialId = table.Column<int>(type: "int", nullable: false),
                    JewelryTypeId = table.Column<int>(type: "int", nullable: false),
                    MainDiamondId = table.Column<int>(type: "int", nullable: false),
                    SecondaryDiamondId = table.Column<int>(type: "int", nullable: true),
                    JewelrySizeId = table.Column<int>(type: "int", nullable: false),
                    JewelryPrice = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    JewelrydTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jewelry", x => x.JewelryId);
                    table.ForeignKey(
                        name: "FK_Jewelry_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jewelry_JewelryMaterial_JewelryMaterialId",
                        column: x => x.JewelryMaterialId,
                        principalTable: "JewelryMaterial",
                        principalColumn: "JewelryMaterialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jewelry_JewelrySize_JewelrySizeId",
                        column: x => x.JewelrySizeId,
                        principalTable: "JewelrySize",
                        principalColumn: "JewelrySizeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jewelry_JewelryType_JewelrydTypeId",
                        column: x => x.JewelrydTypeId,
                        principalTable: "JewelryType",
                        principalColumn: "JewelryTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MainDiamond",
                columns: table => new
                {
                    MainDiamondId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiamondId = table.Column<int>(type: "int", nullable: false),
                    JewelryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainDiamond", x => x.MainDiamondId);
                    table.ForeignKey(
                        name: "FK_MainDiamond_Diamond_DiamondId",
                        column: x => x.DiamondId,
                        principalTable: "Diamond",
                        principalColumn: "DiamondId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MainDiamond_Jewelry_JewelryId",
                        column: x => x.JewelryId,
                        principalTable: "Jewelry",
                        principalColumn: "JewelryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecondaryDiamond",
                columns: table => new
                {
                    SecondaryDiamondId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiamondId = table.Column<int>(type: "int", nullable: false),
                    JewelryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryDiamond", x => x.SecondaryDiamondId);
                    table.ForeignKey(
                        name: "FK_SecondaryDiamond_Diamond_DiamondId",
                        column: x => x.DiamondId,
                        principalTable: "Diamond",
                        principalColumn: "DiamondId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecondaryDiamond_Jewelry_JewelryId",
                        column: x => x.JewelryId,
                        principalTable: "Jewelry",
                        principalColumn: "JewelryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diamond_DiamondClarityId",
                table: "Diamond",
                column: "DiamondClarityId");

            migrationBuilder.CreateIndex(
                name: "IX_Diamond_DiamondColorId",
                table: "Diamond",
                column: "DiamondColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Diamond_DiamondCutId",
                table: "Diamond",
                column: "DiamondCutId");

            migrationBuilder.CreateIndex(
                name: "IX_Diamond_ImageId",
                table: "Diamond",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Jewelry_ImageId",
                table: "Jewelry",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Jewelry_JewelrydTypeId",
                table: "Jewelry",
                column: "JewelrydTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Jewelry_JewelryMaterialId",
                table: "Jewelry",
                column: "JewelryMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Jewelry_JewelrySizeId",
                table: "Jewelry",
                column: "JewelrySizeId");

            migrationBuilder.CreateIndex(
                name: "IX_MainDiamond_DiamondId",
                table: "MainDiamond",
                column: "DiamondId");

            migrationBuilder.CreateIndex(
                name: "IX_MainDiamond_JewelryId",
                table: "MainDiamond",
                column: "JewelryId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryDiamond_DiamondId",
                table: "SecondaryDiamond",
                column: "DiamondId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryDiamond_JewelryId",
                table: "SecondaryDiamond",
                column: "JewelryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diamond_DiamondClarity_DiamondClarityId",
                table: "Diamond",
                column: "DiamondClarityId",
                principalTable: "DiamondClarity",
                principalColumn: "DiamondClarityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Diamond_DiamondColor_DiamondColorId",
                table: "Diamond",
                column: "DiamondColorId",
                principalTable: "DiamondColor",
                principalColumn: "DiamondColorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Diamond_DiamondCut_DiamondCutId",
                table: "Diamond",
                column: "DiamondCutId",
                principalTable: "DiamondCut",
                principalColumn: "DiamondCutId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Diamond_Image_ImageId",
                table: "Diamond",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diamond_DiamondClarity_DiamondClarityId",
                table: "Diamond");

            migrationBuilder.DropForeignKey(
                name: "FK_Diamond_DiamondColor_DiamondColorId",
                table: "Diamond");

            migrationBuilder.DropForeignKey(
                name: "FK_Diamond_DiamondCut_DiamondCutId",
                table: "Diamond");

            migrationBuilder.DropForeignKey(
                name: "FK_Diamond_Image_ImageId",
                table: "Diamond");

            migrationBuilder.DropTable(
                name: "DiamondClarity");

            migrationBuilder.DropTable(
                name: "DiamondColor");

            migrationBuilder.DropTable(
                name: "DiamondCut");

            migrationBuilder.DropTable(
                name: "MainDiamond");

            migrationBuilder.DropTable(
                name: "SecondaryDiamond");

            migrationBuilder.DropTable(
                name: "Jewelry");

            migrationBuilder.DropTable(
                name: "JewelryMaterial");

            migrationBuilder.DropTable(
                name: "JewelrySize");

            migrationBuilder.DropTable(
                name: "JewelryType");

            migrationBuilder.DropIndex(
                name: "IX_Diamond_DiamondClarityId",
                table: "Diamond");

            migrationBuilder.DropIndex(
                name: "IX_Diamond_DiamondColorId",
                table: "Diamond");

            migrationBuilder.DropIndex(
                name: "IX_Diamond_DiamondCutId",
                table: "Diamond");

            migrationBuilder.DropIndex(
                name: "IX_Diamond_ImageId",
                table: "Diamond");

            migrationBuilder.DropColumn(
                name: "DiamondClarityId",
                table: "Diamond");

            migrationBuilder.DropColumn(
                name: "DiamondColorId",
                table: "Diamond");

            migrationBuilder.DropColumn(
                name: "DiamondCutId",
                table: "Diamond");

            migrationBuilder.DropColumn(
                name: "DiamondDiameter",
                table: "Diamond");

            migrationBuilder.DropColumn(
                name: "DiamondPrice",
                table: "Diamond");

            migrationBuilder.DropColumn(
                name: "DiamondWeight",
                table: "Diamond");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Diamond");

            migrationBuilder.RenameColumn(
                name: "DiamondCertificate",
                table: "Diamond",
                newName: "Origin");

            migrationBuilder.AddColumn<double>(
                name: "CaratWeight",
                table: "Diamond",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Clarity",
                table: "Diamond",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Diamond",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cut",
                table: "Diamond",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
