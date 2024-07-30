using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FlashCardsUpdate2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FlashCardSets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCardWords_FlashCardSets_FlashCardSetId",
                table: "FlashCardWords",
                column: "FlashCardSetId",
                principalTable: "FlashCardSets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCardWords_FlashCardSets_FlashCardSetId",
                table: "FlashCardWords");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "FlashCardSets");

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
                principalColumn: "Id");
        }
    }
}
