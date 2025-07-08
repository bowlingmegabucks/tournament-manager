using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable

namespace NortheastMegabuck.Database.Migrations;

/// <inheritdoc />
public partial class BowlerSSN : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "SocialSecurityNumber",
            table: "Bowlers",
            type: "longtext",
            nullable: false)
            .Annotation("MySql:CharSet", "utf8mb4");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "SocialSecurityNumber",
            table: "Bowlers");
    }
}
