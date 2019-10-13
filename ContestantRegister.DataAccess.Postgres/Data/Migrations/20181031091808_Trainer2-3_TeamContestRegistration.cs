using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ContestantRegister.DataAccess.Migrations
{
    public partial class Trainer23_TeamContestRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Trainer2Id",
                table: "ContestRegistrations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trainer3Id",
                table: "ContestRegistrations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContestRegistrations_Trainer2Id",
                table: "ContestRegistrations",
                column: "Trainer2Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContestRegistrations_Trainer3Id",
                table: "ContestRegistrations",
                column: "Trainer3Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContestRegistrations_AspNetUsers_Trainer2Id",
                table: "ContestRegistrations",
                column: "Trainer2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestRegistrations_AspNetUsers_Trainer3Id",
                table: "ContestRegistrations",
                column: "Trainer3Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestRegistrations_AspNetUsers_Trainer2Id",
                table: "ContestRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestRegistrations_AspNetUsers_Trainer3Id",
                table: "ContestRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_ContestRegistrations_Trainer2Id",
                table: "ContestRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_ContestRegistrations_Trainer3Id",
                table: "ContestRegistrations");

            migrationBuilder.DropColumn(
                name: "Trainer2Id",
                table: "ContestRegistrations");

            migrationBuilder.DropColumn(
                name: "Trainer3Id",
                table: "ContestRegistrations");
        }
    }
}
