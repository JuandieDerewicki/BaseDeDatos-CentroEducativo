using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentroEducativoAPISQL.Migrations
{
    /// <inheritdoc />
    public partial class EliminacionTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "NivelesEducativos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NivelesEducativos",
                columns: table => new
                {
                    id_nivelEducativo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo_nivel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NivelesEducativos", x => x.id_nivelEducativo);
                });

            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    id_alumno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_nivelEducativo = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.id_alumno);
                    table.ForeignKey(
                        name: "FK_Alumnos_NivelesEducativos_id_nivelEducativo",
                        column: x => x.id_nivelEducativo,
                        principalTable: "NivelesEducativos",
                        principalColumn: "id_nivelEducativo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alumnos_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "dni",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_id_nivelEducativo",
                table: "Alumnos",
                column: "id_nivelEducativo");

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_id_usuario",
                table: "Alumnos",
                column: "id_usuario");
        }
    }
}
