using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentroEducativoAPISQL.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Roles",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo_rol = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id_rol);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    dni = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    nombreCompleto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fechaNacimientoSolicitante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contraseña = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    id_rol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.dni);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_id_rol",
                        column: x => x.id_rol,
                        principalTable: "Roles",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    id_alumno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    id_nivelEducativo = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Noticias",
                columns: table => new
                {
                    id_noticia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    parrafos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imagenes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    redactor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fecha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_usuario = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Noticias", x => x.id_noticia);
                    table.ForeignKey(
                        name: "FK_Noticias_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "dni",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesInscripcion",
                columns: table => new
                {
                    id_solicitud = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreCompletoSolicitante = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    correoSolicitante = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    nivelEducativoSolicitante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fechaNacimientoSolicitante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_usuario = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesInscripcion", x => x.id_solicitud);
                    table.ForeignKey(
                        name: "FK_SolicitudesInscripcion_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "dni",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    id_comentario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaHoraComentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_usuario = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    id_noticia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.id_comentario);
                    table.ForeignKey(
                        name: "FK_Comentarios_Noticias_id_noticia",
                        column: x => x.id_noticia,
                        principalTable: "Noticias",
                        principalColumn: "id_noticia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "dni",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_id_nivelEducativo",
                table: "Alumnos",
                column: "id_nivelEducativo");

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_id_usuario",
                table: "Alumnos",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_id_noticia",
                table: "Comentarios",
                column: "id_noticia");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_id_usuario",
                table: "Comentarios",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Noticias_id_usuario",
                table: "Noticias",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesInscripcion_id_usuario",
                table: "SolicitudesInscripcion",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_correo",
                table: "Usuarios",
                column: "correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_dni",
                table: "Usuarios",
                column: "dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_id_rol",
                table: "Usuarios",
                column: "id_rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "SolicitudesInscripcion");

            migrationBuilder.DropTable(
                name: "NivelesEducativos");

            migrationBuilder.DropTable(
                name: "Noticias");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
