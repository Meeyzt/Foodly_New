using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodly_new.Migrations
{
    public partial class initDatabse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentID = table.Column<string>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    Entry = table.Column<string>(nullable: false),
                    ReviewID = table.Column<string>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                });

            migrationBuilder.CreateTable(
                name: "MenuPhotos",
                columns: table => new
                {
                    PhotoID = table.Column<string>(nullable: false),
                    MenuID = table.Column<string>(nullable: false),
                    Photo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPhotos", x => x.PhotoID);
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

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<string>(nullable: false),
                    Header = table.Column<string>(nullable: false),
                    Blog = table.Column<string>(nullable: false),
                    RestaurantName = table.Column<string>(nullable: false),
                    Star = table.Column<double>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    BannerImage = table.Column<string>(nullable: false),
                    ShortCast = table.Column<string>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Publish = table.Column<bool>(nullable: false),
                    UserID = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "MenuPhotos");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
