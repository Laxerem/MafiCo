using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MafiCo.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class make_bot_llm_optional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bots_LlmData_LlmId",
                schema: "mafico",
                table: "Bots");

            migrationBuilder.AlterColumn<Guid>(
                name: "LlmId",
                schema: "mafico",
                table: "Bots",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_LlmData_LlmId",
                schema: "mafico",
                table: "Bots",
                column: "LlmId",
                principalSchema: "mafico",
                principalTable: "LlmData",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bots_LlmData_LlmId",
                schema: "mafico",
                table: "Bots");

            migrationBuilder.AlterColumn<Guid>(
                name: "LlmId",
                schema: "mafico",
                table: "Bots",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_LlmData_LlmId",
                schema: "mafico",
                table: "Bots",
                column: "LlmId",
                principalSchema: "mafico",
                principalTable: "LlmData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
