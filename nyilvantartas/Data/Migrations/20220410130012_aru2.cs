using Microsoft.EntityFrameworkCore.Migrations;

namespace nyilvantartas.Data.Migrations
{
    public partial class aru2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aruk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Megnevezes = table.Column<string>(type: "nvarchar(60)", nullable: true),
                    Gyarto = table.Column<string>(type: "nvarchar(60)", nullable: true),
                    Tipus = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    BeszerzesiAr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aruk", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aruk");
        }
    }
}
