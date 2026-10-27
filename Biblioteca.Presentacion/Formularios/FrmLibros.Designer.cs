namespace Biblioteca.Presentacion.Formularios
{
    partial class FrmLibros
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblEncabezado = new System.Windows.Forms.Label();
            this.grpDatos = new System.Windows.Forms.GroupBox();
            this.lblIsbn = new System.Windows.Forms.Label();
            this.txtIsbn = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.lblAnio = new System.Windows.Forms.Label();
            this.txtAnio = new System.Windows.Forms.TextBox();
            this.lblExistencias = new System.Windows.Forms.Label();
            this.nudExistencias = new System.Windows.Forms.NumericUpDown();
            this.lblDisponibles = new System.Windows.Forms.Label();
            this.lblEditorial = new System.Windows.Forms.Label();
            this.cboEditorial = new System.Windows.Forms.ComboBox();
            this.lblAutores = new System.Windows.Forms.Label();
            this.clbAutores = new System.Windows.Forms.CheckedListBox();
            this.lblAyudaAutores = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lblModo = new System.Windows.Forms.Label();
            this.grpBusqueda = new System.Windows.Forms.GroupBox();
            this.cboCriterio = new System.Windows.Forms.ComboBox();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnVerTodos = new System.Windows.Forms.Button();
            this.dgvLibros = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.grpDatos.SuspendLayout();
            this.grpBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExistencias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLibros)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEncabezado
            // 
            this.lblEncabezado.AutoSize = true;
            this.lblEncabezado.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncabezado.ForeColor = System.Drawing.Color.FromArgb(((31)), ((78)), ((121)));
            this.lblEncabezado.Location = new System.Drawing.Point(20, 12);
            this.lblEncabezado.Name = "lblEncabezado";
            this.lblEncabezado.TabIndex = 0;
            this.lblEncabezado.Text = "Gestión de libros";
            // 
            // grpDatos
            // 
            this.grpDatos.Location = new System.Drawing.Point(20, 55);
            this.grpDatos.Name = "grpDatos";
            this.grpDatos.Size = new System.Drawing.Size(390, 400);
            this.grpDatos.TabIndex = 1;
            this.grpDatos.Text = "Datos del libro";
            this.grpDatos.Controls.Add(this.lblIsbn);
            this.grpDatos.Controls.Add(this.txtIsbn);
            this.grpDatos.Controls.Add(this.lblTitulo);
            this.grpDatos.Controls.Add(this.txtTitulo);
            this.grpDatos.Controls.Add(this.lblAnio);
            this.grpDatos.Controls.Add(this.txtAnio);
            this.grpDatos.Controls.Add(this.lblExistencias);
            this.grpDatos.Controls.Add(this.nudExistencias);
            this.grpDatos.Controls.Add(this.lblDisponibles);
            this.grpDatos.Controls.Add(this.lblEditorial);
            this.grpDatos.Controls.Add(this.cboEditorial);
            this.grpDatos.Controls.Add(this.lblAutores);
            this.grpDatos.Controls.Add(this.clbAutores);
            this.grpDatos.Controls.Add(this.lblAyudaAutores);
            // 
            // lblIsbn
            // 
            this.lblIsbn.AutoSize = true;
            this.lblIsbn.Location = new System.Drawing.Point(15, 31);
            this.lblIsbn.Name = "lblIsbn";
            this.lblIsbn.TabIndex = 2;
            this.lblIsbn.Text = "ISBN *";
            // 
            // txtIsbn
            // 
            this.txtIsbn.Location = new System.Drawing.Point(140, 28);
            this.txtIsbn.Name = "txtIsbn";
            this.txtIsbn.Size = new System.Drawing.Size(230, 25);
            this.txtIsbn.TabIndex = 3;
            this.txtIsbn.MaxLength = 17;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(15, 66);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.TabIndex = 4;
            this.lblTitulo.Text = "Título *";
            // 
            // txtTitulo
            // 
            this.txtTitulo.Location = new System.Drawing.Point(140, 63);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(230, 25);
            this.txtTitulo.TabIndex = 5;
            this.txtTitulo.MaxLength = 150;
            // 
            // lblAnio
            // 
            this.lblAnio.AutoSize = true;
            this.lblAnio.Location = new System.Drawing.Point(15, 101);
            this.lblAnio.Name = "lblAnio";
            this.lblAnio.TabIndex = 6;
            this.lblAnio.Text = "Año publicación";
            // 
            // txtAnio
            // 
            this.txtAnio.Location = new System.Drawing.Point(140, 98);
            this.txtAnio.Name = "txtAnio";
            this.txtAnio.Size = new System.Drawing.Size(80, 25);
            this.txtAnio.TabIndex = 7;
            this.txtAnio.MaxLength = 4;
            // 
            // lblExistencias
            // 
            this.lblExistencias.AutoSize = true;
            this.lblExistencias.Location = new System.Drawing.Point(15, 136);
            this.lblExistencias.Name = "lblExistencias";
            this.lblExistencias.TabIndex = 8;
            this.lblExistencias.Text = "Existencias *";
            // 
            // nudExistencias
            // 
            this.nudExistencias.Location = new System.Drawing.Point(140, 133);
            this.nudExistencias.Name = "nudExistencias";
            this.nudExistencias.Size = new System.Drawing.Size(80, 25);
            this.nudExistencias.TabIndex = 9;
            this.nudExistencias.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudExistencias.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblDisponibles
            // 
            this.lblDisponibles.AutoSize = true;
            this.lblDisponibles.ForeColor = System.Drawing.Color.FromArgb(((90)), ((90)), ((90)));
            this.lblDisponibles.Location = new System.Drawing.Point(230, 136);
            this.lblDisponibles.Name = "lblDisponibles";
            this.lblDisponibles.TabIndex = 10;
            this.lblDisponibles.Text = "Disponibles: -";
            // 
            // lblEditorial
            // 
            this.lblEditorial.AutoSize = true;
            this.lblEditorial.Location = new System.Drawing.Point(15, 171);
            this.lblEditorial.Name = "lblEditorial";
            this.lblEditorial.TabIndex = 11;
            this.lblEditorial.Text = "Editorial *";
            // 
            // cboEditorial
            // 
            this.cboEditorial.Location = new System.Drawing.Point(140, 168);
            this.cboEditorial.Name = "cboEditorial";
            this.cboEditorial.Size = new System.Drawing.Size(230, 25);
            this.cboEditorial.TabIndex = 12;
            this.cboEditorial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEditorial.FormattingEnabled = true;
            // 
            // lblAutores
            // 
            this.lblAutores.AutoSize = true;
            this.lblAutores.Location = new System.Drawing.Point(15, 206);
            this.lblAutores.Name = "lblAutores";
            this.lblAutores.TabIndex = 13;
            this.lblAutores.Text = "Autores *";
            // 
            // clbAutores
            // 
            this.clbAutores.Location = new System.Drawing.Point(140, 204);
            this.clbAutores.Name = "clbAutores";
            this.clbAutores.Size = new System.Drawing.Size(230, 148);
            this.clbAutores.TabIndex = 14;
            this.clbAutores.CheckOnClick = true;
            this.clbAutores.FormattingEnabled = true;
            // 
            // lblAyudaAutores
            // 
            this.lblAyudaAutores.AutoSize = true;
            this.lblAyudaAutores.ForeColor = System.Drawing.Color.FromArgb(((90)), ((90)), ((90)));
            this.lblAyudaAutores.Location = new System.Drawing.Point(140, 362);
            this.lblAyudaAutores.Name = "lblAyudaAutores";
            this.lblAyudaAutores.TabIndex = 15;
            this.lblAyudaAutores.Text = "Marque uno o varios autores.";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(20, 470);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 34);
            this.btnGuardar.TabIndex = 16;
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(150, 470);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(120, 34);
            this.btnEliminar.TabIndex = 17;
            this.btnEliminar.Text = "Eli&minar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(280, 470);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(120, 34);
            this.btnLimpiar.TabIndex = 18;
            this.btnLimpiar.Text = "&Nuevo / Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // lblModo
            // 
            this.lblModo.AutoSize = false;
            this.lblModo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModo.ForeColor = System.Drawing.Color.FromArgb(((90)), ((90)), ((90)));
            this.lblModo.Location = new System.Drawing.Point(20, 520);
            this.lblModo.Name = "lblModo";
            this.lblModo.Size = new System.Drawing.Size(380, 40);
            this.lblModo.TabIndex = 19;
            this.lblModo.Text = "Modo: nuevo registro";
            // 
            // grpBusqueda
            // 
            this.grpBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBusqueda.Location = new System.Drawing.Point(430, 55);
            this.grpBusqueda.Name = "grpBusqueda";
            this.grpBusqueda.Size = new System.Drawing.Size(550, 70);
            this.grpBusqueda.TabIndex = 20;
            this.grpBusqueda.Text = "Buscar";
            this.grpBusqueda.Controls.Add(this.cboCriterio);
            this.grpBusqueda.Controls.Add(this.txtBuscar);
            this.grpBusqueda.Controls.Add(this.btnBuscar);
            this.grpBusqueda.Controls.Add(this.btnVerTodos);
            // 
            // cboCriterio
            // 
            this.cboCriterio.Location = new System.Drawing.Point(15, 28);
            this.cboCriterio.Name = "cboCriterio";
            this.cboCriterio.Size = new System.Drawing.Size(130, 25);
            this.cboCriterio.TabIndex = 21;
            this.cboCriterio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCriterio.FormattingEnabled = true;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(155, 28);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(195, 25);
            this.txtBuscar.TabIndex = 22;
            this.txtBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscar_KeyDown);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(360, 26);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(85, 29);
            this.btnBuscar.TabIndex = 23;
            this.btnBuscar.Text = "&Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnVerTodos
            // 
            this.btnVerTodos.Location = new System.Drawing.Point(450, 26);
            this.btnVerTodos.Name = "btnVerTodos";
            this.btnVerTodos.Size = new System.Drawing.Size(85, 29);
            this.btnVerTodos.TabIndex = 24;
            this.btnVerTodos.Text = "&Ver todos";
            this.btnVerTodos.UseVisualStyleBackColor = true;
            this.btnVerTodos.Click += new System.EventHandler(this.btnVerTodos_Click);
            // 
            // dgvLibros
            // 
            this.dgvLibros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLibros.Location = new System.Drawing.Point(430, 135);
            this.dgvLibros.Name = "dgvLibros";
            this.dgvLibros.Size = new System.Drawing.Size(550, 445);
            this.dgvLibros.TabIndex = 25;
            this.dgvLibros.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLibros_CellClick);
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((90)), ((90)), ((90)));
            this.lblTotal.Location = new System.Drawing.Point(430, 588);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.TabIndex = 26;
            this.lblTotal.Text = "Registros: 0";
            // 
            // FrmLibros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmLibros";
            this.Text = "Libros";
            this.ClientSize = new System.Drawing.Size(1000, 620);
            this.Controls.Add(this.lblEncabezado);
            this.Controls.Add(this.grpDatos);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.lblModo);
            this.Controls.Add(this.grpBusqueda);
            this.Controls.Add(this.dgvLibros);
            this.Controls.Add(this.lblTotal);
            this.Load += new System.EventHandler(this.FrmLibros_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudExistencias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLibros)).EndInit();
            this.grpDatos.ResumeLayout(false);
            this.grpDatos.PerformLayout();
            this.grpBusqueda.ResumeLayout(false);
            this.grpBusqueda.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEncabezado;
        private System.Windows.Forms.GroupBox grpDatos;
        private System.Windows.Forms.Label lblIsbn;
        private System.Windows.Forms.TextBox txtIsbn;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label lblAnio;
        private System.Windows.Forms.TextBox txtAnio;
        private System.Windows.Forms.Label lblExistencias;
        private System.Windows.Forms.NumericUpDown nudExistencias;
        private System.Windows.Forms.Label lblDisponibles;
        private System.Windows.Forms.Label lblEditorial;
        private System.Windows.Forms.ComboBox cboEditorial;
        private System.Windows.Forms.Label lblAutores;
        private System.Windows.Forms.CheckedListBox clbAutores;
        private System.Windows.Forms.Label lblAyudaAutores;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblModo;
        private System.Windows.Forms.GroupBox grpBusqueda;
        private System.Windows.Forms.ComboBox cboCriterio;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnVerTodos;
        private System.Windows.Forms.DataGridView dgvLibros;
        private System.Windows.Forms.Label lblTotal;
    }
}
