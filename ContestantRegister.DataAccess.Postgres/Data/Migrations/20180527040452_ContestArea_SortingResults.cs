using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ContestantRegister.DataAccess.Migrations
{
    public partial class ContestArea_SortingResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortingResults",
                table: "Contests");

            migrationBuilder.AddColumn<string>(
                name: "SortingResults",
                table: "ContestAreas",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortingResults",
                table: "ContestAreas");

            migrationBuilder.AddColumn<string>(
                name: "SortingResults",
                table: "Contests",
                maxLength: 1000,
                nullable: true);
        }
    }
}
