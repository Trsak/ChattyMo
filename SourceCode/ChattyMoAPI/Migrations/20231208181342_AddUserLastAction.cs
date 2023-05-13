using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChattyMoAPI.Migrations
{
    public partial class AddUserLastAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastAction",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastAction",
                table: "Users");
        }
    }
}
