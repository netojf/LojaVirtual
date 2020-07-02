using Microsoft.EntityFrameworkCore.Migrations;

namespace LojaVirtual.Migrations
{
	public partial class OrderModelUpdate : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
					name: "FK_Products_ShoppingCarts_ShoppingCartId",
					table: "Products");

			migrationBuilder.DropIndex(
					name: "IX_Products_ShoppingCartId",
					table: "Products");

			migrationBuilder.DropColumn(
					name: "ShoppingCartId",
					table: "Products");

			migrationBuilder.AlterColumn<string>(
					name: "Name",
					table: "Users",
					nullable: true,
					oldClrType: typeof(string),
					oldType: "nvarchar(max)",
					oldNullable: true);

			migrationBuilder.AddColumn<int>(
					name: "ProductOrderId",
					table: "Products",
					nullable: true);

			migrationBuilder.CreateTable(
					name: "productOrders",
					columns: table => new
					{
						ProductOrderId = table.Column<int>(nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Quantity = table.Column<int>(nullable: false),
						ShoppingCartId = table.Column<int>(nullable: true)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_productOrders", x => x.ProductOrderId);
						table.ForeignKey(
											name: "FK_productOrders_ShoppingCarts_ShoppingCartId",
											column: x => x.ShoppingCartId,
											principalTable: "ShoppingCarts",
											principalColumn: "ShoppingCartId",
											onDelete: ReferentialAction.Restrict);
					});

			migrationBuilder.CreateIndex(
					name: "IX_Users_Name",
					table: "Users",
					column: "Name",
					unique: true,
					filter: "[Name] IS NOT NULL");

			migrationBuilder.CreateIndex(
					name: "IX_Products_Barcode",
					table: "Products",
					column: "Barcode",
					unique: true);

			migrationBuilder.CreateIndex(
					name: "IX_Products_ProductOrderId",
					table: "Products",
					column: "ProductOrderId");

			migrationBuilder.CreateIndex(
					name: "IX_productOrders_ShoppingCartId",
					table: "productOrders",
					column: "ShoppingCartId");

			migrationBuilder.AddForeignKey(
					name: "FK_Products_productOrders_ProductOrderId",
					table: "Products",
					column: "ProductOrderId",
					principalTable: "productOrders",
					principalColumn: "ProductOrderId",
					onDelete: ReferentialAction.Restrict);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
					name: "FK_Products_productOrders_ProductOrderId",
					table: "Products");

			migrationBuilder.DropTable(
					name: "productOrders");

			migrationBuilder.DropIndex(
					name: "IX_Users_Name",
					table: "Users");

			migrationBuilder.DropIndex(
					name: "IX_Products_Barcode",
					table: "Products");

			migrationBuilder.DropIndex(
					name: "IX_Products_ProductOrderId",
					table: "Products");

			migrationBuilder.DropColumn(
					name: "ProductOrderId",
					table: "Products");

			migrationBuilder.AlterColumn<string>(
					name: "Name",
					table: "Users",
					type: "nvarchar(max)",
					nullable: true,
					oldClrType: typeof(string),
					oldNullable: true);

			migrationBuilder.AddColumn<int>(
					name: "ShoppingCartId",
					table: "Products",
					type: "int",
					nullable: true);

			migrationBuilder.CreateIndex(
					name: "IX_Products_ShoppingCartId",
					table: "Products",
					column: "ShoppingCartId");

			migrationBuilder.AddForeignKey(
					name: "FK_Products_ShoppingCarts_ShoppingCartId",
					table: "Products",
					column: "ShoppingCartId",
					principalTable: "ShoppingCarts",
					principalColumn: "ShoppingCartId",
					onDelete: ReferentialAction.Restrict);
		}
	}
}
