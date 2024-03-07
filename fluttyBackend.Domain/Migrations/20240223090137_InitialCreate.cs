using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fluttyBackend.Domain.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Photo = table.Column<string>(type: "character varying(90)", maxLength: 90, nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    InProduction = table.Column<bool>(type: "boolean", nullable: false),
                    Blocked = table.Column<bool>(type: "boolean", nullable: false),
                    Verified = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_product_addition_request",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Photo = table.Column<string>(type: "character varying(90)", maxLength: 90, nullable: false),
                    AdditionalPhotos = table.Column<List<string>>(type: "text[]", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_product_addition_request", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_user",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(325)", maxLength: 325, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    LastName = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_user", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_otm_photo_of_product",
                columns: table => new
                {
                    PhotoId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_otm_photo_of_product", x => x.PhotoId);
                    table.ForeignKey(
                        name: "FK_tbl_otm_photo_of_product_tbl_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Photo = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    AboutCompany = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Approved = table.Column<bool>(type: "boolean", nullable: false),
                    Blocked = table.Column<bool>(type: "boolean", nullable: false),
                    FounderId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_company_tbl_user_FounderId",
                        column: x => x.FounderId,
                        principalTable: "tbl_user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_otm_company_employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_otm_company_employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_tbl_otm_company_employees_tbl_company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tbl_company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_otm_company_employees_tbl_user_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "tbl_user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_company_FounderId",
                table: "tbl_company",
                column: "FounderId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_company_Name",
                table: "tbl_company",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_otm_company_employees_CompanyId",
                table: "tbl_otm_company_employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_otm_photo_of_product_FileName",
                table: "tbl_otm_photo_of_product",
                column: "FileName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_otm_photo_of_product_ProductId",
                table: "tbl_otm_photo_of_product",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_product_Name",
                table: "tbl_product",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_product_addition_request_Name",
                table: "tbl_product_addition_request",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_user_Email",
                table: "tbl_user",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_otm_company_employees");

            migrationBuilder.DropTable(
                name: "tbl_otm_photo_of_product");

            migrationBuilder.DropTable(
                name: "tbl_product_addition_request");

            migrationBuilder.DropTable(
                name: "tbl_company");

            migrationBuilder.DropTable(
                name: "tbl_product");

            migrationBuilder.DropTable(
                name: "tbl_user");
        }
    }
}
