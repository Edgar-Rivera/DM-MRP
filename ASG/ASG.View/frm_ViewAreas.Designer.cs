using System.Security.Cryptography;

namespace ASG.ASG.View
{
    partial class frm_ViewAreas
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

        public void setDetalleArea(string idArea, string descripcion, string encargado)
        {
            lblCodArea.Text = idArea;
            lblDescripArea.Text = descripcion;
            lblEncargado.Text = encargado;
        }



        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCodArea = new System.Windows.Forms.Label();
            this.lblDescripArea = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblEncargado = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCodArea
            // 
            this.lblCodArea.BackColor = System.Drawing.Color.Transparent;
            this.lblCodArea.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodArea.ForeColor = System.Drawing.Color.DarkGray;
            this.lblCodArea.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCodArea.Location = new System.Drawing.Point(233, 139);
            this.lblCodArea.Name = "lblCodArea";
            this.lblCodArea.Size = new System.Drawing.Size(190, 28);
            this.lblCodArea.TabIndex = 357;
            this.lblCodArea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDescripArea
            // 
            this.lblDescripArea.BackColor = System.Drawing.Color.Transparent;
            this.lblDescripArea.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripArea.ForeColor = System.Drawing.Color.DarkGray;
            this.lblDescripArea.Location = new System.Drawing.Point(39, 251);
            this.lblDescripArea.Name = "lblDescripArea";
            this.lblDescripArea.Size = new System.Drawing.Size(800, 96);
            this.lblDescripArea.TabIndex = 355;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(39, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 28);
            this.label2.TabIndex = 352;
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
            this.label17.TabIndex = 351;
            this.label17.Text = "Código de Área:";
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
            this.label5.TabIndex = 350;
            this.label5.Text = "Multivelas";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(39, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 21);
            this.label1.TabIndex = 349;
            this.label1.Text = "Consulta los datos del área registrada";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(101)))), ((int)(((byte)(36)))));
            this.panel2.Location = new System.Drawing.Point(16, 65);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(850, 3);
            this.panel2.TabIndex = 348;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Image = global::ASG.Properties.Resources.box;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(28, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(395, 28);
            this.label4.TabIndex = 347;
            this.label4.Text = "Información del Área de Producción";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::ASG.Properties.Resources.cancel__3_;
            this.pictureBox3.Location = new System.Drawing.Point(851, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(14, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 346;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // lblEncargado
            // 
            this.lblEncargado.BackColor = System.Drawing.Color.Transparent;
            this.lblEncargado.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncargado.ForeColor = System.Drawing.Color.DarkGray;
            this.lblEncargado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEncargado.Location = new System.Drawing.Point(649, 139);
            this.lblEncargado.Name = "lblEncargado";
            this.lblEncargado.Size = new System.Drawing.Size(190, 28);
            this.lblEncargado.TabIndex = 359;
            this.lblEncargado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(454, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(169, 28);
            this.label6.TabIndex = 358;
            this.label6.Text = "Encargado de Área:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frm_ViewAreas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(39)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(880, 477);
            this.Controls.Add(this.lblEncargado);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblCodArea);
            this.Controls.Add(this.lblDescripArea);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frm_ViewAreas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_ViewAreas";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_ViewAreas_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frm_ViewAreas_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frm_ViewAreas_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frm_ViewAreas_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblCodArea;
        private System.Windows.Forms.Label lblDescripArea;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblEncargado;
        private System.Windows.Forms.Label label6;
    }
}