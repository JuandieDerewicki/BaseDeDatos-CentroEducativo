IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE TABLE [Curso] (
        [id_curso] int NOT NULL IDENTITY,
        [nombre_curso] nvarchar(50) NOT NULL,
        [aula] nvarchar(50) NOT NULL,
        [descripcion_curso] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_Curso] PRIMARY KEY ([id_curso])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE TABLE [Roles] (
        [id_rol] int NOT NULL IDENTITY,
        [tipo_rol] nvarchar(40) NOT NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([id_rol])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE TABLE [Usuarios] (
        [dni] nvarchar(20) NOT NULL,
        [nombreCompleto] nvarchar(100) NOT NULL,
        [correo] nvarchar(100) NOT NULL,
        [fechaNacimiento] nvarchar(max) NOT NULL,
        [contraseña] nvarchar(64) NOT NULL,
        [telefono] nvarchar(15) NOT NULL,
        [id_rol] int NOT NULL,
        CONSTRAINT [PK_Usuarios] PRIMARY KEY ([dni]),
        CONSTRAINT [FK_Usuarios_Roles_id_rol] FOREIGN KEY ([id_rol]) REFERENCES [Roles] ([id_rol]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE TABLE [Clases] (
        [id_clase] int NOT NULL IDENTITY,
        [hora_inicio] nvarchar(50) NOT NULL,
        [hora_fin] nvarchar(50) NOT NULL,
        [materia] nvarchar(50) NOT NULL,
        [id_profesor] nvarchar(20) NULL,
        CONSTRAINT [PK_Clases] PRIMARY KEY ([id_clase]),
        CONSTRAINT [FK_Clases_Usuarios_id_profesor] FOREIGN KEY ([id_profesor]) REFERENCES [Usuarios] ([dni])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE TABLE [Noticias] (
        [id_noticia] int NOT NULL IDENTITY,
        [titulo] nvarchar(100) NOT NULL,
        [parrafos] nvarchar(max) NOT NULL,
        [imagenes] nvarchar(max) NOT NULL,
        [redactor] nvarchar(max) NOT NULL,
        [fecha] nvarchar(max) NOT NULL,
        [id_usuario] nvarchar(20) NULL,
        CONSTRAINT [PK_Noticias] PRIMARY KEY ([id_noticia]),
        CONSTRAINT [FK_Noticias_Usuarios_id_usuario] FOREIGN KEY ([id_usuario]) REFERENCES [Usuarios] ([dni])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE TABLE [Pagos] (
        [id_pago] int NOT NULL IDENTITY,
        [nro_factura] nvarchar(20) NOT NULL,
        [monto] nvarchar(20) NOT NULL,
        [fecha_pago] nvarchar(50) NOT NULL,
        [tipo_pago] nvarchar(50) NOT NULL,
        [fecha_vencimiento] nvarchar(50) NOT NULL,
        [concepto] nvarchar(100) NOT NULL,
        [id_usuario] nvarchar(20) NULL,
        CONSTRAINT [PK_Pagos] PRIMARY KEY ([id_pago]),
        CONSTRAINT [FK_Pagos_Usuarios_id_usuario] FOREIGN KEY ([id_usuario]) REFERENCES [Usuarios] ([dni]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE TABLE [SolicitudesInscripcion] (
        [id_solicitud] int NOT NULL IDENTITY,
        [nombreCompletoSolicitante] nvarchar(100) NOT NULL,
        [correoSolicitante] nvarchar(40) NOT NULL,
        [nivelEducativoSolicitante] nvarchar(max) NOT NULL,
        [fechaNacimientoSolicitante] nvarchar(max) NOT NULL,
        [id_usuario] nvarchar(20) NULL,
        CONSTRAINT [PK_SolicitudesInscripcion] PRIMARY KEY ([id_solicitud]),
        CONSTRAINT [FK_SolicitudesInscripcion_Usuarios_id_usuario] FOREIGN KEY ([id_usuario]) REFERENCES [Usuarios] ([dni]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE TABLE [UsuariosCursos] (
        [Dni] nvarchar(20) NOT NULL,
        [IdCurso] int NOT NULL,
        [id_usuario_curso] int NOT NULL,
        CONSTRAINT [PK_UsuariosCursos] PRIMARY KEY ([Dni], [IdCurso]),
        CONSTRAINT [FK_UsuariosCursos_Curso_IdCurso] FOREIGN KEY ([IdCurso]) REFERENCES [Curso] ([id_curso]) ON DELETE CASCADE,
        CONSTRAINT [FK_UsuariosCursos_Usuarios_Dni] FOREIGN KEY ([Dni]) REFERENCES [Usuarios] ([dni]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE TABLE [CursoClases] (
        [IdCurso] int NOT NULL,
        [IdClase] int NOT NULL,
        [id_curso_clase] int NOT NULL,
        CONSTRAINT [PK_CursoClases] PRIMARY KEY ([IdCurso], [IdClase]),
        CONSTRAINT [FK_CursoClases_Clases_IdClase] FOREIGN KEY ([IdClase]) REFERENCES [Clases] ([id_clase]) ON DELETE CASCADE,
        CONSTRAINT [FK_CursoClases_Curso_IdCurso] FOREIGN KEY ([IdCurso]) REFERENCES [Curso] ([id_curso]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE TABLE [Notas] (
        [id_nota] int NOT NULL IDENTITY,
        [nota] nvarchar(max) NOT NULL,
        [id_docente] nvarchar(20) NULL,
        [id_alumno] nvarchar(20) NULL,
        [id_clase] int NULL,
        [fecha] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Notas] PRIMARY KEY ([id_nota]),
        CONSTRAINT [FK_Notas_Clases_id_clase] FOREIGN KEY ([id_clase]) REFERENCES [Clases] ([id_clase]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Notas_Usuarios_id_alumno] FOREIGN KEY ([id_alumno]) REFERENCES [Usuarios] ([dni]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Notas_Usuarios_id_docente] FOREIGN KEY ([id_docente]) REFERENCES [Usuarios] ([dni]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE TABLE [UsuariosClases] (
        [Dni] nvarchar(20) NOT NULL,
        [IdClase] int NOT NULL,
        [id_usuario_clase] int NOT NULL,
        CONSTRAINT [PK_UsuariosClases] PRIMARY KEY ([Dni], [IdClase]),
        CONSTRAINT [FK_UsuariosClases_Clases_IdClase] FOREIGN KEY ([IdClase]) REFERENCES [Clases] ([id_clase]) ON DELETE CASCADE,
        CONSTRAINT [FK_UsuariosClases_Usuarios_Dni] FOREIGN KEY ([Dni]) REFERENCES [Usuarios] ([dni]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE TABLE [Comentarios] (
        [id_comentario] int NOT NULL IDENTITY,
        [contenido] nvarchar(max) NOT NULL,
        [nombre] nvarchar(max) NULL,
        [fechaHoraComentario] nvarchar(max) NOT NULL,
        [id_usuario] nvarchar(20) NULL,
        [id_noticia] int NULL,
        CONSTRAINT [PK_Comentarios] PRIMARY KEY ([id_comentario]),
        CONSTRAINT [FK_Comentarios_Noticias_id_noticia] FOREIGN KEY ([id_noticia]) REFERENCES [Noticias] ([id_noticia]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Comentarios_Usuarios_id_usuario] FOREIGN KEY ([id_usuario]) REFERENCES [Usuarios] ([dni]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE INDEX [IX_Clases_id_profesor] ON [Clases] ([id_profesor]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE INDEX [IX_Comentarios_id_noticia] ON [Comentarios] ([id_noticia]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE INDEX [IX_Comentarios_id_usuario] ON [Comentarios] ([id_usuario]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE INDEX [IX_CursoClases_IdClase] ON [CursoClases] ([IdClase]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE INDEX [IX_Notas_id_alumno] ON [Notas] ([id_alumno]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE INDEX [IX_Notas_id_clase] ON [Notas] ([id_clase]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE INDEX [IX_Notas_id_docente] ON [Notas] ([id_docente]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE INDEX [IX_Noticias_id_usuario] ON [Noticias] ([id_usuario]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE INDEX [IX_Pagos_id_usuario] ON [Pagos] ([id_usuario]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE INDEX [IX_SolicitudesInscripcion_id_usuario] ON [SolicitudesInscripcion] ([id_usuario]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE UNIQUE INDEX [IX_Usuarios_correo] ON [Usuarios] ([correo]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE UNIQUE INDEX [IX_Usuarios_dni] ON [Usuarios] ([dni]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE INDEX [IX_Usuarios_id_rol] ON [Usuarios] ([id_rol]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE INDEX [IX_UsuariosClases_IdClase] ON [UsuariosClases] ([IdClase]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    CREATE INDEX [IX_UsuariosCursos_IdCurso] ON [UsuariosCursos] ([IdCurso]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113130241_mssql_migration_811')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231113130241_mssql_migration_811', N'7.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113132306_mssql_migration_492')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231113132306_mssql_migration_492', N'7.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231113133053_mssql_migration_720')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231113133053_mssql_migration_720', N'7.0.13');
END;
GO

COMMIT;
GO

