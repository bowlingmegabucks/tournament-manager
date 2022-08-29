using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NortheastMegabuck.Database.Migrations;

public partial class StartingAndNumberOfLanes : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<short>(
            name: "NumberOfLanes",
            table: "Squads",
            type: "smallint",
            nullable: false,
            defaultValue: (short)0);

        migrationBuilder.AddColumn<short>(
            name: "StartingLane",
            table: "Squads",
            type: "smallint",
            nullable: false,
            defaultValue: (short)0);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "NumberOfLanes",
            table: "Squads");

        migrationBuilder.DropColumn(
            name: "StartingLane",
            table: "Squads");
    }
}
