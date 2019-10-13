using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ContestantRegister.DataAccess.Migrations
{
    public partial class InitialModelCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDateTime",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RegistredById",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudyPlaceId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EducationEndDate",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EducationStartDate",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSchool",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetUserClaims",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetRoleClaims",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ContestStatus = table.Column<int>(nullable: false),
                    ContestType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    Duration = table.Column<int>(nullable: false),
                    IsArchive = table.Column<bool>(nullable: false),
                    IsEnglishLanguage = table.Column<bool>(nullable: false),
                    IsProgrammingLanguageNeeded = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    ParticipantType = table.Column<int>(nullable: false),
                    RegistrationEnd = table.Column<DateTime>(nullable: false),
                    RegistrationStart = table.Column<DateTime>(nullable: false),
                    SendRegistrationEmail = table.Column<bool>(nullable: false),
                    ShowRegistrationInfo = table.Column<bool>(nullable: false),
                    UsedAccountsCount = table.Column<int>(nullable: false),
                    YaContestAccountsCSV = table.Column<string>(nullable: true),
                    YaContestLink = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudyPlaces",
                columns: table => new
                {
                    BaylorLink = table.Column<string>(maxLength: 200, nullable: true),
                    FullNameEnglish = table.Column<string>(maxLength: 200, nullable: true),
                    ShortNameEnglish = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CityId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(maxLength: 200, nullable: false),
                    ShortName = table.Column<string>(maxLength: 50, nullable: false),
                    Site = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyPlaces_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContestRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ComputerName = table.Column<string>(maxLength: 20, nullable: true),
                    ContestId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ManagerId = table.Column<string>(nullable: true),
                    Participant1Id = table.Column<string>(nullable: false),
                    ProgrammingLanguage = table.Column<string>(maxLength: 100, nullable: true),
                    RegistrationDateTime = table.Column<DateTime>(nullable: true),
                    RegistredById = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StudyPlaceId = table.Column<int>(nullable: false),
                    TrainerId = table.Column<string>(nullable: false),
                    YaContestLogin = table.Column<string>(maxLength: 20, nullable: true),
                    YaContestPassword = table.Column<string>(maxLength: 20, nullable: true),
                    Participant2Id = table.Column<string>(nullable: true),
                    Participant3Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContestRegistrations_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContestRegistrations_AspNetUsers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContestRegistrations_AspNetUsers_Participant1Id",
                        column: x => x.Participant1Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContestRegistrations_AspNetUsers_RegistredById",
                        column: x => x.RegistredById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContestRegistrations_StudyPlaces_StudyPlaceId",
                        column: x => x.StudyPlaceId,
                        principalTable: "StudyPlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContestRegistrations_AspNetUsers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContestRegistrations_AspNetUsers_Participant2Id",
                        column: x => x.Participant2Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContestRegistrations_AspNetUsers_Participant3Id",
                        column: x => x.Participant3Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RegistredById",
                table: "AspNetUsers",
                column: "RegistredById");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudyPlaceId",
                table: "AspNetUsers",
                column: "StudyPlaceId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContestRegistrations_ContestId",
                table: "ContestRegistrations",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_ContestRegistrations_ManagerId",
                table: "ContestRegistrations",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ContestRegistrations_Participant1Id",
                table: "ContestRegistrations",
                column: "Participant1Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContestRegistrations_RegistredById",
                table: "ContestRegistrations",
                column: "RegistredById");

            migrationBuilder.CreateIndex(
                name: "IX_ContestRegistrations_StudyPlaceId",
                table: "ContestRegistrations",
                column: "StudyPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ContestRegistrations_TrainerId",
                table: "ContestRegistrations",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ContestRegistrations_Participant2Id",
                table: "ContestRegistrations",
                column: "Participant2Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContestRegistrations_Participant3Id",
                table: "ContestRegistrations",
                column: "Participant3Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlaces_CityId",
                table: "StudyPlaces",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_RegistredById",
                table: "AspNetUsers",
                column: "RegistredById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_StudyPlaces_StudyPlaceId",
                table: "AspNetUsers",
                column: "StudyPlaceId",
                principalTable: "StudyPlaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_RegistredById",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_StudyPlaces_StudyPlaceId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ContestRegistrations");

            migrationBuilder.DropTable(
                name: "Contests");

            migrationBuilder.DropTable(
                name: "StudyPlaces");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RegistredById",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudyPlaceId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegistrationDateTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegistredById",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudyPlaceId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EducationEndDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EducationStartDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsSchool",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetUserClaims",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetRoleClaims",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
