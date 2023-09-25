using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentroEducativoAPISQL.Migrations
{
    /// <inheritdoc />
    public partial class TerceraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Noticias_id_noticia",
                table: "Comentarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Noticias_id_noticia",
                table: "Comentarios",
                column: "id_noticia",
                principalTable: "Noticias",
                principalColumn: "id_noticia",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Noticias_id_noticia",
                table: "Comentarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Noticias_id_noticia",
                table: "Comentarios",
                column: "id_noticia",
                principalTable: "Noticias",
                principalColumn: "id_noticia",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
