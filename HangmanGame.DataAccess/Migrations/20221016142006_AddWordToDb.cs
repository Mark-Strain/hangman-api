using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HangmanGame.DataAccess.Migrations
{
    public partial class AddWordToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1, "fade" },
                    { 2, "decorative" },
                    { 3, "credibility" },
                    { 4, "killer" },
                    { 5, "foreigner" },
                    { 6, "notice" },
                    { 7, "hiccup" },
                    { 8, "eternal" },
                    { 9, "age" },
                    { 10, "key" },
                    { 11, "surface" },
                    { 12, "activate" },
                    { 13, "discount" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
