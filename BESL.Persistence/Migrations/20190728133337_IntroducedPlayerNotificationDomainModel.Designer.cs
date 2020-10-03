﻿// <auto-generated />
using System;
using BESL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BESL.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190728133337_IntroducedPlayerNotificationDomainModel")]
    partial class IntroducedPlayerNotificationDomainModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BESL.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(true);

                    b.Property<string>("GameImageUrl")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("BESL.Entities.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AwayTeamId");

                    b.Property<int>("AwayTeamScore");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int>("HomeTeamId");

                    b.Property<int>("HomeTeamScore");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsDraw");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<DateTime?>("ScheduledDate");

                    b.Property<int>("TeamTableResultId");

                    b.Property<int>("WinnerTeamId");

                    b.HasKey("Id");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("TeamTableResultId");

                    b.HasIndex("WinnerTeamId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("BESL.Entities.Notification", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .HasMaxLength(1024)
                        .IsUnicode(true);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("PlayerId");

                    b.Property<int>("Severity");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("BESL.Entities.Player", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BESL.Entities.PlayerMatch", b =>
                {
                    b.Property<string>("PlayerId");

                    b.Property<int>("MatchId");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("PlayerId", "MatchId");

                    b.HasIndex("MatchId");

                    b.ToTable("PlayerMatches");
                });

            modelBuilder.Entity("BESL.Entities.PlayerRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("BESL.Entities.PlayerTeam", b =>
                {
                    b.Property<string>("PlayerId");

                    b.Property<int>("TeamId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("PlayerId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("PlayerTeams");
                });

            modelBuilder.Entity("BESL.Entities.Setting", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("BESL.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int?>("CurrentActiveTeamTableResultId");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .IsUnicode(true);

                    b.Property<string>("HomepageUrl")
                        .HasMaxLength(256);

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(35)
                        .IsUnicode(true);

                    b.Property<string>("OwnerId");

                    b.Property<int>("TournamentFormatId");

                    b.HasKey("Id");

                    b.HasIndex("CurrentActiveTeamTableResultId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("TournamentFormatId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("BESL.Entities.TeamTableResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsDropped");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("PenaltyPoints");

                    b.Property<int>("TeamId");

                    b.Property<int>("TotalPoints");

                    b.Property<int>("TournamentTableId");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("TournamentTableId");

                    b.ToTable("TeamTableResults");
                });

            modelBuilder.Entity("BESL.Entities.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(true);

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("FormatId");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("TournamentImageUrl");

                    b.HasKey("Id");

                    b.HasIndex("FormatId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("BESL.Entities.TournamentFormat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(true);

                    b.Property<int>("GameId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<int>("TeamPlayersCount");

                    b.Property<int>("TotalPlayersCount");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("Name", "GameId")
                        .IsUnique();

                    b.ToTable("TournamentFormats");
                });

            modelBuilder.Entity("BESL.Entities.TournamentTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("MaxNumberOfTeams");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(true);

                    b.Property<int>("TournamentId");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("TournamentTables");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("BESL.Entities.Match", b =>
                {
                    b.HasOne("BESL.Entities.Team", "AwayTeam")
                        .WithMany()
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BESL.Entities.Team", "HomeTeam")
                        .WithMany()
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BESL.Entities.TeamTableResult", "TeamTableResult")
                        .WithMany("PlayedMatches")
                        .HasForeignKey("TeamTableResultId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BESL.Entities.Team", "WinnerTeam")
                        .WithMany()
                        .HasForeignKey("WinnerTeamId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BESL.Entities.Notification", b =>
                {
                    b.HasOne("BESL.Entities.Player", "Player")
                        .WithMany("Notifications")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("BESL.Entities.PlayerMatch", b =>
                {
                    b.HasOne("BESL.Entities.Match", "Match")
                        .WithMany("ParticipatedPlayers")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BESL.Entities.Player", "Player")
                        .WithMany("PlayerMatches")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BESL.Entities.PlayerTeam", b =>
                {
                    b.HasOne("BESL.Entities.Player", "Player")
                        .WithMany("PlayerTeams")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BESL.Entities.Team", "Team")
                        .WithMany("PlayerTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BESL.Entities.Team", b =>
                {
                    b.HasOne("BESL.Entities.TeamTableResult", "CurrentActiveTeamTableResult")
                        .WithMany()
                        .HasForeignKey("CurrentActiveTeamTableResultId");

                    b.HasOne("BESL.Entities.Player", "Owner")
                        .WithMany("OwnedTeams")
                        .HasForeignKey("OwnerId");

                    b.HasOne("BESL.Entities.TournamentFormat", "TournamentFormat")
                        .WithMany("Teams")
                        .HasForeignKey("TournamentFormatId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BESL.Entities.TeamTableResult", b =>
                {
                    b.HasOne("BESL.Entities.Team", "Team")
                        .WithMany("PreviousTeamTableResults")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BESL.Entities.TournamentTable", "TournamentTable")
                        .WithMany("TeamTableResults")
                        .HasForeignKey("TournamentTableId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BESL.Entities.Tournament", b =>
                {
                    b.HasOne("BESL.Entities.TournamentFormat", "Format")
                        .WithMany("Tournaments")
                        .HasForeignKey("FormatId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BESL.Entities.TournamentFormat", b =>
                {
                    b.HasOne("BESL.Entities.Game", "Game")
                        .WithMany("TournamentFormats")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("BESL.Entities.TournamentTable", b =>
                {
                    b.HasOne("BESL.Entities.Tournament", "Tournament")
                        .WithMany("Tables")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("BESL.Entities.PlayerRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BESL.Entities.Player")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BESL.Entities.Player")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("BESL.Entities.PlayerRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BESL.Entities.Player")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BESL.Entities.Player")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
