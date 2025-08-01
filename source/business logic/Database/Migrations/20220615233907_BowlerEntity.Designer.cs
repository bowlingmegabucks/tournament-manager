﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using BowlingMegabucks.TournamentManager.Database;

#nullable disable
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BowlingMegabucks.TournamentManager.Database.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220615233907_BowlerEntity")]
    partial class BowlerEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BowlingMegabucks.TournamentManager.Database.Entities.Bowler", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CityAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MiddleInitial")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("char(1)")
                        .IsFixedLength();

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("char(10)")
                        .IsFixedLength();

                    b.Property<string>("StateAddress")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("char(2)")
                        .IsFixedLength();

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Suffix")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("USBCId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("char(9)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("Bowlers");
                });

            modelBuilder.Entity("BowlingMegabucks.TournamentManager.Database.Entities.Division", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<int?>("HandicapBase")
                        .HasColumnType("int");

                    b.Property<decimal?>("HandicapPercentage")
                        .HasPrecision(3, 2)
                        .HasColumnType("decimal(3,2)");

                    b.Property<short?>("MaximumAge")
                        .HasColumnType("smallint");

                    b.Property<int?>("MaximumAverage")
                        .HasColumnType("int");

                    b.Property<int?>("MaximumHandicapPerGame")
                        .HasColumnType("int");

                    b.Property<short?>("MinimumAge")
                        .HasColumnType("smallint");

                    b.Property<int?>("MinimumAverage")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<short>("Number")
                        .HasColumnType("smallint");

                    b.Property<Guid>("TournamentId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("BowlingMegabucks.TournamentManager.Database.Entities.Squad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal?>("CashRatio")
                        .HasPrecision(3, 1)
                        .HasColumnType("decimal(3,1)");

                    b.Property<bool>("Complete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<short>("MaxPerPair")
                        .HasColumnType("smallint");

                    b.Property<int>("SquadType")
                        .HasColumnType("int");

                    b.Property<Guid>("TournamentId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Squads", (string)null);

                    b.HasDiscriminator<int>("SquadType");
                });

            modelBuilder.Entity("BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision", b =>
                {
                    b.Property<Guid>("SweeperId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DivisionId")
                        .HasColumnType("char(36)");

                    b.Property<int?>("BonusPinsPerGame")
                        .HasColumnType("int");

                    b.HasKey("SweeperId", "DivisionId");

                    b.HasIndex("DivisionId");

                    b.ToTable("SweeperDivision");
                });

            modelBuilder.Entity("BowlingMegabucks.TournamentManager.Database.Entities.Tournament", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("BowlingCenter")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("CashRatio")
                        .HasPrecision(3, 1)
                        .HasColumnType("decimal(3,1)");

                    b.Property<bool>("Completed")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("EntryFee")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<decimal>("FinalsRatio")
                        .HasPrecision(3, 1)
                        .HasColumnType("decimal(3,1)");

                    b.Property<short>("Games")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad", b =>
                {
                    b.HasBaseType("BowlingMegabucks.TournamentManager.Database.Entities.Squad");

                    b.Property<decimal>("EntryFee")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<short>("Games")
                        .HasColumnType("smallint");

                    b.HasIndex("TournamentId");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad", b =>
                {
                    b.HasBaseType("BowlingMegabucks.TournamentManager.Database.Entities.Squad");

                    b.Property<decimal?>("FinalsRatio")
                        .HasPrecision(3, 1)
                        .HasColumnType("decimal(3,1)");

                    b.HasIndex("TournamentId");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("BowlingMegabucks.TournamentManager.Database.Entities.Division", b =>
                {
                    b.HasOne("BowlingMegabucks.TournamentManager.Database.Entities.Tournament", "Tournament")
                        .WithMany("Divisions")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision", b =>
                {
                    b.HasOne("BowlingMegabucks.TournamentManager.Database.Entities.Division", "Division")
                        .WithMany("Sweepers")
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad", "Sweeper")
                        .WithMany("Divisions")
                        .HasForeignKey("SweeperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Division");

                    b.Navigation("Sweeper");
                });

            modelBuilder.Entity("BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad", b =>
                {
                    b.HasOne("BowlingMegabucks.TournamentManager.Database.Entities.Tournament", "Tournament")
                        .WithMany("Sweepers")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad", b =>
                {
                    b.HasOne("BowlingMegabucks.TournamentManager.Database.Entities.Tournament", "Tournament")
                        .WithMany("Squads")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("BowlingMegabucks.TournamentManager.Database.Entities.Division", b =>
                {
                    b.Navigation("Sweepers");
                });

            modelBuilder.Entity("BowlingMegabucks.TournamentManager.Database.Entities.Tournament", b =>
                {
                    b.Navigation("Divisions");

                    b.Navigation("Squads");

                    b.Navigation("Sweepers");
                });

            modelBuilder.Entity("BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad", b =>
                {
                    b.Navigation("Divisions");
                });
#pragma warning restore 612, 618
        }
    }
}
