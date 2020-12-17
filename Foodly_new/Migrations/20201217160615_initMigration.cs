using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodly_new.Migrations
{
    public partial class initMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Reviews");
        }
    }
}
