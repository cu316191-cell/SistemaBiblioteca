using System.Collections.Generic;
using System.Data;
using Biblioteca.Datos;
using Biblioteca.Entidades;

namespace Biblioteca.Negocio
{
    /// <summary>Reportes disponibles en la aplicación.</summary>
    public enum TipoReporte
    {
        ResumenGeneral,
        LibrosRegistrados,
        Autores,
        Editoriales,
        UsuariosRegistrados,
        PersonasRegistradas,
        PrestamosActivos,
        PrestamosVencidos,
        PrestamosDevueltos,
        HistorialPrestamos,
        HistorialPorUsuario
    }

    /// <summary>Arma los reportes del sistema.</summary>
    public class ReporteNegocio
    {
        private readonly ReporteDAO _dao = new ReporteDAO();
        private readonly AutorDAO _autorDao = new AutorDAO();
        private readonly UsuarioDAO _usuarioDao = new UsuarioDAO();

        /// <summary>Nombre que se muestra en la lista de reportes.</summary>
        public static string Titulo(TipoReporte tipo)
        {
            switch (tipo)
            {
                case TipoReporte.ResumenGeneral: return "Resumen general de la biblioteca";
                case TipoReporte.LibrosRegistrados: return "Listado de libros registrados";
                case TipoReporte.Autores: return "Listado de autores";
                case TipoReporte.Editoriales: return "Listado de editoriales";
                case TipoReporte.UsuariosRegistrados: return "Listado de usuarios registrados";
                case TipoReporte.PersonasRegistradas: return "Personas registradas (autores y usuarios)";
                case TipoReporte.PrestamosActivos: return "Préstamos activos";
                case TipoReporte.PrestamosVencidos: return "Préstamos vencidos";
                case TipoReporte.PrestamosDevueltos: return "Préstamos devueltos";
                case TipoReporte.HistorialPrestamos: return "Historial de préstamos";
                case TipoReporte.HistorialPorUsuario: return "Historial de préstamos de un usuario";
                default: return tipo.ToString();
            }
        }

        public DataTable Generar(TipoReporte tipo, int idUsuario)
        {
            switch (tipo)
            {
                case TipoReporte.ResumenGeneral: return _dao.Resumen();
                case TipoReporte.LibrosRegistrados: return _dao.LibrosRegistrados();
                case TipoReporte.Autores: return _dao.AutoresConLibros();
                case TipoReporte.Editoriales: return _dao.EditorialesConLibros();
                case TipoReporte.UsuariosRegistrados: return _dao.UsuariosRegistrados();
                case TipoReporte.PersonasRegistradas: return PersonasRegistradas();
                case TipoReporte.PrestamosActivos: return _dao.Prestamos(EstadoPrestamo.Prestado, false);
                case TipoReporte.PrestamosVencidos: return _dao.Prestamos(null, true);
                case TipoReporte.PrestamosDevueltos: return _dao.Prestamos(EstadoPrestamo.Devuelto, false);
                case TipoReporte.HistorialPrestamos: return _dao.Prestamos(null, false);
                case TipoReporte.HistorialPorUsuario:
                    if (idUsuario <= 0)
                    {
                        throw new ValidacionException("Seleccione el usuario para ver su historial.");
                    }
                    return _dao.HistorialUsuario(idUsuario);
                default:
                    throw new ValidacionException("Seleccione un reporte.");
            }
        }

        /// <summary>
        /// Une autores y usuarios en una sola lista de tipo Persona.
        /// Cada objeto responde con su propia versión de TipoPersona y
        /// ObtenerDescripcion() (polimorfismo).
        /// </summary>
        private DataTable PersonasRegistradas()
        {
            List<Persona> personas = new List<Persona>();
            personas.AddRange(_autorDao.Listar());
            personas.AddRange(_usuarioDao.Listar());

            DataTable tabla = new DataTable();
            tabla.Columns.Add("Tipo");
            tabla.Columns.Add("Nombre completo");
            tabla.Columns.Add("Descripción");
            foreach (Persona persona in personas)
            {
                tabla.Rows.Add(persona.TipoPersona, persona.NombreCompleto, persona.ObtenerDescripcion());
            }
            return tabla;
        }
    }
}
