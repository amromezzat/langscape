using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FlashCardsUpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCardWord_FlashCardSets_FlashCardSetId",
                table: "FlashCardWord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlashCardWord",
                table: "FlashCardWord");

            migrationBuilder.RenameTable(
                name: "FlashCardWord",
                newName: "FlashCardWords");

            migrationBuilder.RenameColumn(
                name: "FlashCardSetId",
                table: "FlashCardWords",
                newName: "SetId");

            migrationBuilder.RenameIndex(
                name: "IX_FlashCardWord_FlashCardSetId",
                table: "FlashCardWords",
                newName: "IX_FlashCardWords_SetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlashCardWords",
                table: "FlashCardWords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCardWords_FlashCardSets_SetId",
                table: "FlashCardWords",
                column: "SetId",
                principalTable: "FlashCardSets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCardWords_FlashCardSets_SetId",
                table: "FlashCardWords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlashCardWords",
                table: "FlashCardWords");

            migrationBuilder.RenameTable(
                name: "FlashCardWords",
                newName: "FlashCardWord");

            migrationBuilder.RenameColumn(
                name: "SetId",
                table: "FlashCardWord",
                newName: "FlashCardSetId");

            migrationBuilder.RenameIndex(
                name: "IX_FlashCardWords_SetId",
                table: "FlashCardWord",
                newName: "IX_FlashCardWord_FlashCardSetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlashCardWord",
                table: "FlashCardWord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCardWord_FlashCardSets_FlashCardSetId",
                table: "FlashCardWord",
                column: "FlashCardSetId",
                principalTable: "FlashCardSets",
                principalColumn: "Id");
        }
    }
}
