﻿// <auto-generated />

using ContestantRegister.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace ContestantRegister.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181031092241_TrainerCount_Contest")]
    partial class TrainerCount_Contest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("ContestantRegister.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("BaylorEmail")
                        .HasMaxLength(100);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<DateTime?>("EducationEndDate");

                    b.Property<DateTime?>("EducationStartDate");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<bool>("IsBaylorRegistrationCompleted");

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<DateTime>("RegistrationDateTime");

                    b.Property<string>("RegistredById");

                    b.Property<string>("SecurityStamp");

                    b.Property<int?>("StudentType");

                    b.Property<int>("StudyPlaceId");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int>("UserType");

                    b.Property<string>("VkProfile")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("RegistredById");

                    b.HasIndex("StudyPlaceId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ContestantRegister.Models.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("ContestantRegister.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("RegionId");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("ContestantRegister.Models.CompClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AreaId");

                    b.Property<string>("Comment")
                        .HasMaxLength(500);

                    b.Property<int>("CompNumber");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("CompClasses");
                });

            modelBuilder.Entity("ContestantRegister.Models.Contest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContestParticipationType");

                    b.Property<int>("ContestStatus");

                    b.Property<int>("ContestType");

                    b.Property<string>("Description")
                        .HasMaxLength(5000);

                    b.Property<int>("Duration");

                    b.Property<bool>("IsArchive");

                    b.Property<bool>("IsAreaRequired");

                    b.Property<bool>("IsEnglishLanguage");

                    b.Property<bool>("IsOutOfCompetitionAllowed");

                    b.Property<bool>("IsProgrammingLanguageNeeded");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("ParticipantType");

                    b.Property<DateTime>("RegistrationEnd");

                    b.Property<int>("RegistrationsCount");

                    b.Property<bool>("SendRegistrationEmail");

                    b.Property<bool>("ShowBaylorRegistrationStatus");

                    b.Property<bool>("ShowRegistrationInfo");

                    b.Property<DateTime>("Start");

                    b.Property<int>("TrainerCount");

                    b.Property<int>("UsedAccountsCount");

                    b.Property<string>("YaContestAccountsCSV");

                    b.Property<string>("YaContestLink")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Contests");
                });

            modelBuilder.Entity("ContestantRegister.Models.ContestArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AreaId");

                    b.Property<int>("ContestId");

                    b.Property<string>("SortingCompClassIds")
                        .HasMaxLength(200);

                    b.Property<string>("SortingResults")
                        .HasMaxLength(1000);

                    b.HasKey("Id");

                    b.HasAlternateKey("ContestId", "AreaId");

                    b.HasIndex("AreaId");

                    b.ToTable("ContestAreas");
                });

            modelBuilder.Entity("ContestantRegister.Models.ContestRegistration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ComputerName")
                        .HasMaxLength(50);

                    b.Property<int?>("ContestAreaId");

                    b.Property<int>("ContestId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("IsOutOfCompetition");

                    b.Property<string>("ManagerId");

                    b.Property<int>("Number");

                    b.Property<string>("Participant1Id")
                        .IsRequired();

                    b.Property<string>("ProgrammingLanguage")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("RegistrationDateTime");

                    b.Property<string>("RegistredById");

                    b.Property<int>("Status");

                    b.Property<int>("StudyPlaceId");

                    b.Property<string>("TrainerId")
                        .IsRequired();

                    b.Property<string>("YaContestLogin")
                        .HasMaxLength(50);

                    b.Property<string>("YaContestPassword")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("ContestAreaId");

                    b.HasIndex("ContestId");

                    b.HasIndex("ManagerId");

                    b.HasIndex("Participant1Id");

                    b.HasIndex("RegistredById");

                    b.HasIndex("StudyPlaceId");

                    b.HasIndex("TrainerId");

                    b.ToTable("ContestRegistrations");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ContestRegistration");
                });

            modelBuilder.Entity("ContestantRegister.Models.Email", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ChangeDate");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<bool>("IsSended");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(4000);

                    b.Property<int>("SendAttempts");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("ContestantRegister.Models.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("ContestantRegister.Models.StudyPlace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Site")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("StudyPlaces");

                    b.HasDiscriminator<string>("Discriminator").HasValue("StudyPlace");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ContestantRegister.Models.IndividualContestRegistration", b =>
                {
                    b.HasBaseType("ContestantRegister.Models.ContestRegistration");

                    b.Property<int?>("Class");

                    b.Property<int?>("Course");

                    b.Property<int?>("StudentType");

                    b.ToTable("IndividualContestRegistration");

                    b.HasDiscriminator().HasValue("IndividualContestRegistration");
                });

            modelBuilder.Entity("ContestantRegister.Models.TeamContestRegistration", b =>
                {
                    b.HasBaseType("ContestantRegister.Models.ContestRegistration");

                    b.Property<string>("OfficialTeamName")
                        .HasMaxLength(128);

                    b.Property<string>("Participant2Id")
                        .IsRequired();

                    b.Property<string>("Participant3Id")
                        .IsRequired();

                    b.Property<string>("ReserveParticipantId");

                    b.Property<string>("TeamName")
                        .HasMaxLength(128);

                    b.Property<string>("Trainer2Id");

                    b.Property<string>("Trainer3Id");

                    b.HasIndex("Participant2Id");

                    b.HasIndex("Participant3Id");

                    b.HasIndex("ReserveParticipantId");

                    b.HasIndex("Trainer2Id");

                    b.HasIndex("Trainer3Id");

                    b.ToTable("TeamContestRegistration");

                    b.HasDiscriminator().HasValue("TeamContestRegistration");
                });

            modelBuilder.Entity("ContestantRegister.Models.Institution", b =>
                {
                    b.HasBaseType("ContestantRegister.Models.StudyPlace");

                    b.Property<string>("BaylorFullName")
                        .HasMaxLength(200);

                    b.Property<string>("FullNameEnglish")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("ShortNameEnglish")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.ToTable("Institution");

                    b.HasDiscriminator().HasValue("Institution");
                });

            modelBuilder.Entity("ContestantRegister.Models.School", b =>
                {
                    b.HasBaseType("ContestantRegister.Models.StudyPlace");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.ToTable("School");

                    b.HasDiscriminator().HasValue("School");
                });

            modelBuilder.Entity("ContestantRegister.Models.ApplicationUser", b =>
                {
                    b.HasOne("ContestantRegister.Models.ApplicationUser", "RegistredBy")
                        .WithMany("RegistredByThisUser")
                        .HasForeignKey("RegistredById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ContestantRegister.Models.StudyPlace", "StudyPlace")
                        .WithMany("Users")
                        .HasForeignKey("StudyPlaceId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ContestantRegister.Models.City", b =>
                {
                    b.HasOne("ContestantRegister.Models.Region", "Region")
                        .WithMany("Cities")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ContestantRegister.Models.CompClass", b =>
                {
                    b.HasOne("ContestantRegister.Models.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId");
                });

            modelBuilder.Entity("ContestantRegister.Models.ContestArea", b =>
                {
                    b.HasOne("ContestantRegister.Models.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContestantRegister.Models.Contest", "Contest")
                        .WithMany("ContestAreas")
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContestantRegister.Models.ContestRegistration", b =>
                {
                    b.HasOne("ContestantRegister.Models.ContestArea", "ContestArea")
                        .WithMany("ContestRegistrations")
                        .HasForeignKey("ContestAreaId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ContestantRegister.Models.Contest", "Contest")
                        .WithMany("ContestRegistrations")
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContestantRegister.Models.ApplicationUser", "Manager")
                        .WithMany("ContestRegistrationsManager")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ContestantRegister.Models.ApplicationUser", "Participant1")
                        .WithMany("ContestRegistrationsParticipant1")
                        .HasForeignKey("Participant1Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ContestantRegister.Models.ApplicationUser", "RegistredBy")
                        .WithMany("ContestRegistrationsRegistredBy")
                        .HasForeignKey("RegistredById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ContestantRegister.Models.StudyPlace", "StudyPlace")
                        .WithMany("ContestRegistrations")
                        .HasForeignKey("StudyPlaceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ContestantRegister.Models.ApplicationUser", "Trainer")
                        .WithMany("ContestRegistrationsTrainer")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ContestantRegister.Models.StudyPlace", b =>
                {
                    b.HasOne("ContestantRegister.Models.City", "City")
                        .WithMany("StudyPlaces")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ContestantRegister.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ContestantRegister.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContestantRegister.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ContestantRegister.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContestantRegister.Models.TeamContestRegistration", b =>
                {
                    b.HasOne("ContestantRegister.Models.ApplicationUser", "Participant2")
                        .WithMany("ContestRegistrationsParticipant2")
                        .HasForeignKey("Participant2Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ContestantRegister.Models.ApplicationUser", "Participant3")
                        .WithMany("ContestRegistrationsParticipant3")
                        .HasForeignKey("Participant3Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ContestantRegister.Models.ApplicationUser", "ReserveParticipant")
                        .WithMany("ContestRegistrationsReserveParticipant")
                        .HasForeignKey("ReserveParticipantId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ContestantRegister.Models.ApplicationUser", "Trainer2")
                        .WithMany("ContestRegistrationsTrainer2")
                        .HasForeignKey("Trainer2Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ContestantRegister.Models.ApplicationUser", "Trainer3")
                        .WithMany("ContestRegistrationsTrainer3")
                        .HasForeignKey("Trainer3Id")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
