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
                    RestaurantID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Adress = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Tel = table.Column<string>(nullable: false),
                    Web = table.Column<string>(nullable: true),
                    StarCount = table.Column<int>(nullable: false),
                    IsAccepted = table.Column<bool>(nullable: false),
                    CreatedByID = table.Column<string>(nullable: true)
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

            migrationBuilder.DropColumn(
                name: "RestaurantID",
                table: "Reviews");
        }
    }
}
