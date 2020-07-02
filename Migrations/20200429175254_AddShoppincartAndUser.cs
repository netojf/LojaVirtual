using Microsoft.EntityFrameworkCore.Migrations;

namespace LojaVirtual.Migrations
{
	public partial class AddShoppincartAndUser : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
					name: "FK_Products_Categories_CategoryId",
					table: "Products");

			migrationBuilder.AlterColumn<int>(
					name: "CategoryId",
					table: "Products",
					nullable: true,
					oldClrType: typeof(int),
					oldType: "int");

			migrationBuilder.AddColumn<int>(
					name: "ShoppingCartId",
					table: "Products",
					nullable: true);

			migrationBuilder.CreateTable(
					name: "Users",
					columns: table => new
					{
						UserId = table.Column<int>(nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Name = table.Column<string>(nullable: true),
						Password = table.Column<string>(nullable: true)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Users", x => x.UserId);
					});

			migrationBuilder.CreateTable(
					name: "ShoppingCarts",
					columns: table => new
					{
						ShoppingCartId = table.Column<int>(nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						UserFk = table.Column<int>(nullable: false)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_ShoppingCarts", x => x.ShoppingCartId);
						table.ForeignKey(
											name: "FK_ShoppingCarts_Users_UserFk",
											column: x => x.UserFk,
											principalTable: "Users",
											principalColumn: "UserId",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateIndex(
					name: "IX_Products_ShoppingCartId",
					table: "Products",
					column: "ShoppingCartId");

			migrationBuilder.CreateIndex(
					name: "IX_ShoppingCarts_UserFk",
					table: "ShoppingCarts",
					column: "UserFk",
					unique: true);

			migrationBuilder.AddForeignKey(
					name: "FK_Products_Categories_CategoryId",
					table: "Products",
					column: "CategoryId",
					principalTable: "Categories",
					principalColumn: "CategoryId",
					onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
					name: "FK_Products_ShoppingCarts_ShoppingCartId",
					table: "Products",
					column: "ShoppingCartId",
					principalTable: "ShoppingCarts",
					principalColumn: "ShoppingCartId",
					onDelete: ReferentialAction.Restrict);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
					name: "FK_Products_Categories_CategoryId",
					table: "Products");

			migrationBuilder.DropForeignKey(
					name: "FK_Products_ShoppingCarts_ShoppingCartId",
					table: "Products");

			migrationBuilder.DropTable(
					name: "ShoppingCarts");

			migrationBuilder.DropTable(
					name: "Users");

			migrationBuilder.DropIndex(
					name: "IX_Products_ShoppingCartId",
					table: "Products");

			migrationBuilder.DropColumn(
					name: "ShoppingCartId",
					table: "Products");

			migrationBuilder.AlterColumn<int>(
					name: "CategoryId",
					table: "Products",
					type: "int",
					nullable: false,
					oldClrType: typeof(int),
					oldNullable: true);

			migrationBuilder.AddForeignKey(
					name: "FK_Products_Categories_CategoryId",
					table: "Products",
					column: "CategoryId",
					principalTable: "Categories",
					principalColumn: "CategoryId",
					onDelete: ReferentialAction.Cascade);
		}
	}
}
