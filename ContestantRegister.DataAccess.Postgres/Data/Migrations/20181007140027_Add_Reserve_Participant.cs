using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ContestantRegister.DataAccess.Migrations
{
    public partial class Add_Reserve_Participant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReserveParticipantId",
                table: "ContestRegistrations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContestRegistrations_ReserveParticipantId",
                table: "ContestRegistrations",
                column: "ReserveParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContestRegistrations_AspNetUsers_ReserveParticipantId",
                table: "ContestRegistrations",
                column: "ReserveParticipantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestRegistrations_AspNetUsers_ReserveParticipantId",
                table: "ContestRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_ContestRegistrations_ReserveParticipantId",
                table: "ContestRegistrations");

            migrationBuilder.DropColumn(
                name: "ReserveParticipantId",
                table: "ContestRegistrations");
        }
    }
}
