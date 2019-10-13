using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ContestantRegister.DataAccess.Migrations
{
    public partial class BaylorId_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBaylorRegistered",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "BaylorId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaylorId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsBaylorRegistered",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }
    }
}
