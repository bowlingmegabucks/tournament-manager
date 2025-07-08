using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable

namespace NortheastMegabuck.Database.Migrations;

public partial class SquadLaneAssignment : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "LaneAssignment",
            table: "SquadRegistration",
            type: "varchar(3)",
            maxLength: 3,
            nullable: false,
            defaultValue: "")
            .Annotation("MySql:CharSet", "utf8mb4");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "LaneAssignment",
            table: "SquadRegistration");
    }
}
