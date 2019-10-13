using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ContestantRegister.DataAccess.Migrations
{
    public partial class CompClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Areas",
                table: "Contests");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "ContestRegistrations");

            migrationBuilder.AddColumn<int>(
                name: "ContestAreaId",
                table: "ContestRegistrations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AreaId = table.Column<int>(nullable: true),
                    Comment = table.Column<string>(maxLength: 500, nullable: true),
                    CompNumber = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompClasses_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContestAreas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AreaId = table.Column<int>(nullable: false),
                    ContestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestAreas", x => x.Id);
                    table.UniqueConstraint("AK_ContestAreas_ContestId_AreaId", x => new { x.ContestId, x.AreaId });
                    table.ForeignKey(
                        name: "FK_ContestAreas_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContestAreas_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContestRegistrations_ContestAreaId",
                table: "ContestRegistrations",
                column: "ContestAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CompClasses_AreaId",
                table: "CompClasses",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ContestAreas_AreaId",
                table: "ContestAreas",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContestRegistrations_ContestAreas_ContestAreaId",
                table: "ContestRegistrations",
                column: "ContestAreaId",
                principalTable: "ContestAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestRegistrations_ContestAreas_ContestAreaId",
                table: "ContestRegistrations");

            migrationBuilder.DropTable(
                name: "CompClasses");

            migrationBuilder.DropTable(
                name: "ContestAreas");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_ContestRegistrations_ContestAreaId",
                table: "ContestRegistrations");

            migrationBuilder.DropColumn(
                name: "ContestAreaId",
                table: "ContestRegistrations");

            migrationBuilder.AddColumn<string>(
                name: "Areas",
                table: "Contests",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "ContestRegistrations",
                maxLength: 50,
                nullable: true);
        }
    }
}
