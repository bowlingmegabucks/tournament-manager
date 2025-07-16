using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable

namespace NortheastMegabuck.Database.Migrations;

public partial class AddRegistration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Registrations",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                BowlerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                DivisionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Average = table.Column<int>(type: "int", nullable: true),
                SuperSweeper = table.Column<bool>(type: "tinyint(1)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Registrations", x => x.Id);
                table.ForeignKey(
                    name: "FK_Registrations_Bowlers_BowlerId",
                    column: x => x.BowlerId,
                    principalTable: "Bowlers",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Registrations_Divisions_DivisionId",
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
                    name: "FK_SquadRegistration_Registrations_RegistrationId",
                    column: x => x.RegistrationId,
                    principalTable: "Registrations",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_SquadRegistration_Squads_SquadId",
                    column: x => x.SquadId,
                    principalTable: "Squads",
                    principalColumn: "Id");
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_Registrations_DivisionId",
            table: "Registrations",
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
            name: "Registrations");
    }
}
