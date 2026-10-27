using System.Drawing;
using System.Windows.Forms;

namespace Biblioteca.Presentacion.Utilidades
{
    /// <summary>Apariencia común de las tablas (DataGridView).</summary>
    public static class Estilos
    {
        public static readonly Color ColorEncabezado = Color.FromArgb(31, 78, 121);
        public static readonly Color ColorVencido = Color.FromArgb(253, 226, 226);
        public static readonly Color ColorSeleccion = Color.FromArgb(205, 226, 245);

        public static void AplicarTabla(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.FixedSingle;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorEncabezado;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.DefaultCellStyle.SelectionBackColor = ColorSeleccion;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 251);
        }

        /// <summary>Cambia los encabezados de las columnas. Recibe pares: nombre, texto.</summary>
        public static void Encabezados(DataGridView dgv, params string[] pares)
        {
            for (int i = 0; i + 1 < pares.Length; i += 2)
            {
                if (dgv.Columns.Contains(pares[i]))
                {
                    dgv.Columns[pares[i]].HeaderText = pares[i + 1];
                }
            }
        }

        /// <summary>Oculta las columnas indicadas.</summary>
        public static void Ocultar(DataGridView dgv, params string[] columnas)
        {
            foreach (string c in columnas)
            {
                if (dgv.Columns.Contains(c)) dgv.Columns[c].Visible = false;
            }
        }

        /// <summary>Da a una columna un ancho fijo (las demás se reparten el espacio).</summary>
        public static void AnchoFijo(DataGridView dgv, string columna, int ancho)
        {
            if (!dgv.Columns.Contains(columna)) return;
            dgv.Columns[columna].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns[columna].Width = ancho;
        }

        /// <summary>Devuelve el valor entero de una celda de la fila indicada (0 si no hay).</summary>
        public static int ValorEntero(DataGridView dgv, int fila, string columna)
        {
            if (fila < 0 || fila >= dgv.Rows.Count || !dgv.Columns.Contains(columna)) return 0;
            object valor = dgv.Rows[fila].Cells[columna].Value;
            int resultado;
            return valor != null && int.TryParse(valor.ToString(), out resultado) ? resultado : 0;
        }
    }
}
