using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyBookWeb.Migrations
{
    public partial class AddCategoryToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Crea una tabella
            migrationBuilder.CreateTable(
                //di nome Categories
                name: "Categories",
                columns: table => new
                {
                    //con quattro colonne
                    //la prima è una Id int not null identity (che parte da 1 e fa autoincrement di 1)
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    //la seconda "name" è nullable false (required). attenzione required serve per i campi che non vogliamo 
                    //siano nullable.
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    //nonostante DisplayOrder non sia required è nullable false in quanto non è una stringa ma un int
                    //se volessimo nullable true basterebbe cambiare nella definizione del modello int con int? prima di lanciare add-migration
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    //definisce una primary key su id
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });
            //una volta verificato che tutto è quello che ci aspettiamo, da Package Manager Console lanciamo il comando upgrade-database
            //la migration creerà una query sql che verrà lanciata direttamente sul server sql.
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
