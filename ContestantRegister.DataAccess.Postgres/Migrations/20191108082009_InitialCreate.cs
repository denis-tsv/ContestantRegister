using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContestantRegister.DataAccess.Postgres.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    RegistrationEnd = table.Column<DateTime>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    ParticipantType = table.Column<int>(nullable: false),
                    ContestType = table.Column<int>(nullable: false),
                    IsArchive = table.Column<bool>(nullable: false),
                    ContestStatus = table.Column<int>(nullable: false),
                    ContestParticipationType = table.Column<int>(nullable: false),
                    IsEnglishLanguage = table.Column<bool>(nullable: false),
                    IsProgrammingLanguageNeeded = table.Column<bool>(nullable: false),
                    IsOutOfCompetitionAllowed = table.Column<bool>(nullable: false),
                    YaContestLink = table.Column<string>(maxLength: 100, nullable: true),
                    SendRegistrationEmail = table.Column<bool>(nullable: false),
                    ShowRegistrationInfo = table.Column<bool>(nullable: false),
                    ShowBaylorRegistrationStatus = table.Column<bool>(nullable: false),
                    YaContestAccountsCSV = table.Column<string>(nullable: true),
                    UsedAccountsCount = table.Column<int>(nullable: false),
                    IsAreaRequired = table.Column<bool>(nullable: false),
                    RegistrationsCount = table.Column<int>(nullable: false),
                    TrainerCount = table.Column<int>(nullable: false),
                    ShowSharpTeamNumber = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(maxLength: 100, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    Message = table.Column<string>(maxLength: 4000, nullable: false),
                    IsSended = table.Column<bool>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    SendAttempts = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CompNumber = table.Column<int>(nullable: false),
                    AreaId = table.Column<int>(nullable: true),
                    Comment = table.Column<string>(maxLength: 500, nullable: true)
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContestAreas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContestId = table.Column<int>(nullable: false),
                    AreaId = table.Column<int>(nullable: false),
                    SortingResults = table.Column<string>(maxLength: 1000, nullable: true),
                    SortingCompClassIds = table.Column<string>(maxLength: 200, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RegionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudyPlaces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(nullable: false),
                    ShortName = table.Column<string>(maxLength: 50, nullable: false),
                    FullName = table.Column<string>(maxLength: 200, nullable: false),
                    Site = table.Column<string>(maxLength: 200, nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ShortNameEnglish = table.Column<string>(maxLength: 50, nullable: true),
                    FullNameEnglish = table.Column<string>(maxLength: 200, nullable: true),
                    BaylorFullName = table.Column<string>(maxLength: 200, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true)
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    UserType = table.Column<int>(nullable: false),
                    RegistrationDateTime = table.Column<DateTime>(nullable: false),
                    RegistredById = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Surname = table.Column<string>(maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    StudyPlaceId = table.Column<int>(nullable: false),
                    EducationStartDate = table.Column<DateTime>(nullable: true),
                    EducationEndDate = table.Column<DateTime>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    StudentType = table.Column<int>(nullable: true),
                    BaylorEmail = table.Column<string>(maxLength: 100, nullable: true),
                    IsBaylorRegistrationCompleted = table.Column<bool>(nullable: false),
                    VkProfile = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_RegistredById",
                        column: x => x.RegistredById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_StudyPlaces_StudyPlaceId",
                        column: x => x.StudyPlaceId,
                        principalTable: "StudyPlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContestRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Participant1Id = table.Column<string>(nullable: true),
                    RegistrationDateTime = table.Column<DateTime>(nullable: true),
                    RegistredById = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TrainerId = table.Column<string>(nullable: false),
                    ManagerId = table.Column<string>(nullable: true),
                    StudyPlaceId = table.Column<int>(nullable: false),
                    ContestId = table.Column<int>(nullable: false),
                    ComputerName = table.Column<string>(maxLength: 50, nullable: true),
                    ProgrammingLanguage = table.Column<string>(maxLength: 100, nullable: true),
                    YaContestLogin = table.Column<string>(maxLength: 50, nullable: true),
                    YaContestPassword = table.Column<string>(maxLength: 50, nullable: true),
                    ContestAreaId = table.Column<int>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    IsOutOfCompetition = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Course = table.Column<int>(nullable: true),
                    Class = table.Column<int>(nullable: true),
                    StudentType = table.Column<int>(nullable: true),
                    Participant2Id = table.Column<string>(nullable: true),
                    Participant3Id = table.Column<string>(nullable: true),
                    ReserveParticipantId = table.Column<string>(nullable: true),
                    Trainer2Id = table.Column<string>(nullable: true),
                    Trainer3Id = table.Column<string>(nullable: true),
                    TeamName = table.Column<string>(maxLength: 128, nullable: true),
                    OfficialTeamName = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContestRegistrations_ContestAreas_ContestAreaId",
                        column: x => x.ContestAreaId,
                        principalTable: "ContestAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ContestRegistrations_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_ContestRegistrations_AspNetUsers_ReserveParticipantId",
                        column: x => x.ReserveParticipantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContestRegistrations_AspNetUsers_Trainer2Id",
                        column: x => x.Trainer2Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContestRegistrations_AspNetUsers_Trainer3Id",
                        column: x => x.Trainer3Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RegistredById",
                table: "AspNetUsers",
                column: "RegistredById");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudyPlaceId",
                table: "AspNetUsers",
                column: "StudyPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_RegionId",
                table: "Cities",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompClasses_AreaId",
                table: "CompClasses",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ContestAreas_AreaId",
                table: "ContestAreas",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ContestRegistrations_ContestAreaId",
                table: "ContestRegistrations",
                column: "ContestAreaId");

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
                name: "IX_ContestRegistrations_ReserveParticipantId",
                table: "ContestRegistrations",
                column: "ReserveParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_ContestRegistrations_Trainer2Id",
                table: "ContestRegistrations",
                column: "Trainer2Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContestRegistrations_Trainer3Id",
                table: "ContestRegistrations",
                column: "Trainer3Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudyPlaces_CityId",
                table: "StudyPlaces",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CompClasses");

            migrationBuilder.DropTable(
                name: "ContestRegistrations");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ContestAreas");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Contests");

            migrationBuilder.DropTable(
                name: "StudyPlaces");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
