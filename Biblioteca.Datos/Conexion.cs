using System.Configuration;
using System.Data.SqlClient;

namespace Biblioteca.Datos
{
    /// <summary>
    /// Administra la conexión con SQL Server.
    /// La cadena de conexión se lee del archivo App.config del proyecto
    /// Biblioteca.Presentacion (entrada "BibliotecaDB"), así se puede cambiar
    /// el servidor sin tocar el código.
    /// </summary>
    public static class Conexion
    {
        private const string NombreCadena = "BibliotecaDB";

        public static string CadenaConexion
        {
            get
            {
                ConnectionStringSettings cadena = ConfigurationManager.ConnectionStrings[NombreCadena];
                if (cadena == null || string.IsNullOrWhiteSpace(cadena.ConnectionString))
                {
                    throw new DatosException(
                        "No se encontró la cadena de conexión \"" + NombreCadena + "\" en el archivo App.config.");
                }
                return cadena.ConnectionString;
            }
        }

        /// <summary>Crea una conexión nueva (todavía cerrada).</summary>
        public static SqlConnection CrearConexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        /// <summary>Abre y cierra una conexión para comprobar que el servidor responde.</summary>
        public static bool ProbarConexion(out string mensaje)
        {
            try
            {
                using (SqlConnection cn = CrearConexion())
                {
                    cn.Open();
                    mensaje = "Conectado a " + cn.DataSource + " / " + cn.Database;
                    return true;
                }
            }
            catch (SqlException ex)
            {
                mensaje = DatosException.TraducirMensaje(ex);
                return false;
            }
            catch (DatosException ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }
    }
}
