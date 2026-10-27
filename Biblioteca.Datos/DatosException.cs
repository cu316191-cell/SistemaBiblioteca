using System;
using System.Data.SqlClient;

namespace Biblioteca.Datos
{
    /// <summary>
    /// Error ocurrido al trabajar con la base de datos. Convierte los errores
    /// técnicos de SQL Server en mensajes que el usuario pueda entender.
    /// </summary>
    public class DatosException : Exception
    {
        public DatosException(string mensaje) : base(mensaje)
        {
        }

        public DatosException(string mensaje, Exception interna) : base(mensaje, interna)
        {
        }

        /// <summary>Crea la excepción a partir de un error de SQL Server.</summary>
        public static DatosException DesdeSql(SqlException ex)
        {
            return new DatosException(TraducirMensaje(ex), ex);
        }

        public static string TraducirMensaje(SqlException ex)
        {
            switch (ex.Number)
            {
                case 2627: // violación de UNIQUE o PRIMARY KEY
                case 2601: // índice único duplicado
                    return "Ya existe un registro con ese dato. Revise los campos que no se pueden repetir.";
                case 547:  // violación de FOREIGN KEY o CHECK
                    return "La operación no se puede realizar porque el registro está relacionado con otros datos "
                         + "o porque algún valor no cumple las reglas de la base de datos.";
                case 8152: // texto más largo que la columna
                case 2628:
                    return "Uno de los textos supera la longitud permitida.";
                case 4060: // base de datos no existe o sin permiso
                    return "No se pudo abrir la base de datos BibliotecaDB. Verifique que el script SQL se haya ejecutado.";
                case 18456: // error de inicio de sesión
                    return "SQL Server rechazó el inicio de sesión. Revise el usuario o la seguridad integrada en App.config.";
                case -2:   // tiempo de espera agotado
                    return "La base de datos tardó demasiado en responder. Intente de nuevo.";
                case 2:
                case 53:
                case -1:
                    return "No se pudo conectar con SQL Server. Verifique que el servicio esté encendido "
                         + "y que el nombre del servidor en App.config sea correcto.";
                default:
                    return "Error de base de datos: " + ex.Message;
            }
        }
    }
}
