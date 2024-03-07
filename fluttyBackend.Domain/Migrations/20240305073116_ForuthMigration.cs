using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fluttyBackend.Domain.Migrations
{
    public partial class ForuthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_cart_item",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    UtcAddedTime = table.Column<int>(type: "integer", nullable: false),
                    UtcUpdatedTime = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_cart_item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_cart_item_tbl_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_cart_item_tbl_user_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_delivery_product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "character varying(190)", maxLength: 190, nullable: false),
                    Coordinate = table.Column<string>(type: "character varying(190)", maxLength: 190, nullable: false),
                    Finished = table.Column<bool>(type: "boolean", nullable: false),
                    FullDelivered = table.Column<bool>(type: "boolean", nullable: false),
                    FinishedCause = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FullPrice = table.Column<bool>(type: "boolean", nullable: false),
                    UtcWillFinishedTime = table.Column<int>(type: "integer", nullable: false),
                    UtcAddedTime = table.Column<int>(type: "integer", nullable: false),
                    UtcUpdatedTime = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_delivery_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_delivery_product_tbl_user_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_otm_delivery_product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ByPrice = table.Column<double>(type: "double precision", nullable: false),
                    Finished = table.Column<bool>(type: "boolean", nullable: false),
                    Delivered = table.Column<bool>(type: "boolean", nullable: false),
                    UTCFinishedAt = table.Column<int>(type: "integer", nullable: false),
                    UtcAddedTime = table.Column<int>(type: "integer", nullable: false),
                    UtcUpdatedTime = table.Column<int>(type: "integer", nullable: false),
                    DeliveryOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_otm_delivery_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_otm_delivery_product_tbl_delivery_product_DeliveryOrder~",
                        column: x => x.DeliveryOrderId,
                        principalTable: "tbl_delivery_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_otm_delivery_product_tbl_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_cart_item_ProductId",
                table: "tbl_cart_item",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_cart_item_UserId",
                table: "tbl_cart_item",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_delivery_product_UserId",
                table: "tbl_delivery_product",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_otm_delivery_product_DeliveryOrderId",
                table: "tbl_otm_delivery_product",
                column: "DeliveryOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_otm_delivery_product_ProductId",
                table: "tbl_otm_delivery_product",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_cart_item");

            migrationBuilder.DropTable(
                name: "tbl_otm_delivery_product");

            migrationBuilder.DropTable(
                name: "tbl_delivery_product");
        }
    }
}
