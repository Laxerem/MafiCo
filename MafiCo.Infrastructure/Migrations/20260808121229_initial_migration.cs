using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MafiCo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mafico");

            migrationBuilder.CreateTable(
                name: "LlmBots",
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
                    table.PrimaryKey("PK_LlmBots", x => x.Id);
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LlmBots",
                schema: "mafico");

            migrationBuilder.DropTable(
                name: "Profiles",
                schema: "mafico");
        }
    }
}
