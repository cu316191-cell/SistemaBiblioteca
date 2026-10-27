# Sistema de Gestión de Biblioteca

Aplicación de escritorio para administrar la biblioteca de una institución educativa: libros, autores, editoriales, usuarios, préstamos, devoluciones y reportes.

Proyecto del ACA de **Programación Avanzada**, desarrollado en **C#**, **Windows Forms** y **SQL Server** con arquitectura por capas.

## Tecnologías

| Elemento | Versión usada |
|---|---|
| Lenguaje | C# 7.3 |
| Plataforma | .NET Framework 4.8 (Windows Forms) |
| Base de datos | SQL Server 2017 o superior (probado con SQL Server 2025 Express) |
| Acceso a datos | ADO.NET (`System.Data.SqlClient`) con consultas parametrizadas |
| IDE | Visual Studio 2022 o superior (carga de trabajo "Desarrollo de escritorio de .NET") |
| Control de versiones | Git y GitHub |

El proyecto no usa paquetes NuGet externos.

## Estructura del repositorio

```
SistemaBiblioteca/
├── SistemaBiblioteca.sln            Solución de Visual Studio
├── Biblioteca.Entidades/            Clases del dominio (Libro, Autor, Usuario, Prestamo...)
├── Biblioteca.Datos/                Conexión con SQL Server y consultas (DAO)
├── Biblioteca.Negocio/              Reglas y validaciones del sistema
├── Biblioteca.Presentacion/         Formularios Windows Forms (proyecto de inicio)
├── BaseDatos/BibliotecaDB.sql       Script de la base de datos con datos de prueba
├── Documentacion/Diagramas/         Diagramas en draw.io (.drawio) y en imagen (.png)
└── Capturas/                        Capturas de pantalla del sistema
```

## Arquitectura por capas

```
Presentación (Windows Forms)  →  Negocio (reglas y validaciones)  →  Datos (ADO.NET)  →  SQL Server
                 ↘                         ↓                          ↙
                              Entidades (clases del dominio)
```

- **Presentación:** formularios, captura de datos y mensajes al usuario. No conoce la capa de datos.
- **Negocio:** valida la información y aplica las reglas antes de guardar.
- **Datos:** abre la conexión, ejecuta consultas parametrizadas y traduce los errores de SQL Server a mensajes claros.
- **Entidades:** clases que viajan entre las capas.

## Cómo ejecutar el proyecto

1. **Crear la base de datos**
   - Abra **SQL Server Management Studio** y conéctese a su servidor.
   - Abra el archivo `BaseDatos/BibliotecaDB.sql` y presione **Ejecutar** (F5).
   - El script crea la base `BibliotecaDB`, las tablas, la vista y los datos de prueba. Si la base ya existe, la borra y la vuelve a crear.

2. **Revisar la cadena de conexión**
   - Abra `Biblioteca.Presentacion/App.config`.
   - En `Data Source` escriba el nombre de su servidor tal como aparece al conectarse en SSMS. Por defecto es `.\SQLEXPRESS`.
   ```xml
   <add name="BibliotecaDB"
        connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=BibliotecaDB;Integrated Security=True"
        providerName="System.Data.SqlClient" />
   ```

3. **Abrir y ejecutar**
   - Abra `SistemaBiblioteca.sln` en Visual Studio 2022.
   - Verifique que **Biblioteca.Presentacion** sea el proyecto de inicio (clic derecho → *Establecer como proyecto de inicio*).
   - Presione **F5**. La pantalla de inicio muestra si la conexión con SQL Server es correcta.

## Módulos

| Módulo | Funciones |
|---|---|
| Inicio | Estado de la conexión y resumen (libros, usuarios, préstamos activos y vencidos) |
| Libros | Registrar, consultar, editar y eliminar; buscar por código/ISBN, título o autor; asignar editorial y uno o varios autores; ver disponibles |
| Usuarios | Registrar, consultar, editar y eliminar; buscar por documento, nombre o apellido |
| Autores | Registrar, consultar, editar y eliminar |
| Editoriales | Registrar, consultar, editar y eliminar |
| Préstamos | Registrar préstamo, modificarlo, eliminarlo, registrar devolución y filtrar por estado (activos, devueltos, vencidos) |
| Reportes | Resumen general, libros, autores, editoriales, usuarios, personas registradas, préstamos activos, vencidos, devueltos, historial e historial por usuario |

## Reglas del sistema

- Campos obligatorios, longitudes máximas y formatos (documento y teléfono solo con números, correo válido, ISBN de 10 o 13 dígitos).
- No se repiten el ISBN de un libro, el documento de un usuario ni el nombre de una editorial.
- No se elimina un autor o una editorial con libros asociados, ni un libro o un usuario con préstamos en el historial.
- Un préstamo solo se registra si el libro tiene ejemplares disponibles. Los disponibles se calculan como existencias menos préstamos activos.
- Cada usuario puede tener máximo 3 préstamos activos y no puede pedir libros si tiene préstamos vencidos.
- El plazo del préstamo es de máximo 30 días (14 por defecto).
- Al registrar la devolución, el estado pasa a "Devuelto" y el ejemplar vuelve a estar disponible.

## Base de datos

Tablas: `Editoriales`, `Autores`, `Libros`, `LibroAutor` (relación muchos a muchos entre libros y autores), `Usuarios` y `Prestamos`.
Vista: `vw_LibrosDisponibilidad`.

El modelo entidad-relación y los demás diagramas están en `Documentacion/Diagramas`. Los archivos `.drawio` se abren en [draw.io](https://app.diagrams.net).

## Capturas de pantalla

Las capturas del sistema en funcionamiento están en la carpeta [`Capturas`](Capturas).
