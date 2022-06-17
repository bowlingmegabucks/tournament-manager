using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewEnglandClassic.Database.Migrations;

public partial class BowlerEntity : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Bowlers",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                FirstName = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                MiddleInitial = table.Column<string>(type: "char(1)", fixedLength: true, maxLength: 1, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                LastName = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Suffix = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                StreetAddress = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                CityAddress = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                StateAddress = table.Column<string>(type: "char(2)", fixedLength: true, maxLength: 2, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                ZipCode = table.Column<string>(type: "char(9)", fixedLength: true, maxLength: 9, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                EmailAddress = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                PhoneNumber = table.Column<string>(type: "char(10)", fixedLength: true, maxLength: 10, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                USBCId = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                Gender = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_Bowlers", x => x.Id))
            .Annotation("MySql:CharSet", "utf8mb4");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Bowlers");
    }
}
