using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ContestantRegister.DataAccess.Migrations
{
    public partial class Area_At_Contest_And_Contest_Registration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Areas",
                table: "Contests",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAreaRequired",
                table: "Contests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "ContestRegistrations",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Areas",
                table: "Contests");

            migrationBuilder.DropColumn(
                name: "IsAreaRequired",
                table: "Contests");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "ContestRegistrations");
        }
    }
}
