using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentroEducativoAPISQL.Migrations
{
    /// <inheritdoc />
    public partial class MyfirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    id_curso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_curso = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    aula = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion_curso = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.id_curso);
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
                    fechaNacimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contraseña = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
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
                name: "Clases",
                columns: table => new
                {
                    id_clase = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hora_inicio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    hora_fin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    materia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    id_profesor = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clases", x => x.id_clase);
                    table.ForeignKey(
                        name: "FK_Clases_Usuarios_id_profesor",
                        column: x => x.id_profesor,
                        principalTable: "Usuarios",
                        principalColumn: "dni");
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
                    id_usuario = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Noticias", x => x.id_noticia);
                    table.ForeignKey(
                        name: "FK_Noticias_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "dni");
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    id_pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nro_factura = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    monto = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    fecha_pago = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tipo_pago = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    fecha_vencimiento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    concepto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    id_usuario = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.id_pago);
                    table.ForeignKey(
                        name: "FK_Pagos_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "dni",
                        onDelete: ReferentialAction.Cascade);
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
                    id_usuario = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesInscripcion", x => x.id_solicitud);
                    table.ForeignKey(
                        name: "FK_SolicitudesInscripcion_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "dni",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosCursos",
                columns: table => new
                {
                    Dni = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    IdCurso = table.Column<int>(type: "int", nullable: false),
                    id_usuario_curso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosCursos", x => new { x.Dni, x.IdCurso });
                    table.ForeignKey(
                        name: "FK_UsuariosCursos_Curso_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Curso",
                        principalColumn: "id_curso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosCursos_Usuarios_Dni",
                        column: x => x.Dni,
                        principalTable: "Usuarios",
                        principalColumn: "dni",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CursoClases",
                columns: table => new
                {
                    IdCurso = table.Column<int>(type: "int", nullable: false),
                    IdClase = table.Column<int>(type: "int", nullable: false),
                    id_curso_clase = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoClases", x => new { x.IdCurso, x.IdClase });
                    table.ForeignKey(
                        name: "FK_CursoClases_Clases_IdClase",
                        column: x => x.IdClase,
                        principalTable: "Clases",
                        principalColumn: "id_clase",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoClases_Curso_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Curso",
                        principalColumn: "id_curso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    id_nota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_docente = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    id_alumno = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    id_clase = table.Column<int>(type: "int", nullable: true),
                    fecha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.id_nota);
                    table.ForeignKey(
                        name: "FK_Notas_Clases_id_clase",
                        column: x => x.id_clase,
                        principalTable: "Clases",
                        principalColumn: "id_clase",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notas_Usuarios_id_alumno",
                        column: x => x.id_alumno,
                        principalTable: "Usuarios",
                        principalColumn: "dni",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notas_Usuarios_id_docente",
                        column: x => x.id_docente,
                        principalTable: "Usuarios",
                        principalColumn: "dni",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosClases",
                columns: table => new
                {
                    Dni = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    IdClase = table.Column<int>(type: "int", nullable: false),
                    id_usuario_clase = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosClases", x => new { x.Dni, x.IdClase });
                    table.ForeignKey(
                        name: "FK_UsuariosClases_Clases_IdClase",
                        column: x => x.IdClase,
                        principalTable: "Clases",
                        principalColumn: "id_clase",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosClases_Usuarios_Dni",
                        column: x => x.Dni,
                        principalTable: "Usuarios",
                        principalColumn: "dni",
                        onDelete: ReferentialAction.Cascade);
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
                    id_usuario = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    id_noticia = table.Column<int>(type: "int", nullable: true)
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clases_id_profesor",
                table: "Clases",
                column: "id_profesor");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_id_noticia",
                table: "Comentarios",
                column: "id_noticia");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_id_usuario",
                table: "Comentarios",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_CursoClases_IdClase",
                table: "CursoClases",
                column: "IdClase");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_id_alumno",
                table: "Notas",
                column: "id_alumno");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_id_clase",
                table: "Notas",
                column: "id_clase");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_id_docente",
                table: "Notas",
                column: "id_docente");

            migrationBuilder.CreateIndex(
                name: "IX_Noticias_id_usuario",
                table: "Noticias",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_id_usuario",
                table: "Pagos",
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

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosClases_IdClase",
                table: "UsuariosClases",
                column: "IdClase");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosCursos_IdCurso",
                table: "UsuariosCursos",
                column: "IdCurso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "CursoClases");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "SolicitudesInscripcion");

            migrationBuilder.DropTable(
                name: "UsuariosClases");

            migrationBuilder.DropTable(
                name: "UsuariosCursos");

            migrationBuilder.DropTable(
                name: "Noticias");

            migrationBuilder.DropTable(
                name: "Clases");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
