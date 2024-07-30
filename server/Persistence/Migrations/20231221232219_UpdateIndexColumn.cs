using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIndexColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE FlashCardWords
                SET Position = (
                    SELECT RowNum
                    FROM (
                        SELECT 
                            FlashCardWords.id,
                            ROW_NUMBER() OVER (
                                PARTITION BY FlashCardSets.Id
                                ORDER BY FlashCardSets.Id, FlashCardWords.Id 
                            ) AS RowNum
                        FROM FlashCardWords 
                        INNER JOIN FlashCardSets ON FlashCardWords.SetId = FlashCardSets.Id
                    ) AS nf
                    WHERE nf.id = FlashCardWords.id
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
