namespace ProyectoFinal.Formularios
{
    partial class Reportes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtgvRegistros = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNombreusuario = new System.Windows.Forms.TextBox();
            this.txtNombrelenguaje = new System.Windows.Forms.TextBox();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btntxt = new System.Windows.Forms.Button();
            this.btnexcel = new System.Windows.Forms.Button();
            this.btnCSV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvRegistros)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgvRegistros
            // 
            this.dtgvRegistros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvRegistros.Location = new System.Drawing.Point(3, 122);
            this.dtgvRegistros.Name = "dtgvRegistros";
            this.dtgvRegistros.Size = new System.Drawing.Size(522, 150);
            this.dtgvRegistros.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filtro";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nombre Usuario";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(179, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Nombre Lenguaje";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(373, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "label4";
            // 
            // txtNombreusuario
            // 
            this.txtNombreusuario.Location = new System.Drawing.Point(23, 62);
            this.txtNombreusuario.Name = "txtNombreusuario";
            this.txtNombreusuario.Size = new System.Drawing.Size(100, 20);
            this.txtNombreusuario.TabIndex = 5;
            // 
            // txtNombrelenguaje
            // 
            this.txtNombrelenguaje.Location = new System.Drawing.Point(182, 62);
            this.txtNombrelenguaje.Name = "txtNombrelenguaje";
            this.txtNombrelenguaje.Size = new System.Drawing.Size(100, 20);
            this.txtNombrelenguaje.TabIndex = 6;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(283, 173);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(75, 23);
            this.btnFiltrar.TabIndex = 7;
            this.btnFiltrar.Text = "filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(430, 283);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 8;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btntxt
            // 
            this.btntxt.Location = new System.Drawing.Point(3, 283);
            this.btntxt.Name = "btntxt";
            this.btntxt.Size = new System.Drawing.Size(75, 23);
            this.btntxt.TabIndex = 9;
            this.btntxt.Text = "TXT";
            this.btntxt.UseVisualStyleBackColor = true;
            this.btntxt.Click += new System.EventHandler(this.btntxt_Click);
            // 
            // btnexcel
            // 
            this.btnexcel.Location = new System.Drawing.Point(95, 283);
            this.btnexcel.Name = "btnexcel";
            this.btnexcel.Size = new System.Drawing.Size(75, 23);
            this.btnexcel.TabIndex = 10;
            this.btnexcel.Text = "XLSX";
            this.btnexcel.UseVisualStyleBackColor = true;
            this.btnexcel.Click += new System.EventHandler(this.btnexcel_Click);
            // 
            // btnCSV
            // 
            this.btnCSV.Location = new System.Drawing.Point(182, 283);
            this.btnCSV.Name = "btnCSV";
            this.btnCSV.Size = new System.Drawing.Size(75, 23);
            this.btnCSV.TabIndex = 11;
            this.btnCSV.Text = "CSV";
            this.btnCSV.UseVisualStyleBackColor = true;
            this.btnCSV.Click += new System.EventHandler(this.btnCSV_Click);
            // 
            // Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(532, 318);
            this.Controls.Add(this.btnCSV);
            this.Controls.Add(this.btnexcel);
            this.Controls.Add(this.btntxt);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.txtNombrelenguaje);
            this.Controls.Add(this.txtNombreusuario);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtgvRegistros);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Reportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Reportes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvRegistros)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgvRegistros;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNombreusuario;
        private System.Windows.Forms.TextBox txtNombrelenguaje;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btntxt;
        private System.Windows.Forms.Button btnexcel;
        private System.Windows.Forms.Button btnCSV;
    }
}