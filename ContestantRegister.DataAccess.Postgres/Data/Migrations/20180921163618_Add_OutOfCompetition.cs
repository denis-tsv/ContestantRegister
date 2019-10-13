using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ContestantRegister.DataAccess.Migrations
{
    public partial class Add_OutOfCompetition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOutOfCompetitionAllowed",
                table: "Contests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOutOfCompetition",
                table: "ContestRegistrations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOutOfCompetitionAllowed",
                table: "Contests");

            migrationBuilder.DropColumn(
                name: "IsOutOfCompetition",
                table: "ContestRegistrations");
        }
    }
}
