using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodly_new.Data.Migrations
{
    public partial class profileImageAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfileImageData",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageData",
                table: "AspNetUsers");
        }
    }
}
