/* =====================================================================
   SISTEMA DE GESTIÓN DE BIBLIOTECA
   Script de creación de la base de datos BibliotecaDB (SQL Server)

   Contenido:
     1. Creación de la base de datos
     2. Creación de tablas con claves primarias y foráneas
     3. Restricciones de integridad (UNIQUE, CHECK, DEFAULT)
     4. Vista usada por la aplicación (disponibilidad de libros)
     5. Datos de prueba

   Ejecución: abrir este archivo en SQL Server Management Studio y
   presionar "Ejecutar" (F5). El script borra y vuelve a crear la base
   de datos, así que se puede ejecutar varias veces.
   ===================================================================== */

USE master;
GO

IF DB_ID(N'BibliotecaDB') IS NOT NULL
BEGIN
    ALTER DATABASE BibliotecaDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE BibliotecaDB;
END
GO

CREATE DATABASE BibliotecaDB;
GO

USE BibliotecaDB;
GO

/* =====================================================================
   2. TABLAS
   ===================================================================== */

-- Editoriales: empresas que publican los libros
CREATE TABLE Editoriales (
    IdEditorial   INT IDENTITY(1,1) NOT NULL,
    Nombre        NVARCHAR(100)     NOT NULL,
    Pais          NVARCHAR(50)      NULL,
    Telefono      VARCHAR(15)       NULL,
    Correo        VARCHAR(100)      NULL,
    CONSTRAINT PK_Editoriales PRIMARY KEY (IdEditorial),
    CONSTRAINT UQ_Editoriales_Nombre UNIQUE (Nombre)
);
GO

-- Autores: personas que escriben los libros
CREATE TABLE Autores (
    IdAutor       INT IDENTITY(1,1) NOT NULL,
    Nombre        NVARCHAR(60)      NOT NULL,
    Apellido      NVARCHAR(60)      NOT NULL,
    Nacionalidad  NVARCHAR(50)      NULL,
    CONSTRAINT PK_Autores PRIMARY KEY (IdAutor)
);
GO

-- Libros: información bibliográfica y número de ejemplares
CREATE TABLE Libros (
    IdLibro          INT IDENTITY(1,1) NOT NULL,
    ISBN             VARCHAR(17)       NOT NULL,
    Titulo           NVARCHAR(150)     NOT NULL,
    AnioPublicacion  SMALLINT          NULL,
    Existencias      INT               NOT NULL CONSTRAINT DF_Libros_Existencias DEFAULT (1),
    IdEditorial      INT               NOT NULL,
    CONSTRAINT PK_Libros PRIMARY KEY (IdLibro),
    CONSTRAINT UQ_Libros_ISBN UNIQUE (ISBN),
    CONSTRAINT CK_Libros_Existencias CHECK (Existencias >= 0),
    CONSTRAINT CK_Libros_Anio CHECK (AnioPublicacion IS NULL OR AnioPublicacion BETWEEN 1450 AND 2100),
    CONSTRAINT FK_Libros_Editoriales FOREIGN KEY (IdEditorial)
        REFERENCES Editoriales (IdEditorial)
);
GO

-- LibroAutor: tabla intermedia de la relación muchos a muchos.
-- Un libro puede tener varios autores y un autor puede escribir varios libros.
CREATE TABLE LibroAutor (
    IdLibro  INT NOT NULL,
    IdAutor  INT NOT NULL,
    CONSTRAINT PK_LibroAutor PRIMARY KEY (IdLibro, IdAutor),
    CONSTRAINT FK_LibroAutor_Libros FOREIGN KEY (IdLibro)
        REFERENCES Libros (IdLibro) ON DELETE CASCADE,
    CONSTRAINT FK_LibroAutor_Autores FOREIGN KEY (IdAutor)
        REFERENCES Autores (IdAutor)
);
GO

-- Usuarios: personas que pueden pedir libros prestados
CREATE TABLE Usuarios (
    IdUsuario      INT IDENTITY(1,1) NOT NULL,
    Documento      VARCHAR(15)       NOT NULL,
    Nombre         NVARCHAR(60)      NOT NULL,
    Apellido       NVARCHAR(60)      NOT NULL,
    Telefono       VARCHAR(15)       NULL,
    Correo         VARCHAR(100)      NULL,
    FechaRegistro  DATE              NOT NULL CONSTRAINT DF_Usuarios_FechaRegistro DEFAULT (CAST(GETDATE() AS DATE)),
    CONSTRAINT PK_Usuarios PRIMARY KEY (IdUsuario),
    CONSTRAINT UQ_Usuarios_Documento UNIQUE (Documento)
);
GO

-- Prestamos: un usuario se lleva un ejemplar de un libro
CREATE TABLE Prestamos (
    IdPrestamo       INT IDENTITY(1,1) NOT NULL,
    IdUsuario        INT               NOT NULL,
    IdLibro          INT               NOT NULL,
    FechaPrestamo    DATE              NOT NULL,
    FechaLimite      DATE              NOT NULL,
    FechaDevolucion  DATE              NULL,
    Estado           VARCHAR(10)       NOT NULL CONSTRAINT DF_Prestamos_Estado DEFAULT ('Prestado'),
    CONSTRAINT PK_Prestamos PRIMARY KEY (IdPrestamo),
    CONSTRAINT FK_Prestamos_Usuarios FOREIGN KEY (IdUsuario)
        REFERENCES Usuarios (IdUsuario),
    CONSTRAINT FK_Prestamos_Libros FOREIGN KEY (IdLibro)
        REFERENCES Libros (IdLibro),
    CONSTRAINT CK_Prestamos_Estado CHECK (Estado IN ('Prestado', 'Devuelto')),
    CONSTRAINT CK_Prestamos_FechaLimite CHECK (FechaLimite >= FechaPrestamo),
    CONSTRAINT CK_Prestamos_FechaDevolucion CHECK (FechaDevolucion IS NULL OR FechaDevolucion >= FechaPrestamo),
    -- El estado y la fecha de devolución siempre deben coincidir
    CONSTRAINT CK_Prestamos_EstadoCoherente CHECK (
        (Estado = 'Prestado' AND FechaDevolucion IS NULL) OR
        (Estado = 'Devuelto' AND FechaDevolucion IS NOT NULL)
    )
);
GO

/* =====================================================================
   4. VISTA
   Muestra cada libro con su editorial, sus autores y cuántos ejemplares
   quedan disponibles. Los disponibles no se guardan en una columna: se
   calculan restando los préstamos activos a las existencias. Así el dato
   nunca queda desactualizado.
   ===================================================================== */
CREATE VIEW vw_LibrosDisponibilidad
AS
SELECT
    l.IdLibro,
    l.ISBN,
    l.Titulo,
    l.AnioPublicacion,
    l.IdEditorial,
    e.Nombre AS Editorial,
    ISNULL((SELECT STRING_AGG(a.Nombre + N' ' + a.Apellido, N', ')
            FROM LibroAutor la
            INNER JOIN Autores a ON a.IdAutor = la.IdAutor
            WHERE la.IdLibro = l.IdLibro), N'') AS Autores,
    l.Existencias,
    (SELECT COUNT(*) FROM Prestamos p
     WHERE p.IdLibro = l.IdLibro AND p.Estado = 'Prestado') AS Prestados,
    l.Existencias - (SELECT COUNT(*) FROM Prestamos p
                     WHERE p.IdLibro = l.IdLibro AND p.Estado = 'Prestado') AS Disponibles
FROM Libros l
INNER JOIN Editoriales e ON e.IdEditorial = l.IdEditorial;
GO

/* =====================================================================
   5. DATOS DE PRUEBA
   ===================================================================== */

INSERT INTO Editoriales (Nombre, Pais, Telefono, Correo) VALUES
(N'Editorial Sudamericana', N'Argentina', '6015550101', 'contacto@sudamericana.test'),
(N'Penguin Random House',   N'Colombia',  '6015550102', 'servicio@prh.test'),
(N'Planeta',                N'España',    '6015550103', 'info@planeta.test'),
(N'Alfaguara',              N'España',    '6015550104', 'lectores@alfaguara.test'),
(N'Norma',                  N'Colombia',  '6015550105', 'atencion@norma.test'),
(N'Pearson',                N'Estados Unidos', NULL, 'soporte@pearson.test');
GO

INSERT INTO Autores (Nombre, Apellido, Nacionalidad) VALUES
(N'Gabriel',   N'García Márquez', N'Colombiana'),   -- 1
(N'Isabel',    N'Allende',        N'Chilena'),      -- 2
(N'Mario',     N'Vargas Llosa',   N'Peruana'),      -- 3
(N'Jorge Luis',N'Borges',         N'Argentina'),    -- 4
(N'Laura',     N'Restrepo',       N'Colombiana'),   -- 5
(N'Julio',     N'Cortázar',       N'Argentina'),    -- 6
(N'Héctor',    N'Abad Faciolince',N'Colombiana'),   -- 7
(N'Adolfo',    N'Bioy Casares',   N'Argentina'),    -- 8
(N'Abraham',   N'Silberschatz',   N'Estadounidense'),-- 9
(N'Henry',     N'Korth',          N'Estadounidense');-- 10
GO

INSERT INTO Libros (ISBN, Titulo, AnioPublicacion, Existencias, IdEditorial) VALUES
('978-0-0000-0001-1', N'Cien años de soledad',              1967, 4, 1),  -- 1
('978-0-0000-0002-8', N'El amor en los tiempos del cólera', 1985, 2, 2),  -- 2
('978-0-0000-0003-5', N'La casa de los espíritus',          1982, 3, 3),  -- 3
('978-0-0000-0004-2', N'La ciudad y los perros',            1963, 2, 4),  -- 4
('978-0-0000-0005-9', N'Ficciones',                         1944, 2, 1),  -- 5
('978-0-0000-0006-6', N'Delirio',                           2004, 1, 4),  -- 6
('978-0-0000-0007-3', N'Rayuela',                           1963, 2, 1),  -- 7
('978-0-0000-0008-0', N'El olvido que seremos',             2006, 3, 3),  -- 8
('978-0-0000-0009-7', N'Crónica de una muerte anunciada',   1981, 3, 5),  -- 9
('978-0-0000-0010-3', N'Fundamentos de bases de datos',     2014, 2, 6),  -- 10
('978-0-0000-0011-0', N'Seis problemas para don Isidro Parodi', 1942, 1, 1), -- 11
('978-0-0000-0012-7', N'Los ojos del perro siberiano',      1998, 2, 5);  -- 12
GO

-- Relación libro - autor (el libro 10 y el libro 11 tienen dos autores)
INSERT INTO LibroAutor (IdLibro, IdAutor) VALUES
(1, 1), (2, 1), (3, 2), (4, 3), (5, 4), (6, 5), (7, 6), (8, 7), (9, 1),
(10, 9), (10, 10),
(11, 4), (11, 8);
GO

INSERT INTO Usuarios (Documento, Nombre, Apellido, Telefono, Correo, FechaRegistro) VALUES
('1012345678', N'Laura',    N'Gómez Ruiz',     '3001234567', 'laura.gomez@correo.test',   '2026-08-01'),
('1023456789', N'Andrés',   N'Pérez Torres',   '3012345678', 'andres.perez@correo.test',  '2026-08-03'),
('1034567890', N'Camila',   N'Rodríguez Díaz', '3023456789', 'camila.rodriguez@correo.test','2026-08-10'),
('1045678901', N'Santiago', N'Martínez León',  '3034567890', NULL,                         '2026-08-15'),
('1056789012', N'Valentina',N'López Castro',   '3045678901', 'valentina.lopez@correo.test','2026-08-20'),
('1067890123', N'Juan',     N'Ramírez Ortiz',  NULL,         'juan.ramirez@correo.test',   '2026-09-01'),
('1078901234', N'Daniela',  N'Herrera Rojas',  '3067890123', 'daniela.herrera@correo.test','2026-09-05'),
('1089012345', N'Mateo',    N'Sánchez Vargas', '3078901234', NULL,                         '2026-09-12');
GO

INSERT INTO Prestamos (IdUsuario, IdLibro, FechaPrestamo, FechaLimite, FechaDevolucion, Estado) VALUES
(1, 1,  '2026-09-01', '2026-09-15', '2026-09-14', 'Devuelto'),
(2, 3,  '2026-09-03', '2026-09-17', '2026-09-20', 'Devuelto'),
(3, 7,  '2026-09-10', '2026-09-24', '2026-09-22', 'Devuelto'),
(1, 8,  '2026-09-25', '2026-10-09', '2026-10-08', 'Devuelto'),
(4, 1,  '2026-10-01', '2026-10-15', NULL,         'Prestado'),
(5, 6,  '2026-10-05', '2026-10-19', NULL,         'Prestado'),
(2, 10, '2026-10-10', '2026-10-24', NULL,         'Prestado'),
(6, 2,  '2026-10-15', '2026-10-29', NULL,         'Prestado'),
(7, 9,  '2026-10-20', '2026-11-03', NULL,         'Prestado'),
(3, 5,  '2026-10-22', '2026-11-05', NULL,         'Prestado');
GO

-- Comprobación rápida
SELECT * FROM vw_LibrosDisponibilidad ORDER BY Titulo;
GO
