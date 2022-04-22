using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewEnglandClassic.Database.Migrations;

public partial class TournamentEntity : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder) 
        => migrationBuilder.CreateTable(
            name: "Tournaments",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                End = table.Column<DateTime>(type: "datetime2", nullable: false),
                EntryFee = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                Games = table.Column<short>(type: "smallint", nullable: false),
                FinalsRatio = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: false),
                CashRatio = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: false),
                BowlingCenter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Completed = table.Column<bool>(type: "bit", nullable: false)
            },
            
            constraints: 
                table => table.PrimaryKey("PK_Tournaments", x => x.Id));

    protected override void Down(MigrationBuilder migrationBuilder) 
        => migrationBuilder.DropTable(
            name: "Tournaments");
}
