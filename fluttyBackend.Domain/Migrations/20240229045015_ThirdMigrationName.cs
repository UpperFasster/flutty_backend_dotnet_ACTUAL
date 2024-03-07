using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fluttyBackend.Domain.Migrations
{
    public partial class ThirdMigrationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_company_addition_request",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Photo = table.Column<string>(type: "text", nullable: false),
                    OwnerUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_company_addition_request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_company_addition_request_tbl_user_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "tbl_user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_company_addition_request_OwnerUserId",
                table: "tbl_company_addition_request",
                column: "OwnerUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_company_addition_request");
        }
    }
}
