using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrashPasswordSystem.Data.Migrations
{
    public partial class InitalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrashCompany",
                columns: table => new
                {
                    CCID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CCName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrashCompany", x => x.CCID);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    PCID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PCName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.PCID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SupplierName = table.Column<string>(maxLength: 100, nullable: false),
                    SupplierAddress = table.Column<string>(maxLength: 200, nullable: true),
                    SupplierContactNumber = table.Column<string>(maxLength: 30, nullable: true),
                    SupplierEmail = table.Column<string>(maxLength: 100, nullable: true),
                    SupplierWebsite = table.Column<string>(maxLength: 100, nullable: true),
                    SupplierDateAdded = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserFirstName = table.Column<string>(maxLength: 50, nullable: true),
                    UserLastName = table.Column<string>(maxLength: 50, nullable: true),
                    UserInitials = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(maxLength: 100, nullable: true),
                    UserHash = table.Column<string>(maxLength: 100, nullable: true),
                    UserSalt = table.Column<string>(maxLength: 20, nullable: true),
                    UserDateCreated = table.Column<DateTime>(nullable: true),
                    UserActive = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PCID = table.Column<int>(nullable: false),
                    CCID = table.Column<int>(nullable: false),
                    SupplierID = table.Column<int>(nullable: false),
                    StaffID = table.Column<int>(nullable: false),
                    ProductDescription = table.Column<string>(maxLength: 100, nullable: false),
                    ProductURL = table.Column<string>(maxLength: 200, nullable: false),
                    ProductUsername = table.Column<string>(maxLength: 40, nullable: false),
                    ProductPassword = table.Column<string>(maxLength: 100, nullable: false),
                    ProductDateAdded = table.Column<DateTime>(nullable: false),
                    ProductExpiry = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_CrashCompany_CCID",
                        column: x => x.CCID,
                        principalTable: "CrashCompany",
                        principalColumn: "CCID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategory_PCID",
                        column: x => x.PCID,
                        principalTable: "ProductCategory",
                        principalColumn: "PCID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Users_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UpdateHistories",
                columns: table => new
                {
                    UHID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductID = table.Column<int>(nullable: false),
                    StaffID = table.Column<int>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdateHistories", x => x.UHID);
                    table.ForeignKey(
                        name: "FK_UpdateHistories_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CCID",
                table: "Products",
                column: "CCID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PCID",
                table: "Products",
                column: "PCID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StaffID",
                table: "Products",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierID",
                table: "Products",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_UpdateHistories_ProductID",
                table: "UpdateHistories",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UpdateHistories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "CrashCompany");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
