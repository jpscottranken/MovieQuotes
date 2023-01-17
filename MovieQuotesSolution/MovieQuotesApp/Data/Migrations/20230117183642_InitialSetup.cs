using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieQuotesApp.Data.Migrations
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieQuotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteMovie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuoteText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuoteAuthor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieQuotes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieQuotes");
        }
    }
}
