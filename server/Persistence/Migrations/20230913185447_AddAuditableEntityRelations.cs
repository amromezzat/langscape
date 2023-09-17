using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditableEntityRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCardSets_AspNetUsers_AppUserId",
                table: "FlashCardSets");

            migrationBuilder.DropIndex(
                name: "IX_FlashCardSets_AppUserId",
                table: "FlashCardSets");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "FlashCardSets");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "FlashCardSets",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "FlashCardSets",
                newName: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FlashCardSets_CreatedById",
                table: "FlashCardSets",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FlashCardSets_ModifiedById",
                table: "FlashCardSets",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCardSets_AspNetUsers_CreatedById",
                table: "FlashCardSets",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCardSets_AspNetUsers_ModifiedById",
                table: "FlashCardSets",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCardSets_AspNetUsers_CreatedById",
                table: "FlashCardSets");

            migrationBuilder.DropForeignKey(
                name: "FK_FlashCardSets_AspNetUsers_ModifiedById",
                table: "FlashCardSets");

            migrationBuilder.DropIndex(
                name: "IX_FlashCardSets_CreatedById",
                table: "FlashCardSets");

            migrationBuilder.DropIndex(
                name: "IX_FlashCardSets_ModifiedById",
                table: "FlashCardSets");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "FlashCardSets",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "FlashCardSets",
                newName: "CreatedBy");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "FlashCardSets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlashCardSets_AppUserId",
                table: "FlashCardSets",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCardSets_AspNetUsers_AppUserId",
                table: "FlashCardSets",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
