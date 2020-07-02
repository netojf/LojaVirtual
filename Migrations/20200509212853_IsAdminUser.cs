using Microsoft.EntityFrameworkCore.Migrations;

namespace LojaVirtual.Migrations
{
	public partial class IsAdminUser : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<bool>(
					name: "IsAdmin",
					table: "Users",
					nullable: false,
					defaultValue: false);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
					name: "IsAdmin",
					table: "Users");
		}
	}
}
