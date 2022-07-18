using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewEnglandClassic.Database.Migrations;

public partial class AddRegistration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Registration",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                BowlerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                DivisionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Average = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Registration", x => x.Id);
                table.UniqueConstraint("AK_Registration_BowlerId_DivisionId", x => new { x.BowlerId, x.DivisionId });
                table.ForeignKey(
                    name: "FK_Registration_Bowlers_BowlerId",
                    column: x => x.BowlerId,
                    principalTable: "Bowlers",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Registration_Divisions_DivisionId",
                    column: x => x.DivisionId,
                    principalTable: "Divisions",
                    principalColumn: "Id");
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "SquadRegistration",
            columns: table => new
            {
                RegistrationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                SquadId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SquadRegistration", x => new { x.RegistrationId, x.SquadId });
                table.ForeignKey(
                    name: "FK_SquadRegistration_Registration_RegistrationId",
                    column: x => x.RegistrationId,
                    principalTable: "Registration",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_SquadRegistration_Squads_SquadId",
                    column: x => x.SquadId,
                    principalTable: "Squads",
                    principalColumn: "Id");
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_Registration_DivisionId",
            table: "Registration",
            column: "DivisionId");

        migrationBuilder.CreateIndex(
            name: "IX_SquadRegistration_SquadId",
            table: "SquadRegistration",
            column: "SquadId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "SquadRegistration");

        migrationBuilder.DropTable(
            name: "Registration");
    }
}
