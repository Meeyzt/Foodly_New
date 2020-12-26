using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodly_new.Migrations
{
    public partial class initMenus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuPhotos",
                columns: table => new
                {
                    MenuID = table.Column<string>(nullable: false),
                    Photo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPhotos", x => x.MenuID);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuID = table.Column<string>(nullable: false),
                    MenuHeader = table.Column<string>(nullable: false),
                    RestaurantName = table.Column<string>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    PhotoDate = table.Column<string>(nullable: false),
                    UserID = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuPhotos");

            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
