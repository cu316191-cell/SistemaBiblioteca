namespace Biblioteca.Presentacion.Formularios
{
    partial class FrmPrestamos
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
            this.lblUsuario = new System.Windows.Forms.Label();
            this.cboUsuario = new System.Windows.Forms.ComboBox();
            this.lblLibro = new System.Windows.Forms.Label();
            this.cboLibro = new System.Windows.Forms.ComboBox();
            this.lblFechaPrestamo = new System.Windows.Forms.Label();
            this.dtpFechaPrestamo = new System.Windows.Forms.DateTimePicker();
            this.lblFechaLimite = new System.Windows.Forms.Label();
            this.dtpFechaLimite = new System.Windows.Forms.DateTimePicker();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.grpDevolucion = new System.Windows.Forms.GroupBox();
            this.lblFechaDevolucion = new System.Windows.Forms.Label();
            this.dtpFechaDevolucion = new System.Windows.Forms.DateTimePicker();
            this.btnDevolver = new System.Windows.Forms.Button();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.grpFiltro = new System.Windows.Forms.GroupBox();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.cboFiltro = new System.Windows.Forms.ComboBox();
            this.lblLeyenda = new System.Windows.Forms.Label();
            this.dgvPrestamos = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.grpDatos.SuspendLayout();
            this.grpDevolucion.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrestamos)).BeginInit();
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
            this.lblEncabezado.Text = "Préstamos y devoluciones";
            // 
            // grpDatos
            // 
            this.grpDatos.Location = new System.Drawing.Point(20, 55);
            this.grpDatos.Name = "grpDatos";
            this.grpDatos.Size = new System.Drawing.Size(390, 225);
            this.grpDatos.TabIndex = 1;
            this.grpDatos.Text = "Datos del préstamo";
            this.grpDatos.Controls.Add(this.lblUsuario);
            this.grpDatos.Controls.Add(this.cboUsuario);
            this.grpDatos.Controls.Add(this.lblLibro);
            this.grpDatos.Controls.Add(this.cboLibro);
            this.grpDatos.Controls.Add(this.lblFechaPrestamo);
            this.grpDatos.Controls.Add(this.dtpFechaPrestamo);
            this.grpDatos.Controls.Add(this.lblFechaLimite);
            this.grpDatos.Controls.Add(this.dtpFechaLimite);
            this.grpDatos.Controls.Add(this.lblEstado);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(15, 33);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.TabIndex = 2;
            this.lblUsuario.Text = "Usuario *";
            // 
            // cboUsuario
            // 
            this.cboUsuario.Location = new System.Drawing.Point(140, 30);
            this.cboUsuario.Name = "cboUsuario";
            this.cboUsuario.Size = new System.Drawing.Size(235, 25);
            this.cboUsuario.TabIndex = 3;
            this.cboUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsuario.FormattingEnabled = true;
            // 
            // lblLibro
            // 
            this.lblLibro.AutoSize = true;
            this.lblLibro.Location = new System.Drawing.Point(15, 73);
            this.lblLibro.Name = "lblLibro";
            this.lblLibro.TabIndex = 4;
            this.lblLibro.Text = "Libro *";
            // 
            // cboLibro
            // 
            this.cboLibro.Location = new System.Drawing.Point(140, 70);
            this.cboLibro.Name = "cboLibro";
            this.cboLibro.Size = new System.Drawing.Size(235, 25);
            this.cboLibro.TabIndex = 5;
            this.cboLibro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLibro.FormattingEnabled = true;
            // 
            // lblFechaPrestamo
            // 
            this.lblFechaPrestamo.AutoSize = true;
            this.lblFechaPrestamo.Location = new System.Drawing.Point(15, 113);
            this.lblFechaPrestamo.Name = "lblFechaPrestamo";
            this.lblFechaPrestamo.TabIndex = 6;
            this.lblFechaPrestamo.Text = "Fecha préstamo *";
            // 
            // dtpFechaPrestamo
            // 
            this.dtpFechaPrestamo.Location = new System.Drawing.Point(140, 110);
            this.dtpFechaPrestamo.Name = "dtpFechaPrestamo";
            this.dtpFechaPrestamo.Size = new System.Drawing.Size(130, 25);
            this.dtpFechaPrestamo.TabIndex = 7;
            this.dtpFechaPrestamo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaPrestamo.ValueChanged += new System.EventHandler(this.dtpFechaPrestamo_ValueChanged);
            // 
            // lblFechaLimite
            // 
            this.lblFechaLimite.AutoSize = true;
            this.lblFechaLimite.Location = new System.Drawing.Point(15, 153);
            this.lblFechaLimite.Name = "lblFechaLimite";
            this.lblFechaLimite.TabIndex = 8;
            this.lblFechaLimite.Text = "Fecha límite *";
            // 
            // dtpFechaLimite
            // 
            this.dtpFechaLimite.Location = new System.Drawing.Point(140, 150);
            this.dtpFechaLimite.Name = "dtpFechaLimite";
            this.dtpFechaLimite.Size = new System.Drawing.Size(130, 25);
            this.dtpFechaLimite.TabIndex = 9;
            this.dtpFechaLimite.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((90)), ((90)), ((90)));
            this.lblEstado.Location = new System.Drawing.Point(15, 192);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.TabIndex = 10;
            this.lblEstado.Text = "Estado: nuevo préstamo";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(20, 292);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(185, 34);
            this.btnGuardar.TabIndex = 11;
            this.btnGuardar.Text = "Re&gistrar préstamo";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(215, 292);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(95, 34);
            this.btnEliminar.TabIndex = 12;
            this.btnEliminar.Text = "Eli&minar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(315, 292);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(95, 34);
            this.btnLimpiar.TabIndex = 13;
            this.btnLimpiar.Text = "&Nuevo";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // grpDevolucion
            // 
            this.grpDevolucion.Location = new System.Drawing.Point(20, 340);
            this.grpDevolucion.Name = "grpDevolucion";
            this.grpDevolucion.Size = new System.Drawing.Size(390, 110);
            this.grpDevolucion.TabIndex = 14;
            this.grpDevolucion.Text = "Devolución";
            this.grpDevolucion.Controls.Add(this.lblFechaDevolucion);
            this.grpDevolucion.Controls.Add(this.dtpFechaDevolucion);
            this.grpDevolucion.Controls.Add(this.btnDevolver);
            // 
            // lblFechaDevolucion
            // 
            this.lblFechaDevolucion.AutoSize = true;
            this.lblFechaDevolucion.Location = new System.Drawing.Point(15, 33);
            this.lblFechaDevolucion.Name = "lblFechaDevolucion";
            this.lblFechaDevolucion.TabIndex = 15;
            this.lblFechaDevolucion.Text = "Fecha devolución";
            // 
            // dtpFechaDevolucion
            // 
            this.dtpFechaDevolucion.Location = new System.Drawing.Point(140, 30);
            this.dtpFechaDevolucion.Name = "dtpFechaDevolucion";
            this.dtpFechaDevolucion.Size = new System.Drawing.Size(130, 25);
            this.dtpFechaDevolucion.TabIndex = 16;
            this.dtpFechaDevolucion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            // 
            // btnDevolver
            // 
            this.btnDevolver.Location = new System.Drawing.Point(140, 65);
            this.btnDevolver.Name = "btnDevolver";
            this.btnDevolver.Size = new System.Drawing.Size(180, 32);
            this.btnDevolver.TabIndex = 17;
            this.btnDevolver.Text = "Registrar &devolución";
            this.btnDevolver.UseVisualStyleBackColor = true;
            this.btnDevolver.Click += new System.EventHandler(this.btnDevolver_Click);
            // 
            // lblAyuda
            // 
            this.lblAyuda.AutoSize = false;
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((90)), ((90)), ((90)));
            this.lblAyuda.Location = new System.Drawing.Point(20, 460);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(390, 120);
            this.lblAyuda.TabIndex = 18;
            this.lblAyuda.Text = "Para devolver un libro o modificar un préstamo, selecciónelo en la tabla. Reglas: máximo 3 préstamos activos por usuario, plazo máximo de 30 días y no se presta a usuarios con préstamos vencidos.";
            // 
            // grpFiltro
            // 
            this.grpFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFiltro.Location = new System.Drawing.Point(430, 55);
            this.grpFiltro.Name = "grpFiltro";
            this.grpFiltro.Size = new System.Drawing.Size(550, 70);
            this.grpFiltro.TabIndex = 19;
            this.grpFiltro.Text = "Filtrar préstamos";
            this.grpFiltro.Controls.Add(this.lblFiltro);
            this.grpFiltro.Controls.Add(this.cboFiltro);
            this.grpFiltro.Controls.Add(this.lblLeyenda);
            // 
            // lblFiltro
            // 
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Location = new System.Drawing.Point(15, 31);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.TabIndex = 20;
            this.lblFiltro.Text = "Estado";
            // 
            // cboFiltro
            // 
            this.cboFiltro.Location = new System.Drawing.Point(75, 28);
            this.cboFiltro.Name = "cboFiltro";
            this.cboFiltro.Size = new System.Drawing.Size(180, 25);
            this.cboFiltro.TabIndex = 21;
            this.cboFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltro.FormattingEnabled = true;
            this.cboFiltro.SelectedIndexChanged += new System.EventHandler(this.cboFiltro_SelectedIndexChanged);
            // 
            // lblLeyenda
            // 
            this.lblLeyenda.AutoSize = true;
            this.lblLeyenda.ForeColor = System.Drawing.Color.FromArgb(((90)), ((90)), ((90)));
            this.lblLeyenda.Location = new System.Drawing.Point(270, 31);
            this.lblLeyenda.Name = "lblLeyenda";
            this.lblLeyenda.TabIndex = 22;
            this.lblLeyenda.Text = "Las filas en rojo son préstamos vencidos.";
            // 
            // dgvPrestamos
            // 
            this.dgvPrestamos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPrestamos.Location = new System.Drawing.Point(430, 135);
            this.dgvPrestamos.Name = "dgvPrestamos";
            this.dgvPrestamos.Size = new System.Drawing.Size(550, 445);
            this.dgvPrestamos.TabIndex = 23;
            this.dgvPrestamos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPrestamos_CellClick);
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((90)), ((90)), ((90)));
            this.lblTotal.Location = new System.Drawing.Point(430, 588);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.TabIndex = 24;
            this.lblTotal.Text = "Registros: 0";
            // 
            // FrmPrestamos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmPrestamos";
            this.Text = "Préstamos";
            this.ClientSize = new System.Drawing.Size(1000, 620);
            this.Controls.Add(this.lblEncabezado);
            this.Controls.Add(this.grpDatos);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.grpDevolucion);
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.grpFiltro);
            this.Controls.Add(this.dgvPrestamos);
            this.Controls.Add(this.lblTotal);
            this.Load += new System.EventHandler(this.FrmPrestamos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrestamos)).EndInit();
            this.grpDatos.ResumeLayout(false);
            this.grpDatos.PerformLayout();
            this.grpDevolucion.ResumeLayout(false);
            this.grpDevolucion.PerformLayout();
            this.grpFiltro.ResumeLayout(false);
            this.grpFiltro.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEncabezado;
        private System.Windows.Forms.GroupBox grpDatos;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ComboBox cboUsuario;
        private System.Windows.Forms.Label lblLibro;
        private System.Windows.Forms.ComboBox cboLibro;
        private System.Windows.Forms.Label lblFechaPrestamo;
        private System.Windows.Forms.DateTimePicker dtpFechaPrestamo;
        private System.Windows.Forms.Label lblFechaLimite;
        private System.Windows.Forms.DateTimePicker dtpFechaLimite;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.GroupBox grpDevolucion;
        private System.Windows.Forms.Label lblFechaDevolucion;
        private System.Windows.Forms.DateTimePicker dtpFechaDevolucion;
        private System.Windows.Forms.Button btnDevolver;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.GroupBox grpFiltro;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.ComboBox cboFiltro;
        private System.Windows.Forms.Label lblLeyenda;
        private System.Windows.Forms.DataGridView dgvPrestamos;
        private System.Windows.Forms.Label lblTotal;
    }
}
