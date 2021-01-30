using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodly_new.Migrations
{
    public partial class Restaurants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    RestaurantID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Adress = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Tel = table.Column<string>(nullable: false),
                    Web = table.Column<string>(nullable: true),
                    StarCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.RestaurantID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
