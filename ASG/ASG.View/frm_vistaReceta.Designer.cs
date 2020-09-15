namespace ASG.ASG.View
{
    partial class frm_vistaReceta
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

        public void setDatosReceta(string codReceta, string mercaderia, string descripcionReceta, string fecha, string creador)
        {
            lblCodReceta.Text = codReceta;
            lblMercaderiaReceta.Text = mercaderia;
            lblDescripReceta.Text = descripcionReceta;
            lblFechaReceta.Text = fecha;
            lblCreadoPor.Text = creador;
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCreadoPor = new System.Windows.Forms.Label();
            this.lblFechaReceta = new System.Windows.Forms.Label();
            this.lblCodReceta = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDescripReceta = new System.Windows.Forms.Label();
            this.lblC = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMercaderiaReceta = new System.Windows.Forms.Label();
            this.btnDetalleReceta = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCreadoPor
            // 
            this.lblCreadoPor.BackColor = System.Drawing.Color.Transparent;
            this.lblCreadoPor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreadoPor.ForeColor = System.Drawing.Color.DarkGray;
            this.lblCreadoPor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCreadoPor.Location = new System.Drawing.Point(663, 185);
            this.lblCreadoPor.Name = "lblCreadoPor";
            this.lblCreadoPor.Size = new System.Drawing.Size(176, 28);
            this.lblCreadoPor.TabIndex = 341;
            this.lblCreadoPor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFechaReceta
            // 
            this.lblFechaReceta.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaReceta.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaReceta.ForeColor = System.Drawing.Color.DarkGray;
            this.lblFechaReceta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFechaReceta.Location = new System.Drawing.Point(659, 140);
            this.lblFechaReceta.Name = "lblFechaReceta";
            this.lblFechaReceta.Size = new System.Drawing.Size(179, 28);
            this.lblFechaReceta.TabIndex = 339;
            this.lblFechaReceta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCodReceta
            // 
            this.lblCodReceta.BackColor = System.Drawing.Color.Transparent;
            this.lblCodReceta.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodReceta.ForeColor = System.Drawing.Color.DarkGray;
            this.lblCodReceta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCodReceta.Location = new System.Drawing.Point(233, 139);
            this.lblCodReceta.Name = "lblCodReceta";
            this.lblCodReceta.Size = new System.Drawing.Size(190, 28);
            this.lblCodReceta.TabIndex = 338;
            this.lblCodReceta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Location = new System.Drawing.Point(39, 185);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(142, 28);
            this.label8.TabIndex = 334;
            this.label8.Text = "Mercaderia:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDescripReceta
            // 
            this.lblDescripReceta.BackColor = System.Drawing.Color.Transparent;
            this.lblDescripReceta.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripReceta.ForeColor = System.Drawing.Color.DarkGray;
            this.lblDescripReceta.Location = new System.Drawing.Point(39, 289);
            this.lblDescripReceta.Name = "lblDescripReceta";
            this.lblDescripReceta.Size = new System.Drawing.Size(800, 96);
            this.lblDescripReceta.TabIndex = 333;
            // 
            // lblC
            // 
            this.lblC.BackColor = System.Drawing.Color.Transparent;
            this.lblC.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblC.ForeColor = System.Drawing.Color.White;
            this.lblC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblC.Location = new System.Drawing.Point(465, 186);
            this.lblC.Name = "lblC";
            this.lblC.Size = new System.Drawing.Size(144, 28);
            this.lblC.TabIndex = 332;
            this.lblC.Text = "Creado Por:";
            this.lblC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(465, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 28);
            this.label3.TabIndex = 331;
            this.label3.Text = "Fecha de Creación:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(39, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 28);
            this.label2.TabIndex = 330;
            this.label2.Text = "Descripción:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label17.Location = new System.Drawing.Point(39, 138);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(169, 28);
            this.label17.TabIndex = 329;
            this.label17.Text = "Código de Receta:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(14, 448);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 15);
            this.label5.TabIndex = 328;
            this.label5.Text = "Multivelas";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(61, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 21);
            this.label1.TabIndex = 327;
            this.label1.Text = "Consulta los datos de la receta registrada";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(101)))), ((int)(((byte)(36)))));
            this.panel2.Location = new System.Drawing.Point(16, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(850, 3);
            this.panel2.TabIndex = 326;
            // 
            // lblMercaderiaReceta
            // 
            this.lblMercaderiaReceta.BackColor = System.Drawing.Color.Transparent;
            this.lblMercaderiaReceta.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMercaderiaReceta.ForeColor = System.Drawing.Color.DarkGray;
            this.lblMercaderiaReceta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMercaderiaReceta.Location = new System.Drawing.Point(237, 184);
            this.lblMercaderiaReceta.Name = "lblMercaderiaReceta";
            this.lblMercaderiaReceta.Size = new System.Drawing.Size(186, 28);
            this.lblMercaderiaReceta.TabIndex = 345;
            this.lblMercaderiaReceta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDetalleReceta
            // 
            this.btnDetalleReceta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(39)))), ((int)(((byte)(48)))));
            this.btnDetalleReceta.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDetalleReceta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetalleReceta.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetalleReceta.ForeColor = System.Drawing.Color.White;
            this.btnDetalleReceta.Image = global::ASG.Properties.Resources.binoculars___Copy;
            this.btnDetalleReceta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetalleReceta.Location = new System.Drawing.Point(684, 424);
            this.btnDetalleReceta.Name = "btnDetalleReceta";
            this.btnDetalleReceta.Size = new System.Drawing.Size(179, 35);
            this.btnDetalleReceta.TabIndex = 344;
            this.btnDetalleReceta.Text = "Ver Detalle de Receta";
            this.btnDetalleReceta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDetalleReceta.UseVisualStyleBackColor = false;
            this.btnDetalleReceta.Click += new System.EventHandler(this.btnDetalleReceta_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Image = global::ASG.Properties.Resources.box;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(28, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(281, 28);
            this.label4.TabIndex = 325;
            this.label4.Text = "Información de la Receta";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::ASG.Properties.Resources.cancel__3_;
            this.pictureBox3.Location = new System.Drawing.Point(850, 11);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(14, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 324;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // frm_vistaReceta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(39)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(880, 477);
            this.Controls.Add(this.lblMercaderiaReceta);
            this.Controls.Add(this.btnDetalleReceta);
            this.Controls.Add(this.lblCreadoPor);
            this.Controls.Add(this.lblFechaReceta);
            this.Controls.Add(this.lblCodReceta);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblDescripReceta);
            this.Controls.Add(this.lblC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frm_vistaReceta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_vistaReceta";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_vistaReceta_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frm_vistaReceta_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frm_vistaReceta_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frm_vistaReceta_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDetalleReceta;
        private System.Windows.Forms.Label lblCreadoPor;
        private System.Windows.Forms.Label lblFechaReceta;
        private System.Windows.Forms.Label lblCodReceta;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDescripReceta;
        private System.Windows.Forms.Label lblC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblMercaderiaReceta;
    }
}