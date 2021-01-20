using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Autuas.Migrations
{
    public partial class DailyCheckIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyCheckIns",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    PhysicalState = table.Column<int>(nullable: true),
                    MentalState = table.Column<int>(nullable: true),
                    PositiveFeelings = table.Column<int>(nullable: true),
                    NegativeFeelings = table.Column<int>(nullable: true),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyCheckIns", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DailyCheckIns_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyCheckIns_UserID",
                table: "DailyCheckIns",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyCheckIns");
        }
    }
}
