﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CentroEducativoAPISQL.Migrations
{
    [DbContext(typeof(MiDbContext))]
    [Migration("20230910155047_SegundaMigracion")]
    partial class SegundaMigracion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CentroEducativoAPISQL.Modelos.Alumno", b =>
                {
                    b.Property<int>("id_alumno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_alumno"));

                    b.Property<int>("id_nivelEducativo")
                        .HasColumnType("int");

                    b.Property<string>("id_usuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("id_alumno");

                    b.HasIndex("id_nivelEducativo");

                    b.HasIndex("id_usuario");

                    b.ToTable("Alumnos");
                });

            modelBuilder.Entity("CentroEducativoAPISQL.Modelos.Comentarios", b =>
                {
                    b.Property<int>("id_comentario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_comentario"));

                    b.Property<string>("contenido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fechaHoraComentario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("id_noticia")
                        .HasColumnType("int");

                    b.Property<string>("id_usuario")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id_comentario");

                    b.HasIndex("id_noticia");

                    b.HasIndex("id_usuario");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("CentroEducativoAPISQL.Modelos.NivelEducativo", b =>
                {
                    b.Property<int>("id_nivelEducativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_nivelEducativo"));

                    b.Property<string>("tipo_nivel")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id_nivelEducativo");

                    b.ToTable("NivelesEducativos");
                });

            modelBuilder.Entity("CentroEducativoAPISQL.Modelos.Noticias", b =>
                {
                    b.Property<int>("id_noticia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_noticia"));

                    b.Property<string>("fecha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("id_usuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("imagenes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("parrafos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("redactor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("id_noticia");

                    b.HasIndex("id_usuario");

                    b.ToTable("Noticias");
                });

            modelBuilder.Entity("CentroEducativoAPISQL.Modelos.Roles", b =>
                {
                    b.Property<int>("id_rol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_rol"));

                    b.Property<string>("tipo_rol")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("id_rol");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CentroEducativoAPISQL.Modelos.SolicitudInscripcion", b =>
                {
                    b.Property<int>("id_solicitud")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_solicitud"));

                    b.Property<string>("correoSolicitante")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("fechaNacimientoSolicitante")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("id_usuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("nivelEducativoSolicitante")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombreCompletoSolicitante")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("id_solicitud");

                    b.HasIndex("id_usuario");

                    b.ToTable("SolicitudesInscripcion");
                });

            modelBuilder.Entity("CentroEducativoAPISQL.Modelos.Usuarios", b =>
                {
                    b.Property<string>("dni")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("contraseña")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("correo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("fechaNacimientoSolicitante")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("id_rol")
                        .HasColumnType("int");

                    b.Property<string>("nombreCompleto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("telefono")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("dni");

                    b.HasIndex("correo")
                        .IsUnique();

                    b.HasIndex("dni")
                        .IsUnique();

                    b.HasIndex("id_rol");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("CentroEducativoAPISQL.Modelos.Alumno", b =>
                {
                    b.HasOne("CentroEducativoAPISQL.Modelos.NivelEducativo", "NivelEducativo")
                        .WithMany("Alumnos")
                        .HasForeignKey("id_nivelEducativo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CentroEducativoAPISQL.Modelos.Usuarios", "Usuario")
                        .WithMany()
                        .HasForeignKey("id_usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NivelEducativo");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("CentroEducativoAPISQL.Modelos.Comentarios", b =>
                {
                    b.HasOne("CentroEducativoAPISQL.Modelos.Noticias", "Noticia")
                        .WithMany("Comentarios")
                        .HasForeignKey("id_noticia")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CentroEducativoAPISQL.Modelos.Usuarios", "Usuario")
                        .WithMany("Comentarios")
                        .HasForeignKey("id_usuario")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Noticia");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("CentroEducativoAPISQL.Modelos.Noticias", b =>
                {
                    b.HasOne("CentroEducativoAPISQL.Modelos.Usuarios", "Usuario")
                        .WithMany("Noticias")
                        .HasForeignKey("id_usuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("CentroEducativoAPISQL.Modelos.SolicitudInscripcion", b =>
                {
                    b.HasOne("CentroEducativoAPISQL.Modelos.Usuarios", "Usuario")
                        .WithMany("SolicitudesInscripcion")
                        .HasForeignKey("id_usuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("CentroEducativoAPISQL.Modelos.Usuarios", b =>
                {
                    b.HasOne("CentroEducativoAPISQL.Modelos.Roles", "RolesUsuarios")
                        .WithMany("Usuarios")
                        .HasForeignKey("id_rol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RolesUsuarios");
                });

            modelBuilder.Entity("CentroEducativoAPISQL.Modelos.NivelEducativo", b =>
                {
                    b.Navigation("Alumnos");
                });

            modelBuilder.Entity("CentroEducativoAPISQL.Modelos.Noticias", b =>
                {
                    b.Navigation("Comentarios");
                });

            modelBuilder.Entity("CentroEducativoAPISQL.Modelos.Roles", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("CentroEducativoAPISQL.Modelos.Usuarios", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("Noticias");

                    b.Navigation("SolicitudesInscripcion");
                });
#pragma warning restore 612, 618
        }
    }
}
