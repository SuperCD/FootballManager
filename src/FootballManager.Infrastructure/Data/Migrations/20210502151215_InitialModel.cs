using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballManager.Infrastructure.Data.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "Formation_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "player_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Team_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FoundedIn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Formation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FormationType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ParentTeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Formation_Teams_ParentTeamId",
                        column: x => x.ParentTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreferredFoot = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CurrentTeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Teams_CurrentTeamId",
                        column: x => x.CurrentTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivePlayerStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivePlayerStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivePlayerStatus_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormationPostition",
                columns: table => new
                {
                    PositionNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: true),
                    FormationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormationPostition", x => x.PositionNo);
                    table.ForeignKey(
                        name: "FK_FormationPostition_Formation_FormationId",
                        column: x => x.FormationId,
                        principalTable: "Formation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormationPostition_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivePlayerStatus_PlayerId",
                table: "ActivePlayerStatus",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Formation_ParentTeamId",
                table: "Formation",
                column: "ParentTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_FormationPostition_FormationId",
                table: "FormationPostition",
                column: "FormationId");

            migrationBuilder.CreateIndex(
                name: "IX_FormationPostition_PlayerId",
                table: "FormationPostition",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CurrentTeamId",
                table: "Players",
                column: "CurrentTeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivePlayerStatus");

            migrationBuilder.DropTable(
                name: "FormationPostition");

            migrationBuilder.DropTable(
                name: "Formation");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropSequence(
                name: "Formation_hilo");

            migrationBuilder.DropSequence(
                name: "player_hilo");

            migrationBuilder.DropSequence(
                name: "Team_hilo");
        }
    }
}
