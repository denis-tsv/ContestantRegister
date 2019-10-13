using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ContestantRegister.DataAccess.Migrations
{
    public partial class Add_Class_Course_StudentType_For_Registration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Class",
                table: "ContestRegistrations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Course",
                table: "ContestRegistrations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentType",
                table: "ContestRegistrations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "ContestRegistrations");

            migrationBuilder.DropColumn(
                name: "Course",
                table: "ContestRegistrations");

            migrationBuilder.DropColumn(
                name: "StudentType",
                table: "ContestRegistrations");
        }
    }
}
