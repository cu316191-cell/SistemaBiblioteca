namespace Biblioteca.Presentacion.Formularios
{
    partial class FrmInicio
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
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.pnlLibros = new System.Windows.Forms.Panel();
            this.lblValorLibros = new System.Windows.Forms.Label();
            this.lblTextoLibros = new System.Windows.Forms.Label();
            this.pnlUsuarios = new System.Windows.Forms.Panel();
            this.lblValorUsuarios = new System.Windows.Forms.Label();
            this.lblTextoUsuarios = new System.Windows.Forms.Label();
            this.pnlActivos = new System.Windows.Forms.Panel();
            this.lblValorActivos = new System.Windows.Forms.Label();
            this.lblTextoActivos = new System.Windows.Forms.Label();
            this.pnlVencidos = new System.Windows.Forms.Panel();
            this.lblValorVencidos = new System.Windows.Forms.Label();
            this.lblTextoVencidos = new System.Windows.Forms.Label();
            this.grpConexion = new System.Windows.Forms.GroupBox();
            this.lblConexion = new System.Windows.Forms.Label();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.grpModulos = new System.Windows.Forms.GroupBox();
            this.lblModulos = new System.Windows.Forms.Label();
            this.pnlLibros.SuspendLayout();
            this.pnlUsuarios.SuspendLayout();
            this.pnlActivos.SuspendLayout();
            this.pnlVencidos.SuspendLayout();
            this.grpConexion.SuspendLayout();
            this.grpModulos.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblEncabezado
            // 
            this.lblEncabezado.AutoSize = true;
            this.lblEncabezado.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncabezado.ForeColor = System.Drawing.Color.FromArgb(((31)), ((78)), ((121)));
            this.lblEncabezado.Location = new System.Drawing.Point(20, 20);
            this.lblEncabezado.Name = "lblEncabezado";
            this.lblEncabezado.TabIndex = 0;
            this.lblEncabezado.Text = "Sistema de Gestión de Biblioteca";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = false;
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((90)), ((90)), ((90)));
            this.lblSubtitulo.Location = new System.Drawing.Point(22, 60);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(900, 45);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Administre libros, autores, editoriales y usuarios, registre préstamos y devoluciones y consulte reportes. Use el menú de la izquierda para ir a cada módulo.";
            // 
            // pnlLibros
            // 
            this.pnlLibros.BackColor = System.Drawing.Color.FromArgb(((242)), ((244)), ((247)));
            this.pnlLibros.Location = new System.Drawing.Point(20, 120);
            this.pnlLibros.Name = "pnlLibros";
            this.pnlLibros.Size = new System.Drawing.Size(225, 110);
            this.pnlLibros.TabIndex = 2;
            this.pnlLibros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLibros.Controls.Add(this.lblValorLibros);
            this.pnlLibros.Controls.Add(this.lblTextoLibros);
            // 
            // lblValorLibros
            // 
            this.lblValorLibros.AutoSize = false;
            this.lblValorLibros.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorLibros.ForeColor = System.Drawing.Color.FromArgb(((31)), ((78)), ((121)));
            this.lblValorLibros.Location = new System.Drawing.Point(10, 12);
            this.lblValorLibros.Name = "lblValorLibros";
            this.lblValorLibros.Size = new System.Drawing.Size(200, 50);
            this.lblValorLibros.TabIndex = 3;
            this.lblValorLibros.Text = "-";
            this.lblValorLibros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTextoLibros
            // 
            this.lblTextoLibros.AutoSize = true;
            this.lblTextoLibros.ForeColor = System.Drawing.Color.FromArgb(((90)), ((90)), ((90)));
            this.lblTextoLibros.Location = new System.Drawing.Point(14, 72);
            this.lblTextoLibros.Name = "lblTextoLibros";
            this.lblTextoLibros.TabIndex = 4;
            this.lblTextoLibros.Text = "Libros registrados";
            // 
            // pnlUsuarios
            // 
            this.pnlUsuarios.BackColor = System.Drawing.Color.FromArgb(((242)), ((244)), ((247)));
            this.pnlUsuarios.Location = new System.Drawing.Point(260, 120);
            this.pnlUsuarios.Name = "pnlUsuarios";
            this.pnlUsuarios.Size = new System.Drawing.Size(225, 110);
            this.pnlUsuarios.TabIndex = 5;
            this.pnlUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUsuarios.Controls.Add(this.lblValorUsuarios);
            this.pnlUsuarios.Controls.Add(this.lblTextoUsuarios);
            // 
            // lblValorUsuarios
            // 
            this.lblValorUsuarios.AutoSize = false;
            this.lblValorUsuarios.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorUsuarios.ForeColor = System.Drawing.Color.FromArgb(((31)), ((78)), ((121)));
            this.lblValorUsuarios.Location = new System.Drawing.Point(10, 12);
            this.lblValorUsuarios.Name = "lblValorUsuarios";
            this.lblValorUsuarios.Size = new System.Drawing.Size(200, 50);
            this.lblValorUsuarios.TabIndex = 6;
            this.lblValorUsuarios.Text = "-";
            this.lblValorUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTextoUsuarios
            // 
            this.lblTextoUsuarios.AutoSize = true;
            this.lblTextoUsuarios.ForeColor = System.Drawing.Color.FromArgb(((90)), ((90)), ((90)));
            this.lblTextoUsuarios.Location = new System.Drawing.Point(14, 72);
            this.lblTextoUsuarios.Name = "lblTextoUsuarios";
            this.lblTextoUsuarios.TabIndex = 7;
            this.lblTextoUsuarios.Text = "Usuarios registrados";
            // 
            // pnlActivos
            // 
            this.pnlActivos.BackColor = System.Drawing.Color.FromArgb(((242)), ((244)), ((247)));
            this.pnlActivos.Location = new System.Drawing.Point(500, 120);
            this.pnlActivos.Name = "pnlActivos";
            this.pnlActivos.Size = new System.Drawing.Size(225, 110);
            this.pnlActivos.TabIndex = 8;
            this.pnlActivos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlActivos.Controls.Add(this.lblValorActivos);
            this.pnlActivos.Controls.Add(this.lblTextoActivos);
            // 
            // lblValorActivos
            // 
            this.lblValorActivos.AutoSize = false;
            this.lblValorActivos.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorActivos.ForeColor = System.Drawing.Color.FromArgb(((31)), ((78)), ((121)));
            this.lblValorActivos.Location = new System.Drawing.Point(10, 12);
            this.lblValorActivos.Name = "lblValorActivos";
            this.lblValorActivos.Size = new System.Drawing.Size(200, 50);
            this.lblValorActivos.TabIndex = 9;
            this.lblValorActivos.Text = "-";
            this.lblValorActivos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTextoActivos
            // 
            this.lblTextoActivos.AutoSize = true;
            this.lblTextoActivos.ForeColor = System.Drawing.Color.FromArgb(((90)), ((90)), ((90)));
            this.lblTextoActivos.Location = new System.Drawing.Point(14, 72);
            this.lblTextoActivos.Name = "lblTextoActivos";
            this.lblTextoActivos.TabIndex = 10;
            this.lblTextoActivos.Text = "Préstamos activos";
            // 
            // pnlVencidos
            // 
            this.pnlVencidos.BackColor = System.Drawing.Color.FromArgb(((242)), ((244)), ((247)));
            this.pnlVencidos.Location = new System.Drawing.Point(740, 120);
            this.pnlVencidos.Name = "pnlVencidos";
            this.pnlVencidos.Size = new System.Drawing.Size(225, 110);
            this.pnlVencidos.TabIndex = 11;
            this.pnlVencidos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVencidos.Controls.Add(this.lblValorVencidos);
            this.pnlVencidos.Controls.Add(this.lblTextoVencidos);
            // 
            // lblValorVencidos
            // 
            this.lblValorVencidos.AutoSize = false;
            this.lblValorVencidos.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorVencidos.ForeColor = System.Drawing.Color.FromArgb(((31)), ((78)), ((121)));
            this.lblValorVencidos.Location = new System.Drawing.Point(10, 12);
            this.lblValorVencidos.Name = "lblValorVencidos";
            this.lblValorVencidos.Size = new System.Drawing.Size(200, 50);
            this.lblValorVencidos.TabIndex = 12;
            this.lblValorVencidos.Text = "-";
            this.lblValorVencidos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTextoVencidos
            // 
            this.lblTextoVencidos.AutoSize = true;
            this.lblTextoVencidos.ForeColor = System.Drawing.Color.FromArgb(((90)), ((90)), ((90)));
            this.lblTextoVencidos.Location = new System.Drawing.Point(14, 72);
            this.lblTextoVencidos.Name = "lblTextoVencidos";
            this.lblTextoVencidos.TabIndex = 13;
            this.lblTextoVencidos.Text = "Préstamos vencidos";
            // 
            // grpConexion
            // 
            this.grpConexion.Location = new System.Drawing.Point(20, 250);
            this.grpConexion.Name = "grpConexion";
            this.grpConexion.Size = new System.Drawing.Size(945, 75);
            this.grpConexion.TabIndex = 14;
            this.grpConexion.Text = "Conexión con SQL Server";
            this.grpConexion.Controls.Add(this.lblConexion);
            this.grpConexion.Controls.Add(this.btnActualizar);
            // 
            // lblConexion
            // 
            this.lblConexion.AutoSize = false;
            this.lblConexion.Location = new System.Drawing.Point(15, 30);
            this.lblConexion.Name = "lblConexion";
            this.lblConexion.Size = new System.Drawing.Size(780, 30);
            this.lblConexion.TabIndex = 15;
            this.lblConexion.Text = "Comprobando...";
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(810, 26);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(120, 32);
            this.btnActualizar.TabIndex = 16;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // grpModulos
            // 
            this.grpModulos.Location = new System.Drawing.Point(20, 340);
            this.grpModulos.Name = "grpModulos";
            this.grpModulos.Size = new System.Drawing.Size(945, 230);
            this.grpModulos.TabIndex = 17;
            this.grpModulos.Text = "Módulos del sistema";
            this.grpModulos.Controls.Add(this.lblModulos);
            // 
            // lblModulos
            // 
            this.lblModulos.AutoSize = false;
            this.lblModulos.Location = new System.Drawing.Point(15, 28);
            this.lblModulos.Name = "lblModulos";
            this.lblModulos.Size = new System.Drawing.Size(915, 190);
            this.lblModulos.TabIndex = 18;
            this.lblModulos.Text = "Libros: registrar, consultar, editar y eliminar libros; relacionarlos con autores y editoriales.\r\nUsuarios: registrar y mantener los datos de los lectores de la biblioteca.\r\nAutores y Editoriales: administrar la información bibliográfica.\r\nPréstamos: prestar libros, registrar devoluciones y ver el estado de cada préstamo.\r\nReportes: consultar listados, préstamos activos, vencidos, devueltos e historial.";
            // 
            // FrmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmInicio";
            this.Text = "Inicio";
            this.ClientSize = new System.Drawing.Size(1000, 620);
            this.Controls.Add(this.lblEncabezado);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.pnlLibros);
            this.Controls.Add(this.pnlUsuarios);
            this.Controls.Add(this.pnlActivos);
            this.Controls.Add(this.pnlVencidos);
            this.Controls.Add(this.grpConexion);
            this.Controls.Add(this.grpModulos);
            this.Load += new System.EventHandler(this.FrmInicio_Load);
            this.pnlLibros.ResumeLayout(false);
            this.pnlLibros.PerformLayout();
            this.pnlUsuarios.ResumeLayout(false);
            this.pnlUsuarios.PerformLayout();
            this.pnlActivos.ResumeLayout(false);
            this.pnlActivos.PerformLayout();
            this.pnlVencidos.ResumeLayout(false);
            this.pnlVencidos.PerformLayout();
            this.grpConexion.ResumeLayout(false);
            this.grpConexion.PerformLayout();
            this.grpModulos.ResumeLayout(false);
            this.grpModulos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEncabezado;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlLibros;
        private System.Windows.Forms.Label lblValorLibros;
        private System.Windows.Forms.Label lblTextoLibros;
        private System.Windows.Forms.Panel pnlUsuarios;
        private System.Windows.Forms.Label lblValorUsuarios;
        private System.Windows.Forms.Label lblTextoUsuarios;
        private System.Windows.Forms.Panel pnlActivos;
        private System.Windows.Forms.Label lblValorActivos;
        private System.Windows.Forms.Label lblTextoActivos;
        private System.Windows.Forms.Panel pnlVencidos;
        private System.Windows.Forms.Label lblValorVencidos;
        private System.Windows.Forms.Label lblTextoVencidos;
        private System.Windows.Forms.GroupBox grpConexion;
        private System.Windows.Forms.Label lblConexion;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.GroupBox grpModulos;
        private System.Windows.Forms.Label lblModulos;
    }
}
