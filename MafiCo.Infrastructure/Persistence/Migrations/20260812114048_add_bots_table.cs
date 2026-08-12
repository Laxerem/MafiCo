using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MafiCo.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_bots_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LlmBots",
                schema: "mafico");

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
                name: "Bots",
                schema: "mafico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProfileId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LlmId = table.Column<Guid>(type: "TEXT", nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
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
                name: "LlmData",
                schema: "mafico");

            migrationBuilder.CreateTable(
                name: "LlmBots",
                schema: "mafico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ApiKey = table.Column<string>(type: "TEXT", nullable: false),
                    ModelName = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LlmBots", x => x.Id);
                });
        }
    }
}
