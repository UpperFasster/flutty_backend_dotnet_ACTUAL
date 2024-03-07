using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fluttyBackend.Domain.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "tbl_product",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tbl_product_CompanyId",
                table: "tbl_product",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_product_tbl_company_CompanyId",
                table: "tbl_product",
                column: "CompanyId",
                principalTable: "tbl_company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_product_tbl_company_CompanyId",
                table: "tbl_product");

            migrationBuilder.DropIndex(
                name: "IX_tbl_product_CompanyId",
                table: "tbl_product");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "tbl_product");
        }
    }
}
