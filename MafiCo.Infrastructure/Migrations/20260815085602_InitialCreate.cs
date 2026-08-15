using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MafiCo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mafico");

            migrationBuilder.CreateTable(
                name: "Games",
                schema: "mafico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LlmData",
                schema: "mafico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModelName = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    ApiKey = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LlmData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                schema: "mafico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    VictoriesCount = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    DefeatsCount = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bots",
                schema: "mafico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProfileId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LlmId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bots_LlmData_LlmId",
                        column: x => x.LlmId,
                        principalSchema: "mafico",
                        principalTable: "LlmData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Bots_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalSchema: "mafico",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bots_LlmId",
                schema: "mafico",
                table: "Bots",
                column: "LlmId");

            migrationBuilder.CreateIndex(
                name: "IX_Bots_ProfileId",
                schema: "mafico",
                table: "Bots",
                column: "ProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bots",
                schema: "mafico");

            migrationBuilder.DropTable(
                name: "Games",
                schema: "mafico");

            migrationBuilder.DropTable(
                name: "LlmData",
                schema: "mafico");

            migrationBuilder.DropTable(
                name: "Profiles",
                schema: "mafico");
        }
    }
}
