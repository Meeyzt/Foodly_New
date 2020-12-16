using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodly_new.Data.Migrations
{
    public partial class profilephoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageData",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Profilephoto",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profilephoto",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfileImageData",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
