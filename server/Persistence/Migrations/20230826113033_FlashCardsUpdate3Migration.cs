using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FlashCardsUpdate3Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCardWords_FlashCardSets_FlashCardSetId",
                table: "FlashCardWords");

            migrationBuilder.RenameColumn(
                name: "FlashCardSetId",
                table: "FlashCardWords",
                newName: "SetId");

            migrationBuilder.RenameIndex(
                name: "IX_FlashCardWords_FlashCardSetId",
                table: "FlashCardWords",
                newName: "IX_FlashCardWords_SetId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCardWords_FlashCardSets_SetId",
                table: "FlashCardWords",
                column: "SetId",
                principalTable: "FlashCardSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCardWords_FlashCardSets_SetId",
                table: "FlashCardWords");

            migrationBuilder.RenameColumn(
                name: "SetId",
                table: "FlashCardWords",
                newName: "FlashCardSetId");

            migrationBuilder.RenameIndex(
                name: "IX_FlashCardWords_SetId",
                table: "FlashCardWords",
                newName: "IX_FlashCardWords_FlashCardSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCardWords_FlashCardSets_FlashCardSetId",
                table: "FlashCardWords",
                column: "FlashCardSetId",
                principalTable: "FlashCardSets",
                principalColumn: "Id");
        }
    }
}
